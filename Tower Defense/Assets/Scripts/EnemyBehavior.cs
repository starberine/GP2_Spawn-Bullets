using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed at which the enemy moves
    public int damage = 1; // Damage dealt to the player's cannon area
    public float floatHeight = 0.5f; // How much the enemy floats when affected
    public Color flashColor = Color.red; // Color to change when hit
    public float effectDuration = 2f; // Duration for the effects (stun/freeze, float, color change)

    private Color originalColor; // Original color of the enemy

    void Start()
    {
        originalColor = GetComponent<Renderer>().material.color; // Save the original color
    }

    void Update()
    {
        // Move the enemy forward (toward the player's area)
        transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime); // Moves along global Z-axis
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check collision with a bullet
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Enemy hit by bullet!");

            Destroy(other.gameObject); // Destroy the bullet

            // Apply a random effect to the enemy
            int randomEffect = Random.Range(0, 3); // 0 = Stun, 1 = Float, 2 = Color change

            if (randomEffect == 0)
            {
                StartCoroutine(StunEffect());
            }
            else if (randomEffect == 1)
            {
                StartCoroutine(FloatEffect());
            }
            else if (randomEffect == 2)
            {
                StartCoroutine(ColorChangeEffect());
            }
        }
    }

    // Stun effect (freeze movement for a short duration)
    private IEnumerator StunEffect()
    {
        moveSpeed = 0f; // Stop the enemy's movement
        yield return new WaitForSeconds(effectDuration); // Wait for the effect duration
        moveSpeed = 5f; // Reset the enemy's movement speed
    }

    // Float effect (makes the enemy float in place for a short duration)
    private IEnumerator FloatEffect()
    {
        Vector3 originalPosition = transform.position;
        float targetHeight = originalPosition.y + floatHeight; // Target height to float to
        float elapsedTime = 0f;

        // Make the enemy float up
        while (elapsedTime < effectDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, new Vector3(originalPosition.x, targetHeight, originalPosition.z), (elapsedTime / effectDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Reset the enemy's position
        transform.position = originalPosition;
    }

    // Color change effect (changes the color to red for a short duration)
    private IEnumerator ColorChangeEffect()
    {
        Renderer enemyRenderer = GetComponent<Renderer>();
        enemyRenderer.material.color = flashColor; // Change the color to red
        yield return new WaitForSeconds(effectDuration); // Wait for the effect duration
        enemyRenderer.material.color = originalColor; // Reset the color back to original
    }
}