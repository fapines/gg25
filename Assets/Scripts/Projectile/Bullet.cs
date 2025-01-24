using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets._2D;

public class Bullet : Projectile
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        PlatformerCharacter2D enemy = other.GetComponent<PlatformerCharacter2D>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
