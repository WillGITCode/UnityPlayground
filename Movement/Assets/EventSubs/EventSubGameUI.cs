using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubGameUI : MonoBehaviour
{
    EventSubPlayer player;
    float playerHealth;

    void Start()
    {
        player = FindObjectOfType<EventSubPlayer>();
        playerHealth = player.health;
        player.OnPlayerDeath += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            DrawHealthBar(player.health);
        }
    }

    void DrawHealthBar(float health)
    {
        if (health != playerHealth)
        {
            playerHealth = health;
            Debug.Log("Player health: " + playerHealth);
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
    }
}
