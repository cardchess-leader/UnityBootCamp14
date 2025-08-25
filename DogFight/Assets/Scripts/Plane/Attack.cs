using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    GameObject missilePrefab;
    [SerializeField]
    GameObject firePoint;

    // Update is called once per frame
    void Update()
    {
        // 스페이스바를 누르면 미사일 발사
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject missile = Instantiate(missilePrefab, firePoint.transform.position, transform.rotation * Quaternion.Euler(90f, 0f, 0f));
            missile.GetComponent<Missile>().SetInitSpeed(GetComponent<Rigidbody>().linearVelocity.magnitude);
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Double barrel gun fire using BulletPool
            FireBullet(firePoint.transform.position + new Vector3(0.5f, 0, 0.3f));
            FireBullet(firePoint.transform.position + new Vector3(-0.5f, 0, 0.1f));
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
}
