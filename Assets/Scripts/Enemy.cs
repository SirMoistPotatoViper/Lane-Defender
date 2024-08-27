/*****************************************************************************
// File Name : Enemy.cs
// Author : Jake Slutzky
// Creation Date : August 21, 2024
//
// Brief Description : This script effects the movement and animation of the enemies
*****************************************************************************/
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /// <summary>
    /// The Variables that the Enemy script refernces
    /// </summary>
    [SerializeField] private float Health = 5f;
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float haltTime = 5f;
    [SerializeField] private float deathTime = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameManager GM;
    [SerializeField] private AudioClip hurtNoise;
    [SerializeField] private AudioClip dieNoise;
    [SerializeField] private AudioClip lifeloseNoise;
    Animator m_Animator;

    /// <summary>
    /// On start, the Enemy will find it's animator and the GameManager
    /// </summary>
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        GM = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// Every frame, the Enemy will check if it's health is greater then 0, or less than or equal to 0. This will
    /// effect if it is moving forward or stops moving.
    /// </summary>
    void Update()
    {
        if (Health > 0) 
        {
            rb.velocity = new Vector2(-Speed, rb.velocity.y);
        }
        else if (Health <= 0) 
        {
            rb.velocity = new Vector2(0, 0);
            
        }
    }

    /// <summary>
    /// On trigger, if colliding with a bullet, take damage, play a sound, and start Coroutine "bulletStop"
    /// If colliding with the player, play life lose sound, 
    /// minus one "PlayerHealth" from the GM script, and destroy self
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            AudioSource.PlayClipAtPoint(hurtNoise, transform.position);
            Health -= 1;
            StartCoroutine(bulletStop());
        }
        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(lifeloseNoise, transform.position);
            GM.PlayerHealth -= 1;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// If it collides with the player, play lifeLose noise, 
    /// The GM PlayerHealth loses one point, and it destroys itself
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(lifeloseNoise, transform.position);
            GM.PlayerHealth -= 1;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// The bulletStop IEnumerator will cause the enemy to be hurt, slow speed, and if the health is less then or
    /// equal to zero, play dieNoise, raise GM score, play dead animation, wait "deathTime", and then destroy self.
    /// Otherwise, wait a second and then set the speed to what it used to be.
    /// </summary>
    private IEnumerator bulletStop()
    {
        m_Animator.SetTrigger("hurt");
        Speed = 0;
        if (Health <= 0)
        {
            AudioSource.PlayClipAtPoint(dieNoise, transform.position);
            GM.Score += 300;
            m_Animator.SetBool("dead", true);
            yield return new WaitForSeconds(deathTime);
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(haltTime);
        if (Health > 0) 
        {
            Speed = maxSpeed;
        }
    }
}
