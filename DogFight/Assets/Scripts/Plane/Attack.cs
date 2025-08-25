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
            GameObject missile = Instantiate(missilePrefab, firePoint.transform.position + transform.forward * 2, transform.rotation * Quaternion.Euler(90f, 0f, 0f));
            missile.GetComponent<Missile>().SetInitSpeed(GetComponent<Rigidbody>().linearVelocity.magnitude);
        }
    }
}
