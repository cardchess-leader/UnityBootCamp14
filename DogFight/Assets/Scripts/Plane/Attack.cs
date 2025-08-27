using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    GameObject missilePrefab;
    [SerializeField]
    GameObject firePoint;
    [SerializeField]
    int maxAmmo;
    int currentAmmo;
    Coroutine reloadCoroutine;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Missile requires 10 ammo
            if (currentAmmo < 10) return;
            currentAmmo -= 10;

            FireMissile();
            UIController.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Bullet requires 1 ammo
            if (currentAmmo < 1) return;
            currentAmmo -= 1;
            // Double barrel gun fire using BulletPool
            FireBullet(firePoint.transform.position + new Vector3(0.5f, 0, 0.3f));
            FireBullet(firePoint.transform.position + new Vector3(-0.5f, 0, 0.1f));
            UIController.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
        }
        // Start reloading when 'R' is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void FireMissile()
    {
        GameObject missile = Instantiate(missilePrefab, firePoint.transform.position + new Vector3(0, 0, 5f), transform.rotation);
        Missile missileComponent = missile.GetComponent<Missile>();

        if (missileComponent != null)
        {
            missileComponent.SetInitSpeed(transform.parent.GetComponent<Rigidbody>().linearVelocity.magnitude);

            // 자동 타겟팅이 활성화되어 있으면 타겟 설정
            if (autoTargeting != null && autoTargeting.IsTargeting())
            {
                Enemy target = autoTargeting.GetCurrentTarget();
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
                    Enemy target = autoTargeting.GetCurrentTarget();
                    if (target != null)
                    {
                        // 총알 위치에서 타겟으로의 방향 계산
                        Vector3 directionToTarget = (target.transform.position - position).normalized;
                        targetRotation = Quaternion.LookRotation(directionToTarget);
                    }
                    else
                    {
                        // 타겟이 없으면 기본 방향 (비행기 앞쪽)
                        targetRotation = transform.rotation;
                    }
                }
                else
                {
                    // 자동 타겟팅이 비활성화되어 있으면 기본 방향
                    targetRotation = transform.rotation;
                }

                bullet.transform.rotation = targetRotation;
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