/*****************************************************************************
// File Name : Player.cs
// Author : Jake Slutzky
// Creation Date : August 21, 2024
//
// Brief Description : This script effects the players movement and bullet shooting
*****************************************************************************/
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    /// <summary>
    /// The Variables that the Player script refernces
    /// </summary>
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float tankSpeed;
    [SerializeField] private Bullet bulletPrefab;
    private float vertical;
    [SerializeField] private GameManager GM;
    [SerializeField] private GameObject tankExplosion;
    private bool restartPressed;
    private bool canShoot = true;
    private bool holdingShoot;
    [SerializeField] private float shotDelay;
    [SerializeField] private float explosionTime;


    /// <summary>
    /// On start, find the GameManager script
    /// </summary>
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
    }

    /// <summary>
    /// Every update frame, the tank velocity will move up or down in order to be accurate to what direction is pressed.
    /// if holdingShoot and canShoot is true, the "shooting" 
    /// couroutine starts. If player health is less then 0, destroy self
    /// </summary>
    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, vertical * tankSpeed);
        if (holdingShoot == true && canShoot == true)
        {
            StartCoroutine(shooting());
        }

        if (GM.PlayerHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// if move is triggered, set the "vertical" variable to -1 or 1.
    /// </summary>
    public void Move(InputAction.CallbackContext context)
    {
        vertical = context.ReadValue<Vector2>().y;
    }

    /// <summary>
    /// If restart is triggered, cehck if restart has already been pressed, then reload the scene.
    /// </summary>
    public void Restart(InputAction.CallbackContext context)
    {
        if (restartPressed == false)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            restartPressed = true;
        }
    }

    /// <summary>
    /// When quit is triggered, quit the application
    /// </summary>
    public void Quit(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    /// <summary>
    /// When shoot is triggered, turn the "holding shoot" bool to true. When you let go of it, holding shoot is false.
    /// </summary>
    public void Shoot(InputAction.CallbackContext context)
    {
       
        if (context.started)
        {
            holdingShoot = true;
            //Debug.Log("Firing");
        }
        if (context.canceled)
        {
            holdingShoot = false;
            //Debug.Log("Not firing");
        }
    }

    /// <summary>
    /// When shooting IEnumerator is triggered, turn on the tankExplosion, spawn the bullet prefab, turn canShoot off,
    /// wait for "explosionTime" before hiding the tank explosion, then wait for "shotDelay" before turning on canShoot.
    /// </summary>
    private IEnumerator shooting()
    {
        //Debug.Log("Firing");
        tankExplosion.SetActive(true);
        Bullet bullet = Instantiate(bulletPrefab, tankExplosion.transform.position, quaternion.identity); 
        canShoot = false;
        yield return new WaitForSeconds(explosionTime);
        tankExplosion.SetActive(false);
        yield return new WaitForSeconds(shotDelay);
        canShoot = true;

    }
}
