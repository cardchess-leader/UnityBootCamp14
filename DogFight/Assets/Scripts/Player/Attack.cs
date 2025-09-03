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

    // �ڵ� Ÿ���� ����
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
        // �����̽��ٸ� ������ �̻��� �߻�
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // Missile requires 10 ammo
            if (currentAmmo < 10) return;
            currentAmmo -= 10;

            FireMissile();
            UIController.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
        }

        // ���� ���콺 Ű�� ������ ���� �� ����� �߻�
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

        // R Ű�� ������ ������
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        // Q Ű�� ������ �������� �ִ� ��� ������ �̻��� �߻�
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
        yield return new WaitForSeconds(1f); // 1�� ��� �� �̻��� �߻�

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

            // Ÿ���� ������ ������ Ÿ�� ����
            if (target != null)
            {
                missileComponent.SetTarget(target);
            }
            else
            {
                // Ÿ���� ������ �ڵ� Ÿ������ Ȱ��ȭ�Ǿ� ������ Ÿ�� ����
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

                // Ÿ���� ������ Ÿ�� ��������, ������ �⺻ �������� ����
                Quaternion targetRotation;

                if (autoTargeting != null && autoTargeting.IsTargeting())
                {
                    EnemyBehavior target = autoTargeting.GetCurrentTarget();
                    if (target != null)
                    {
                        // �Ѿ� ��ġ���� Ÿ�������� ���� ���
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