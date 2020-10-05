using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    [SerializeField] Text _currentScoreTextView;

    int _currentScore;

    // Update is called once per frame
    void Update()
    {
        //Increase Score
        //TODO replace with real implentation later
        if(Input.GetKeyDown(KeyCode.Q))
        {
            IncreaseScore(5);
        }
        //Exit level
        //TODO bring up pop up menu for navigation
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ExitLevel();
        }
    }

    private void IncreaseScore(int scoreIncrease)
    {
        //increase score
        _currentScore += scoreIncrease;
        //update score display, so we can see the new score
        _currentScoreTextView.text = "Score: " + _currentScore.ToString();
    }

    private void ExitLevel()
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
}
