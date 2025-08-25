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

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        // 스페이스바를 누르면 미사일 발사
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Missile requires 10 ammo
            if (currentAmmo < 10) return;
            currentAmmo -= 10;
            GameObject missile = Instantiate(missilePrefab, firePoint.transform.position, transform.rotation * Quaternion.Euler(90f, 0f, 0f));
            missile.GetComponent<Missile>().SetInitSpeed(GetComponent<Rigidbody>().linearVelocity.magnitude);
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

    private void FireBullet(Vector3 position)
    {
        if (BulletPool.Instance != null)
        {
            GameObject bullet = BulletPool.Instance.GetBullet();
            if (bullet != null)
            {
                bullet.transform.position = position;
                bullet.transform.rotation = transform.rotation * Quaternion.Euler(90f, 0f, 0f);
                
                // Reset bullet state for reuse
                var bulletComponent = bullet.GetComponent<Bullet>();
                if (bulletComponent != null)
                {
                    bulletComponent.ResetBullet();
                }
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
        yield return new WaitForSeconds(3f);
        currentAmmo = maxAmmo;
        UIController.Instance.UpdateAmmoText(currentAmmo, maxAmmo);
        reloadCoroutine = null;
    }
}