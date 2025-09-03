using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using NUnit.Framework;

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
    Coroutine hellfireMissileCoroutine;

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
            // Missile requires 10 ammo
            if (currentAmmo < 10) return;
            currentAmmo -= 10;

            FireMissile();
            UIController.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
        }

        // 왼쪽 마우스 키를 누르고 있을 때 기관총 발사
        if (Input.GetMouseButton(0))
        {
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
        }

        // R 키를 누르면 재장전
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        // Q 키를 누르면 범위내에 있는 모든 적에게 미사일 발사
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Inventory.Instance.UseSkill(3);
            if (autoTargeting != null && hellfireMissileCoroutine == null)
            {
                hellfireMissileCoroutine = StartCoroutine(LaunchHellfireMissiles());
            }
        }
    }

    IEnumerator LaunchHellfireMissiles()
    {
        var targets = autoTargeting.FindNearbyEnemies();
        var validTargets = new List<EnemyBehavior>();
        foreach (var target in targets)
        {
            if (currentAmmo >= 10)
            {
                currentAmmo -= 10;
                target.ShowTarget();
                validTargets.Add(target);
            } else
            {
                break;
            }
        }
        UIController.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
        yield return new WaitForSeconds(1f); // 1초 대기 후 미사일 발사

        foreach (var target in validTargets)
        {
            FireMissile(target);
        }
        hellfireMissileCoroutine = null;
    }

    Missile CreateMissile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint = ray.GetPoint(200);
        Quaternion rotation = Quaternion.LookRotation((targetPoint - firePoint.transform.position).normalized);
        return Instantiate(missilePrefab, firePoint.transform.position + new Vector3(0, 0, 5f), rotation).GetComponent<Missile>();
    }

    private void FireMissile(EnemyBehavior target = null)
    {
        Missile missileComponent = CreateMissile();

        if (missileComponent != null)
        {
            missileComponent.SetInitSpeed(transform.parent.GetComponent<Rigidbody>().linearVelocity.magnitude);

            // 타겟이 정해져 있으면 타겟 설정
            if (target != null)
            {
                missileComponent.SetTarget(target);
            }
            else
            {
                // 타겟이 없으면 자동 타겟팅이 활성화되어 있으면 타겟 설정
                if (autoTargeting != null && autoTargeting.IsTargeting())
                {
                    EnemyBehavior autoTarget = autoTargeting.GetCurrentTarget();
                    if (autoTarget != null)
                    {
                        missileComponent.SetTarget(autoTarget);
                    }
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