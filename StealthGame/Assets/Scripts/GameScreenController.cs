using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScreenController : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject gameWonScreen;
    bool gameOver;
    bool gameWon;

    void Start()
    {
        InitGuards();
        InitGameWonObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver || gameWon)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void InitGuards()
    {
        var guards = FindObjectsOfType<Guard>();
        foreach (var guard in guards)
        {
            guard.OnDetectedPlayer += OnDetectedPlayer;
        }
    }

    void InitGameWonObject()
    {
        var gameWonObject = FindObjectOfType<CoinSpinner>();
        gameWonObject.OnGameWon += OnGameWon;
    }

    void OnGameWon()
    {
        if (!gameOver)
        {
            gameWonScreen.SetActive(true);
            gameWon = true;
        }
    }

    void OnDetectedPlayer()
    {
        if (!gameWon)
        {
            gameOverScreen.SetActive(true);
            gameOver = true;
        }
    }
}
