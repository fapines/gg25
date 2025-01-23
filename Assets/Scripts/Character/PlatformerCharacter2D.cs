using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [SerializeField] private float m_MaxSpeed = 10f; // Max speed in the x axis
        [SerializeField] private float m_JumpForce = 400f; // Jump force
        [Range(0, 1)][SerializeField] private float m_CrouchSpeed = .36f; // Crouch speed multiplier
        [SerializeField] private bool m_AirControl = false; // Allow air control?
        [SerializeField] private LayerMask m_WhatIsGround; // Ground layer mask

        private Transform m_GroundCheck; // Ground check position
        private bool m_Grounded; // Is the character grounded?
        private Transform m_CeilingCheck; // Ceiling check position
        private bool m_FacingRight = true; // Is the character facing right?
        private Rigidbody2D m_Rigidbody2D; // Rigidbody2D component

        public GameObject bulletPrefab; // Bullet prefab
        public GameObject grenadePrefab; // Grenade prefab
        public Transform weaponLocation; // Weapon location
        public Vector2 aimingDirection { get; set; } // Direction for aiming

        public float fireRate = 0.5f; // Fire rate for shooting
        private float nextFire = 0.0f; // Time of next allowed fire
        public int health = 100; // Player health

        private void Awake()
        {
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            m_Grounded = Physics2D.OverlapCircle(m_GroundCheck.position, 0.2f, m_WhatIsGround);
        }

        public void Move(float move, bool crouch, bool jump)
        {
            if (m_Grounded || m_AirControl)
            {
                // Move character horizontally
                m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

                // Flip character based on movement direction
                if (move > 0 && !m_FacingRight)
                {
                    Flip();
                }
                else if (move < 0 && m_FacingRight)
                {
                    Flip();
                }
            }

            if (m_Grounded && jump)
            {
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce)); // Apply jump force
            }
        }

        private void Flip()
        {
            m_FacingRight = !m_FacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        public void shoot(bool shoot)
        {
            if (shoot && Time.time > nextFire)
            {
                if (aimingDirection == Vector2.zero)
                {
                    aimingDirection = new Vector2(1, 0); // Default aiming direction
                }

                if (aimingDirection.x > 0 && !m_FacingRight)
                {
                    Flip();
                }
                else if (aimingDirection.x < 0 && m_FacingRight)
                {
                    Flip();
                }

                nextFire = Time.time + fireRate;
                GameObject bullet = Instantiate(bulletPrefab, weaponLocation.position, weaponLocation.rotation);
                bullet.GetComponent<Bullet>().Fire(aimingDirection); // Fire the bullet in the direction of the mouse
            }
        }

        public void shootGrenade(bool shootGrenade)
        {
            if (shootGrenade && Time.time > nextFire)
            {
                if (aimingDirection.x > 0 && !m_FacingRight)
                {
                    Flip();
                }
                else if (aimingDirection.x < 0 && m_FacingRight)
                {
                    Flip();
                }

                nextFire = Time.time + fireRate;
                GameObject grenade = Instantiate(grenadePrefab, weaponLocation.position, weaponLocation.rotation);
                grenade.GetComponent<Grenade>().Fire(aimingDirection); // Fire the grenade in the direction of the mouse
            }
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
            Destroy(gameObject); // Destroy the character
        }
    }
}
