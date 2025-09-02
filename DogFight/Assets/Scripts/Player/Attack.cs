using System.Collections;
using UnityEngine;
using Enemy;

public class Attack : MonoBehaviour
{
    [SerializeField]
    GameObject missilePrefab;
    [SerializeField]
    GameObject firePoint;
    [SerializeField]
    float spreadAngle = 2f; // degrees of random spread for bullets
    [SerializeField]
    int maxAmmo;
    [SerializeField]
    float fireCooldown = 0.2f; // Minimum time between shots
    int currentAmmo;
    Coroutine reloadCoroutine;
    float lastFireTime = 0f;

    // 자동 타겟팅 참조
    private AutoTargeting autoTargeting;

    private void Start()
    {
        currentAmmo = maxAmmo;
        autoTargeting = GetComponent<AutoTargeting>();

        if (autoTargeting == null)
        {
            Debug.LogWarning("AutoTargeting component not found on " + gameObject.name);
        }
    }

    void Update()
    {
        // 스페이스바를 누르면 미사일 발사
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            switch(Inventory.Instance.weaponMode)
            {
                case Inventory.WeaponMode.Missile:
                    // Missile requires 10 ammo
                    if (currentAmmo < 10) return;
                    currentAmmo -= 10;

                    FireMissile();
                    UIController.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
                    break;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            switch (Inventory.Instance.weaponMode)
            {
                case Inventory.WeaponMode.MachineGun:
                    // Give small fraction of cooltime to prevent too fast shooting
                    if (Time.time - lastFireTime < fireCooldown) return;
                    lastFireTime = Time.time;

                    // Bullet requires 1 ammo
                    if (currentAmmo < 1) return;
                    currentAmmo -= 1;
                    // Double barrel gun fire using BulletPool
                    // Fire from multiple points
                    FireBullet(firePoint.transform.position + new Vector3(4f, 0, 0.3f));
                    FireBullet(firePoint.transform.position + new Vector3(-4f, 0, 0.1f));
                    UIController.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
                    break;
            }
        }
        // Start reloading when 'R' is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    Missile CreateMissile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint = ray.GetPoint(200);
        Quaternion rotation = Quaternion.LookRotation((targetPoint - firePoint.transform.position).normalized);
        return Instantiate(missilePrefab, firePoint.transform.position + new Vector3(0, 0, 5f), rotation).GetComponent<Missile>();
    }

    private void FireMissile()
    {
        Missile missileComponent = CreateMissile();

        if (missileComponent != null)
        {
            missileComponent.SetInitSpeed(transform.parent.GetComponent<Rigidbody>().linearVelocity.magnitude);

            // 자동 타겟팅이 활성화되어 있으면 타겟 설정
            if (autoTargeting != null && autoTargeting.IsTargeting())
            {
                EnemyBehavior target = autoTargeting.GetCurrentTarget();
                if (target != null)
                {
                    missileComponent.SetTarget(target);
                }
            }
        }
    }

    private void FireBullet(Vector3 position)
    {
        if (BulletPool.Instance != null)
        {
            GameObject bullet = BulletPool.Instance.GetBullet();
            if (bullet != null)
            {
                bullet.transform.position = position;

                // 타겟이 있으면 타겟 방향으로, 없으면 기본 방향으로 설정
                Quaternion targetRotation;

                if (autoTargeting != null && autoTargeting.IsTargeting())
                {
                    EnemyBehavior target = autoTargeting.GetCurrentTarget();
                    if (target != null)
                    {
                        // 총알 위치에서 타겟으로의 방향 계산
                        Vector3 directionToTarget = (target.transform.position - position).normalized;
                        targetRotation = Quaternion.LookRotation(directionToTarget);
                    }
                    else
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        Vector3 targetPoint = ray.GetPoint(200);
                        targetRotation = Quaternion.LookRotation((targetPoint - position).normalized);
                    }
                }
                else
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Vector3 targetPoint = ray.GetPoint(200);
                    targetRotation = Quaternion.LookRotation((targetPoint - position).normalized);
                }

                bullet.transform.rotation = targetRotation;
                bullet.transform.Rotate(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), 0f, Space.Self);

                bullet.GetComponent<Bullet>()?.ResetBullet();
            }
        }
    }

    // Reload ammo over time
    public void Reload()
    {
        if (reloadCoroutine == null)
        {
            reloadCoroutine = StartCoroutine(ReloadCoroutine());
        }
    }

    // Refill all ammos after 3 seconds
    IEnumerator ReloadCoroutine()
    {
        UIController.Instance.ShowReloadingText();
        yield return new WaitForSeconds(3f);
        currentAmmo = maxAmmo;
        UIController.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
        reloadCoroutine = null;
    }
}