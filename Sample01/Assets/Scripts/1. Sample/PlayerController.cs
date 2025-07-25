using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public Text text;
    Rigidbody rb;
    float speed;
    private int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 5;
        rb = GetComponent<Rigidbody>();
        // GetComponent<T>();
        Debug.Log("������ �Ϸ�Ǿ����ϴ�!");
        text.text = $"Score: {score}";
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
            score += 10;
            other.gameObject.SetActive(false);
            text.text = $"Score: {score}";
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            speed/=1.5f;
            other.gameObject.SetActive(false);
        }
    }

}
