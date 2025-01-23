using UnityEngine;
using UnityStandardAssets._2D;

public class Explosion : MonoBehaviour
{
    public int damage;
    public float destroyAfterSecs = 3f;
    private void Start()
    {
        Destroy(gameObject, destroyAfterSecs);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        PlatformerCharacter2D enemy = other.GetComponent<PlatformerCharacter2D>();
        RaycastHit2D hitWhat = Physics2D.Raycast(transform.position, other.offset);
        if (enemy = null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
        if (hitWhat)
        {
            Debug.Log(hitWhat.transform.name);
        }
    }

    /* private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 4);
    }*/
}
