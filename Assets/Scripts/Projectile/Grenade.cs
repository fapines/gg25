using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityStandardAssets._2D;

public class Grenade : Projectile
{
    public GameObject explosion;
    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
