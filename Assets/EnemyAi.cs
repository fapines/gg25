using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f; // Movement speed of the enemy
    public float detectionRange = 10f; // Range at which the enemy can detect the player
    public float attackRange = 1.5f; // Range at which the enemy can attack the player
    public int health = 50; // Enemy health
    public LayerMask platformLayer; // Layer for the floating platforms
    public float jumpForce = 10f; // Jump force to reach the floating platform
    public float gravityScale = 3f; // Gravity scale for controlling the fall speed

    // Add jump detection range adjustable from the Inspector
    public float jumpDetectionRange = 5f; // Range for detecting platforms for jumping

    private Transform player; // Reference to the player
    private Rigidbody2D rb; // Reference to the enemy's Rigidbody2D
    private bool isDead = false; // Flag to check if the enemy is dead

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform; // Assuming player has the "Player" tag
        rb = GetComponent<Rigidbody2D>(); // Get Rigidbody2D component for movement
        rb.gravityScale = gravityScale; // Adjust gravity scale
    }

    private void Update()
    {
        if (isDead) return; // If the enemy is dead, stop updating

        // Calculate distance between the player and enemy
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Check if the player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            // Detect platforms and decide whether to jump
            DetectPlatformsAndDecideMovement();

            // If within attack range, attack (this can be expanded)
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
        }
        else
        {
            Idle();
        }
    }

    private void DetectPlatformsAndDecideMovement()
    {
        // Raycast to detect if there's a platform in front of the enemy within the jumpDetectionRange
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, jumpDetectionRange, Vector2.zero, 0f, platformLayer);

        if (hit.collider != null)
        {
            // Platform is detected, check distance and decide jump behavior
            Vector2 platformPosition = hit.collider.transform.position;
            float distanceToPlatform = Vector2.Distance(transform.position, platformPosition);
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Check if the platform is reachable and decide to jump
            if (distanceToPlatform < distanceToPlayer && platformPosition.y > transform.position.y)
            {
                // Prioritize jumping to the platform
                JumpToPlatform(hit.collider.transform);
            }
            else
            {
                // Chase player on the ground
                MoveTowardsPlayer();
            }
        }
        else
        {
            // If no platform is detected, chase player on the ground
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        // Get direction to player and move horizontally
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y); // Only adjust the x-axis for movement
    }

    private void JumpToPlatform(Transform platform)
    {
        // Calculate the distance to the platform and make sure it's a valid jump
        float platformHeight = platform.position.y - transform.position.y;
        if (platformHeight > 0f && platformHeight < 4f) // Ensure the platform is within jumping reach
        {
            // Adjust velocity to make the jump more controlled
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Apply vertical jump force
        }
    }

    private void AttackPlayer()
    {
        // Placeholder for attack logic (e.g., deal damage, play attack animation, etc.)
    }

    private void Idle()
    {
        // Placeholder for idle behavior (e.g., standing still, patrolling, etc.)
        rb.velocity = Vector2.zero; // Stop moving
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        // Add death logic here, such as playing death animation, destroying the object, etc.
        Destroy(gameObject); // Destroy the enemy GameObject
    }

    // Draw Gizmos for detection and jump ranges
    private void OnDrawGizmosSelected()
    {
        // Draw detection range as a wire sphere
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw jump detection range as a wire sphere
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, jumpDetectionRange); // Adjustable jump detection range
    }
}
