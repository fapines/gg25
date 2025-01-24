using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 20f;
    public int damage = 0;
    public float destroyAfterSecs = 10f; // time to live in secs
    
    protected SpriteRenderer spriteRenderer;
    protected Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, destroyAfterSecs);
    }


    public void Fire(Vector2 direction)
    {
        
        rb.velocity = direction * speed;
    }

}
