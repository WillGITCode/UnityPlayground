using UnityEngine;
using System;

public class ATBPlayer : MonoBehaviour
{
    public event Action OnPlayerDeath;
    public float speed = 10;
    float screenHalfWidthInWorldUnits;

    void Start()
    {
        float halfPlayerWidth = transform.localScale.x / 2;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "ATBBlock")
        {
            if(OnPlayerDeath != null)
            {
                OnPlayerDeath();
            }
            Destroy(gameObject);
        }
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float velocity = inputX * speed;
        transform.Translate(Vector2.right * velocity * Time.deltaTime);

        if (transform.position.x < -screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(screenHalfWidthInWorldUnits, transform.position.y);
        }

        if (transform.position.x > screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(-screenHalfWidthInWorldUnits, transform.position.y);
        }
    }
}
