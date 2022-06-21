using UnityEngine;
using System;

public class CoinSpinner : MonoBehaviour
{
    public event Action OnGameWon;
    public float speed = 1;

    void Update()
    {
        transform.Rotate(0, speed, 0 * Time.deltaTime);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (OnGameWon != null)
            {
                OnGameWon();
            }
        }
    }
}
