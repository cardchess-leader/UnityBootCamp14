using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PaperPlanePhysics : BasePlaneController
{

    private int rollTapIndex;
    private float rollTapTimer;

    private Vector3 initialPos;
    private Quaternion initialRot;

    private bool performingRollRoutine;


    protected override void Start()
    {
        base.Start();
        initialPos = transform.position;
        initialRot = transform.rotation;

        rollTapTimer = rollInputTapBufferTime;
    }

    protected override void Update()
    {
        base.Update();

        //HandleReset(); // Uncomment this later
        UpdateBodyRoll();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (enableLateralForce)
        {
            HandleLateralForce();
        }
    }

    private void HandleLateralForce()
    {
        if (inputHandler.IsRightThrustDown)
        {
            inputHandler.IsRightThrustDown = false;
            _currentRoll -= rollSpeed;
            var localRight = transform.right;

            var horizontalRight = Vector3.ProjectOnPlane(localRight, Vector3.up).normalized;
            _rigidbody.AddForce(horizontalRight * lateralForceAmount, ForceMode.Impulse);
        }

        if (inputHandler.IsLeftThrustDown)
        {
            inputHandler.IsLeftThrustDown = false;
            _currentRoll += rollSpeed;
            var localLeft = -transform.right;

            var horizontalLeft = Vector3.ProjectOnPlane(localLeft, Vector3.up).normalized;
            _rigidbody.AddForce(horizontalLeft * lateralForceAmount, ForceMode.Impulse);
        }
    }


    private void UpdateBodyRoll()
    {
        if (enableTapRoll)
        {
            if (rollTapTimer >= -0.01f)
            {
                rollTapTimer -= Time.deltaTime;
            }

            if (rollTapTimer <= 0)
            {
                rollTapIndex = 0;
                rollTapTimer = -1f;
                performingRollRoutine = false;
            }

            if (inputHandler.IsRollInputDown)
            {
                performingRollRoutine = true;
                rollTapIndex++;

                rollTapTimer = rollInputTapBufferTime;

                if (rollTapIndex >= 2)
                {
                    StopCoroutine(RollCoroutine());
                    StartCoroutine(RollCoroutine());
                    _currentAdditionalRoll = 0f;
                    rollTapIndex = 0;
                    rollTapTimer = -1f;
                }
            }
        }

        if (performingRollRoutine) return;

        if (!enableAdditionalRoll) return;

        if (inputHandler.RollInput > 0)
        {
            _currentAdditionalRoll -= additionalRollSpeed * Time.deltaTime;

            if (_currentAdditionalRoll > 360f)
            {
                _currentAdditionalRoll = 0f;
            }
        }

        else if (inputHandler.RollInput < 0)
        {
            _currentAdditionalRoll += additionalRollSpeed * Time.deltaTime;

            if (_currentAdditionalRoll < -360f)
            {
                _currentAdditionalRoll = 0f;
            }
        }
        else
        {
            _currentAdditionalRoll = Mathf.Lerp(_currentAdditionalRoll, 0f, additionalRollSpeed * Time.deltaTime);
        }

        if (clampAdditionalRoll)
        {
            _currentAdditionalRoll = Mathf.Clamp(_currentAdditionalRoll, -clampRollValue, clampRollValue);
        }
    }

    private IEnumerator RollCoroutine()
    {
        performingRollRoutine = true;

        var rollTime = tapRollDuration;

        for (int i = 0; i < rollCount; i++)
        {
            var timer = 0f;
            float startRoll = _currentAdditionalRoll + i * 360f * inputHandler.RollDirection;

            float targetRoll = startRoll + (inputHandler.RollDirection > 0 ? -360f : 360f);

            while (timer < rollTime)
            {
                timer += Time.deltaTime;
                _currentAdditionalRoll = Mathf.Lerp(startRoll, targetRoll, timer / rollTime);
                yield return null;
            }
        }

        performingRollRoutine = false;
    }


    private void HandleReset()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            _rigidbody.isKinematic = true;

            transform.position = initialPos;
            transform.rotation = initialRot;

            _rigidbody.isKinematic = false;
            _currentPitch = _currentRoll = 0f;

            _rigidbody.linearVelocity = transform.forward * 15f;

            _isCrashed = false;
            OnReset?.Invoke();
        }
    }
}