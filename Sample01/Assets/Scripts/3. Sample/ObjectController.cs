using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public float speed = 3;
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        if (player != null)
        {
            speed += player.GetComponent<SkeletonController>().score / 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -2)
        {
            // If the object falls below a certain height, destroy it
            Destroy(gameObject);
            if (player != null)
            {
                player.GetComponent<SkeletonController>().IncreaseScore();
            }
        }
        if (player != null)
        {
            // 원에 의한 충돌 판정 로직 사용
            Vector3 v1 = transform.position;
            // If the distance between the object and the player is less than 0.5, destroy the object
            Vector3 v2 = player.transform.position;
            Vector3 diff = v1 - v2; // Difference vector between the object and the player
            float dist = diff.magnitude; // Calculate the distance between the object and the player
            float obj_r1 = 0.5f; // Radius of the object
            float obj_r2 = 1.0f; // Radius of the player
            // If the distance is closer than the sum of the radii, destroy the object
            if (dist < obj_r1 + obj_r2)
            {
                Destroy(gameObject);
                if (player != null) {
                    player.GetComponent<SkeletonController>().DecreaseScore();
                }
            }
        }
    }
}
