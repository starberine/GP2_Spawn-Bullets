using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed = 10f; // Speed of the bullet

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the bullet hits an enemy
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit by bullet!");
            Destroy(other.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
