using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float speed;
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 5;
        rb = GetComponent<Rigidbody>();
        // GetComponent<T>();
        Debug.Log("������ �Ϸ�Ǿ����ϴ�!");

    }

    // Update is called once per frame
    void Update()
    {
        // ���� �̵�
        float horizontal = Input.GetAxis("Horizontal");
        // ���� �̵�
        float vertical = Input.GetAxis("Vertical");
        // �̵� ��ǥ(����) ����
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        rb.AddForce(movement * speed * Time.deltaTime * 60);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Itembox"))
        {
            speed*=1.5f;
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            speed/=1.5f;
            other.gameObject.SetActive(false);
        }
    }

}
