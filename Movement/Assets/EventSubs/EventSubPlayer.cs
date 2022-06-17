using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventSubPlayer : MonoBehaviour
{
    public float health = 10;
    public float speed = 10;
    public event Action OnPlayerDeath;

    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if(health <= 0)
        {
            if(OnPlayerDeath != null)
            {
                OnPlayerDeath();
                Destroy(gameObject);
            }
        }

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 direction = input.normalized;
        Vector3 velocity = direction * speed;
        Vector3 moveAmount = velocity * Time.deltaTime;

        transform.position += moveAmount;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Player hit " + collision.gameObject.name);
            health -= 1;
        }
    }
}
