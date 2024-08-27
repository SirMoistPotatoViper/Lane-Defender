/*****************************************************************************
// File Name : Bullet.cs
// Author : Jake Slutzky
// Creation Date : August 21, 2024
//
// Brief Description : This script effects the movement and animation of the bullet
*****************************************************************************/
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// The Variables the bullet uses
    /// </summary>
    [SerializeField] private Rigidbody2D rb;
    private float bulletSpeed = 10f;
    [SerializeField] private Animator animator;
    [SerializeField] private float bulletExplodeTime;

    /// <summary>
    /// Every frame, the bullet has it's x velocity set to "bulletSpeed"
    /// </summary>
    void Update()
    {
        rb.velocity = new Vector2(bulletSpeed, rb.velocity.y);
    }

    /// <summary>
    /// If the bullet collides with an enemy (either trigger or collision), it will begin to animate the bullet 
    /// explosion, stop the bullet from moving, and start the "bulletBreak" Coroutine
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            gameObject.GetComponent<Animator>().enabled = true;
            bulletSpeed = 0;
            StartCoroutine(bulletBreak());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "Enemy")
            {
                gameObject.GetComponent<Animator>().enabled = true;
                bulletSpeed = 0;
                StartCoroutine(bulletBreak());
            }
    }

    /// <summary>
    /// After "bulletExplodeTime" seconds, the bullet will be destroyed
    /// </summary>
    private IEnumerator bulletBreak()
    {
        yield return new WaitForSeconds(bulletExplodeTime);
        Destroy(gameObject);
    }
}
