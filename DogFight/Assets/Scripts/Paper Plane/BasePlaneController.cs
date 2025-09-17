using System;
using System.Collections;
using UnityEngine;

public class BasePlaneController : MonoBehaviour
{
    // -------------------- AERODYNAMICS --------------------
    [Header("Aerodynamics")]
    [SerializeField]
    [Range(0, 2)]
    private float liftCoefficient = 0.5f;

    [SerializeField][Range(0, 3)] private float liftMultiplier = 1f;
    [SerializeField][Range(0, 1)] private float dragCoefficient = 0.1f;
    [SerializeField][Range(10f, 45f)] private float stallAngle = 35f;
    [SerializeField][Range(1f, 5f)] private float stallDragMultiplier = 3f;

    // -------------------- THRUST --------------------
    [Header("Thrust")][SerializeField] protected float baseThrust = 20f;
    [SerializeField] protected float boostMultiplier = 1.5f;
    [SerializeField] protected float maxSpeed = 150f;
    [SerializeField] protected float boostSpeedMultiplier = 1.2f;
    [SerializeField] protected bool enableLateralForce = false;
    [SerializeField][Range(0f, 100f)] protected float lateralForceAmount = 15f;

    // -------------------- CONTROLS --------------------
    [Header("Controls")][SerializeField] private float yawSpeed = 1f;
    [SerializeField] private float pitchSpeed = 1f;
    [SerializeField] protected float rollSpeed = 500f;
    [SerializeField] private float maxPitchAngle = 40f;

    // -------------------- VISUALS --------------------
    [Header("Visuals")][SerializeField] private Transform planeModel;
    [SerializeField] private TrailRenderer[] trails;

    // -------------------- WING SETTINGS --------------------
    [Header("Wing Settings")]
    [SerializeField]
    private Transform leftWing;

    [SerializeField] private Transform rightWing;
    [SerializeField] private float wingRotationZ = 90f;
    [SerializeField] private float bankAngleMultiplier = 45f;
    [SerializeField] private float wingBankTiltMultiplier = 1.25f;
    [SerializeField] private float wingPitchTiltMultiplier = 1f;

    // -------------------- PLANE BODY TILT --------------------
    [Header("Plane Body Tilt")]
    [SerializeField]
    private float planeBodyTiltSmoothness = 10f;

    [SerializeField] private float planeBodyPitchTiltMultiplier = 0.2f;

    // -------------------- BOOSTING SETTINGS --------------------
    [Header("Boosting Settings")]
    [SerializeField]
    private bool enableWingTiltWhenBoosting = false;

    [SerializeField] private float boostingWingTiltMultiplier = 60f;

    // -------------------- WING FLUTTER EFFECT --------------------
    [Header("Wing Flutter Effect")]
    [SerializeField]
    private bool enableSineWaveFlutters = false;

    [SerializeField] private float wingFlutterAmplitude = 0.1f;
    [SerializeField] private float wingFlutterSpeed = 10f;

    // -------------------- ADDITIONAL ROLL SETTINGS --------------------
    [Header("Additional Roll Settings")]
    [SerializeField]
    protected bool enableAdditionalRoll = false;

    [SerializeField] protected float additionalRollSpeed = 5f;

    // -------------------- Tap ROLL SETTINGS --------------------
    [Header("Tap Roll Settings")]
    [SerializeField] protected bool enableTapRoll;
    [SerializeField] protected int rollCount = 1;
    [SerializeField][Range(0.2f, 3f)] protected float tapRollDuration = 0.5f;
    [SerializeField] protected float rollInputTapBufferTime = 0.35f;

    // -------------------- ROLL CLAMP SETTINGS --------------------
    [Header("Roll Clamp Settings")]
    [SerializeField]
    protected bool clampAdditionalRoll = false;

    [SerializeField] protected float clampRollValue = 90f;

    // -------------------- ROTATION --------------------
    [Header("Rotation Settings")]
    [SerializeField]
    private float rotationSpeed = 10f;

    // -------------------- CRASH HANDLING --------------------
    [Header("Crash Handling")]
    [SerializeField]
    private bool enableCrashHandling = false;

    [SerializeField] private float crashForce = 10f;
    [SerializeField] private float crashTorque = 10f;

    protected bool _isCrashed = false;
    public Action OnCrash;
    public Action OnReset;

    // -------------------- Back Flip --------------------
    [Header("Back Flip")]
    [SerializeField] private bool enableBackFlip = false;
    [SerializeField][Range(1f, 3f)] private float backFlipDuration = 1.5f;
    [SerializeField] private float upwardsForce = 10f;
    [SerializeField] private float backFlipTorque = 2f;

    public Action OnBackFlipEnd;
    public Action OnBackFlipStart;

