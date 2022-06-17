using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverScreen;
    bool gameOver;

    void Start()
    {
        FindObjectOfType<ATBPlayer>().OnPlayerDeath += OnGameOver;
    }

    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void OnGameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
    }
}
