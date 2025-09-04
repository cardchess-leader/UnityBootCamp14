using UnityEngine;

public class Nuke : MonoBehaviour
{
    // Define a explosion vfx field
    [SerializeField]
    GameObject explosionVFX;
    [SerializeField]
    float explosionRadius = 10f;

    // When colliding with another object, destroy itself and play explosionVFX
    private void OnCollisionEnter(Collision collision)
    {
        // Instantiate the explosion VFX at the current position and rotation
        if (explosionVFX != null)
        {
            Instantiate(explosionVFX, transform.position, transform.rotation);
        }
        // Destroy the nuke object
        Destroy(gameObject);

        // Also destroy all enemies within the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                Destroy(hitCollider.gameObject);
            }
        }
    }
}