    protected bool _isBackFlipping = false;
    // -------------------- RUNTIME VARIABLES --------------------
    protected Rigidbody _rigidbody;
    protected float _currentPitch;
    protected float _currentRoll;
    protected Vector2 _controlInput;
    protected bool _isBoosting;
    private float _currentMaxSpeed;
    private float _currentThrust;
    private float _boostingWingTilt = 0f;

    private float smoothRotPitch = 0;
    private float pitchVelocity = 0f;
    private float smoothTime = 0.05f;
    protected float _currentAdditionalRoll;

    private const float VelocityThreshold = 0.1f;

    // -------------------- PUBLIC ACCESSORS --------------------
    public Rigidbody Rigidbody => _rigidbody;
    public float YawInput => _controlInput.x;
    public float PitchInput => _controlInput.y;
    public float MaxSpeed => maxSpeed;
    public bool IsBoosting => _isBoosting;

    protected BaseInputHandler inputHandler;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = true;

        trails = GetComponentsInChildren<TrailRenderer>();
        inputHandler = GetComponent<BaseInputHandler>();
    }

    protected virtual void Start()
    {
        _rigidbody.linearVelocity = transform.forward * 15f;
    }

    protected virtual void Update()
    {
        if (_isCrashed) return;

        if (_isBackFlipping) return;

        inputHandler.UpdateInputs();
        GatherInputs();

        UpdateVisuals();

        if (enableBackFlip)
        {
            HandleBackFlip();
        }
    }


    protected virtual void FixedUpdate()
    {
        if (_isCrashed) return;
        if (_isBackFlipping) return;
        ApplyAerodynamicForces();
        ApplyThrust();
        UpdateRotation();
    }


    private void GatherInputs()
    {
        _controlInput = inputHandler.ControlInput;
        _isBoosting = inputHandler.IsBoosting;
    }

    private void ApplyAerodynamicForces()
    {
        Vector3 velocity = _rigidbody.linearVelocity;
        if (velocity.magnitude < VelocityThreshold) return;

        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float angleOfAttack = Mathf.Atan2(localVelocity.y, localVelocity.z) * Mathf.Rad2Deg;

        float effectiveLift = CalculateLift(angleOfAttack, localVelocity.z);
        Vector3 liftForce = effectiveLift * transform.up;

        Vector3 dragForce = CalculateDrag(angleOfAttack, velocity);

        _rigidbody.AddForce(liftForce + dragForce, ForceMode.Force);
    }

    private float CalculateLift(float angleOfAttack, float forwardSpeed)
    {
        float liftFactor = Mathf.Clamp01(1 - Mathf.Abs(angleOfAttack) / stallAngle);
        float baseLift = liftCoefficient * forwardSpeed * forwardSpeed;
        return baseLift * liftFactor * liftMultiplier;
    }

    private Vector3 CalculateDrag(float angleOfAttack, Vector3 velocity)
    {
        float speedSquared = velocity.sqrMagnitude;
        float dragFactor = 1 + (Mathf.Abs(angleOfAttack) / stallAngle) * stallDragMultiplier;
        return -dragCoefficient * speedSquared * dragFactor * velocity.normalized;
    }


    private void ApplyThrust()
    {
        float targetMaxSpeed = _isBoosting ? maxSpeed * boostSpeedMultiplier : maxSpeed;
        float targetThrust = _isBoosting ? baseThrust * boostMultiplier : baseThrust;

        _currentThrust = Mathf.Lerp(_currentThrust, targetThrust, 2f * Time.fixedDeltaTime);
        _currentMaxSpeed = Mathf.Lerp(_currentMaxSpeed, targetMaxSpeed, 2f * Time.fixedDeltaTime);

        // Apply thrust only if below max speed to prevent extreme forces
        if (_rigidbody.linearVelocity.magnitude < _currentMaxSpeed)
        {
            _rigidbody.AddForce(transform.forward * _currentThrust, ForceMode.Acceleration);
        }
        
        // Smoother velocity clamping
        if (_rigidbody.linearVelocity.magnitude > _currentMaxSpeed)
        {
            _rigidbody.linearVelocity = _rigidbody.linearVelocity.normalized * _currentMaxSpeed;
        }
    }


    private void UpdateRotation()
    {
        _currentPitch += _controlInput.y * pitchSpeed;
        _currentPitch = Mathf.Clamp(_currentPitch, -maxPitchAngle, maxPitchAngle);

        smoothRotPitch = Mathf.SmoothDamp(smoothRotPitch, _currentPitch, ref pitchVelocity, smoothTime);

        if (_currentRoll >= 0)
        {
            _currentRoll -= rollSpeed * Time.fixedDeltaTime;
        }
        else if (_currentRoll <= 0)
        {
            _currentRoll += rollSpeed * Time.fixedDeltaTime;
        }

        Quaternion targetRotation =
            Quaternion.Euler(smoothRotPitch, _rigidbody.rotation.eulerAngles.y, _currentRoll);
        _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation,
            rotationSpeed * Time.fixedDeltaTime));

        float horizontal = (inputHandler.ControlInput.x * yawSpeed - _rigidbody.angularVelocity.y) *
                           Mathf.Abs(inputHandler.ControlInput.x);
        _rigidbody.AddRelativeTorque(Vector3.up * horizontal, ForceMode.VelocityChange);

        var angularVelocity = _rigidbody.angularVelocity;
        angularVelocity.z = 0f;

        if (horizontal == 0)
            angularVelocity.y = Mathf.Lerp(angularVelocity.y, 0, Time.fixedDeltaTime * rotationSpeed);

        if (_controlInput.y == 0)
        {
            angularVelocity.x = Mathf.Lerp(angularVelocity.x, 0, Time.fixedDeltaTime * rotationSpeed);
        }

        _rigidbody.angularVelocity = angularVelocity;
    }


    private void UpdateVisuals()
    {
        if (!planeModel) return;

        UpdateModelRotation();
        UpdateWingRotations();
    }


    private void UpdateModelRotation()
    {
        float bankAngle = -_controlInput.x * bankAngleMultiplier;
        bankAngle = Mathf.Clamp(bankAngle, -bankAngleMultiplier, bankAngleMultiplier);

        Quaternion targetModelRotation = Quaternion.Euler(
            Mathf.Abs(_controlInput.x) * maxPitchAngle * planeBodyPitchTiltMultiplier,
            0f,
            bankAngle + _currentAdditionalRoll
        );

        var currentRotation = planeModel.localRotation;

        var desiredRotation = Quaternion.Slerp(currentRotation, targetModelRotation,
            planeBodyTiltSmoothness * Time.deltaTime);

        planeModel.localRotation = desiredRotation;
    }

    private void UpdateWingRotations()
    {
        float wingTilt = Mathf.Sin(Time.time * wingFlutterSpeed) * wingFlutterAmplitude;
        wingTilt = enableSineWaveFlutters ? wingTilt : 0f;


        if (enableWingTiltWhenBoosting)
        {
            _boostingWingTilt = Mathf.Lerp(_boostingWingTilt, _isBoosting ? boostingWingTiltMultiplier : 0f,
                2f * Time.deltaTime);
        }

        var leftWingTargetRotation = Quaternion.Euler(
            _controlInput.y * maxPitchAngle * wingPitchTiltMultiplier,
            _controlInput.x * bankAngleMultiplier * wingBankTiltMultiplier * (_controlInput.x > 0 ? 1 : -1) +
            wingTilt + _boostingWingTilt,
            wingRotationZ
        );

        var rightWingTargetRotation = Quaternion.Euler(
            _controlInput.y * maxPitchAngle * wingPitchTiltMultiplier,
            _controlInput.x * bankAngleMultiplier * wingBankTiltMultiplier * (_controlInput.x > 0 ? -1 : 1) -
            wingTilt - _boostingWingTilt,
            -wingRotationZ
        );

        leftWing.localRotation =
            Quaternion.Slerp(leftWing.localRotation, leftWingTargetRotation, 5f * Time.deltaTime);
        rightWing.localRotation =
            Quaternion.Slerp(rightWing.localRotation, rightWingTargetRotation, 5f * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!enableCrashHandling) return;

        if (_isCrashed) return;

        _isCrashed = true;
        OnCrash?.Invoke();

        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        var contactPoint = other.GetContact(0);
        var forceDirection = -contactPoint.normal;

        _rigidbody.freezeRotation = false;

        _rigidbody.AddForce(forceDirection * crashForce, ForceMode.Impulse);
    }

    public void DisableEffects()
    {
        foreach (var trail in trails)
        {
            trail.emitting = false;
            trail.gameObject.SetActive(false);
        }
    }

    public void EnableEffects()
    {
        foreach (var trail in trails)
        {
            trail.emitting = true;
            trail.gameObject.SetActive(true);
        }
    }


    private void HandleBackFlip()
    {
        if (inputHandler.BackFlipInput)
        {
            inputHandler.BackFlipInput = false;
            StartCoroutine(PerformBackFlip());
        }
    }



    private IEnumerator PerformBackFlip()
    {
        Vector3 currentEulerAngles = transform.eulerAngles;

        _isBackFlipping = true;
        OnBackFlipStart?.Invoke();

        _rigidbody.AddForce(Vector3.up * upwardsForce, ForceMode.Impulse);

        float elapsedTime = 0f;
        float duration = backFlipDuration;
        float targetRotation = 360f;

        float angularVelocity = (targetRotation * Mathf.Deg2Rad) / duration;
        _rigidbody.AddTorque(-transform.right * angularVelocity * backFlipTorque, ForceMode.VelocityChange);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _rigidbody.linearVelocity = transform.forward * _rigidbody.linearVelocity.magnitude;
            yield return null;
        }

        _rigidbody.angularVelocity = Vector3.zero;

        _isBackFlipping = false;
        OnBackFlipEnd?.Invoke();
    }
}
