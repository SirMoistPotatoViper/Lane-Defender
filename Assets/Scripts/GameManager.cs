/*****************************************************************************
// File Name : GameManager.cs
// Author : Jake Slutzky
// Creation Date : August 21, 2024
//
// Brief Description : This script controls the various scores and health of the player in the game.
*****************************************************************************/
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// The Variables that the GameManager script refernces
    /// </summary>
    [SerializeField] private GameObject playerTank;
    [SerializeField] private GameObject backWall;
    [SerializeField] private int playerHealth;
    [SerializeField] private float score;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text deathScore;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private GameObject deathUI;
    [SerializeField] private GameObject deathSpawners;
  

    public int PlayerHealth { get => playerHealth; set => playerHealth = value; }
    public float Score { get => score; set => score = value; }

    /// <summary>
    /// Every update frame, updates the health, score, and death score to be accurate. If the playerHealth is less then
    /// or equal to 0, turn on deathUI and turn off the spawners.
    /// </summary>
    void Update()
    {
        
        healthText.text = "Health: " + playerHealth;
        scoreText.text = "Score: " + score;
        deathScore.text = "Score: " + score;

        if (playerHealth <= 0)
        {
            deathUI.SetActive(true);
            deathSpawners.SetActive(false);
        }
    }

    /// <summary>
    /// When restart is triggered, reload the scene
    /// </summary>
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// When quit is triggered, quit the game
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
