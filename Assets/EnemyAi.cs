using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 10f;
    public float attackRange = 1.5f;
    public int health = 50;
    public LayerMask platformLayer;
    public float jumpForce = 10f;
    public float gravityScale = 3f;
    public float jumpDetectionRange = 5f;

    private Transform player;
    private Rigidbody2D rb;
    private bool isDead = false;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
    }

    private void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            DetectPlatformsAndDecideMovement();

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
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, jumpDetectionRange, Vector2.zero, 0f, platformLayer);

        if (hit.collider != null)
        {
            Vector2 platformPosition = hit.collider.transform.position;
            float distanceToPlatform = Vector2.Distance(transform.position, platformPosition);
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlatform < distanceToPlayer && platformPosition.y > transform.position.y)
            {
                JumpToPlatform(hit.collider.transform);
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
    }

    private void JumpToPlatform(Transform platform)
    {
        float platformHeight = platform.position.y - transform.position.y;
        if (platformHeight > 0f && platformHeight < 4f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void AttackPlayer()
    {
    }

    private void Idle()
    {
        rb.velocity = Vector2.zero;
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
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, jumpDetectionRange);
    }
}
