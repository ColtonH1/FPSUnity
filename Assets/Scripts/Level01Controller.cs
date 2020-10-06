using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;
    [SerializeField] Text _currentHealthTextView;

    //pause menu
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject deathMenuUI;

    //Health
    static int maxHealth = 100;
    PlayerHealth playerHealth = new PlayerHealth(maxHealth);
    public HealthBar healthBar;
    //public Slider slider;

    int _currentScore;

    private bool isDead = false;

    private void Start()
    {
        Debug.Log("Health: " + playerHealth.GetHealth());
        healthBar.SetMaxHealth(maxHealth);
        //SetSlider();
        Resume();
    }



    // Update is called once per frame
    void Update()
    {
        //Increase Score
        //TODO replace with real implentation later
        DebugMethods();

        //display score
        _currentScoreTextView.text = "Score: " + _currentScore.ToString();

        //update health
        _currentHealthTextView.text = "Health: " + playerHealth.GetHealth().ToString();
        //SetHealth(playerHealth.GetHealth());
        if(playerHealth.GetHealth() == 0)
        {
            Die();
        }

        //pause menu
        EscPressed();
    }  

    private void DebugMethods()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IncreaseScore(5);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TakeDamage(5);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            playerHealth.Heal(5);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Health: " + playerHealth.GetHealth());
        }
    }

    private void EscPressed()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isDead)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void IncreaseScore(int scoreIncrease)
    {
        //increase score
        _currentScore += scoreIncrease;
        //update score display, so we can see the new score
        _currentScoreTextView.text = "Score: " + _currentScore.ToString();
    }

    public void ExitLevel()
    {
        //compare score to high score
        int highScore = PlayerPrefs.GetInt("HighScore");
        if(_currentScore > highScore)
        {
            //save current score as high score
            PlayerPrefs.SetInt("HighScore", _currentScore);
            Debug.Log("New high score: " + _currentScore);
        }
        //load new level
        SceneManager.LoadScene("MainMenu");
    }

    //pause menu functions
    public void Resume()
    {        
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitButton()
    {
        GameIsPaused = false;
        ExitLevel();
    }

    public bool IsPaused()
    {
        return GameIsPaused;
    }

    //health functions
    /*private void SetSlider()
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }*/

    public void TakeDamage(int damage)
    {
        playerHealth.Damage(5);
        healthBar.SetHealth(playerHealth.GetHealth());
    }

    private void Die()
    {
        isDead = true;
        deathMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
