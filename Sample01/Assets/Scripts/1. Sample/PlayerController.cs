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
        Debug.Log("설정이 완료되었습니다!");
        text.text = $"Score: {score}";
    }

    // Update is called once per frame
    void Update()
    {
        // 수평 이동
        float horizontal = Input.GetAxis("Horizontal");
        // 수직 이동
        float vertical = Input.GetAxis("Vertical");
        // 이동 좌표(벡터) 설정
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
