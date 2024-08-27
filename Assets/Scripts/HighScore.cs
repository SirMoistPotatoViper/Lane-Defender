/*****************************************************************************
// File Name : HighScore.cs
// Author : Jake Slutzky
// Creation Date : August 23, 2024
//
// Brief Description : This script controls the highScore of the player and ensures it saves over multiple runs
*****************************************************************************/
using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    /// <summary>
    /// The Variables that the HighScore script refernces
    /// </summary>
    [SerializeField] private static HighScore instance;
    [SerializeField] private GameObject HighScoreTextObject;
    [SerializeField] private GameManager GM;
    [SerializeField] private TMP_Text HighScoreText;
    [SerializeField] private float enclosedHighScoreNumber = 0;
    [SerializeField] private TMP_Text deathHighScore;
    [SerializeField] private GameObject deathHighScoreObject;
    [SerializeField] private GameObject congratsNewHighScore;
    private float roundScore = 0;

    /// <summary>
    /// Ensures the script and attached gameObjects aren't deleted between sceneloads
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// On start, get the "HighScore" float from the player Prefs, set the enclosedHighScoreNumber to "HighScore",
    /// set the roudScore to zero, turn off the "congrats on a new highscore" object, find the "GameManager" script,
    /// and set the HighScore text to the "HighScore" player pref.
    /// </summary>
    void Start()
    {
        PlayerPrefs.GetFloat("HighScore");
        enclosedHighScoreNumber = PlayerPrefs.GetFloat("HighScore");
        roundScore = 0;
        //newHighScore = false;
        congratsNewHighScore.SetActive(false);
        if (GM == null)
        {
            GM = FindObjectOfType<GameManager>();
        }
        //HighScoreText.text = "HighScore: " + enclosedHighScoreNumber;
        HighScoreText.text = $"HighScore: {PlayerPrefs.GetFloat("HighScore", 0)}";
        
        
    }

    /// <summary>
    /// Every Update frame, check the HighScore, set the "deathHighScore" popup to display the enclosedHighScore, set 
    /// the roundScore to the GameManager's score (to make a more local version in case), 
    /// find the GameManager again just in case, If the GM.Score is bigger then the enclosedHighScore, set the enclosed
    /// to the GM.Score and update the HighScore text. If the PlayerHealth, is less then or equal to zero, set the
    /// deathHighscore active. If the roundScore is bigger then or equal to the enclosedHighscore, set the congrats
    /// to active as well.
    /// </summary>
    void Update()
    {
        checkHighScore();
        deathHighScore.text = "HighScore: " + enclosedHighScoreNumber;
        roundScore = GM.Score;

        if (GM == null)
        {
            GM = FindObjectOfType<GameManager>();
        }

        if (GM != null && GM.Score >= enclosedHighScoreNumber)
        {
            HighScoreText.text = "HighScore: " + GM.Score;
            enclosedHighScoreNumber = GM.Score;
        }
        if (GM.PlayerHealth <= 0)
        {
            deathHighScoreObject.SetActive(true);
            if (roundScore >= enclosedHighScoreNumber) 
            { 
                congratsNewHighScore.SetActive(true);
            }
            else
            {
                congratsNewHighScore.SetActive(false);
            }
        }
        else if (GM.PlayerHealth >= 0)
        {
            deathHighScoreObject.SetActive(false);
            congratsNewHighScore.SetActive(false);
        }
    }

    /// <summary>
    /// When CheckHighScore is triggered, if the enclosedHighScoreNumber is greater then the PlayerPrefs  HighScore,
    /// Set the PlayerPrefs highscore to the enclosedHighScoreNumber
    /// </summary>
    void checkHighScore()
    {
        if(enclosedHighScoreNumber > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", enclosedHighScoreNumber);
        }
    }
}

