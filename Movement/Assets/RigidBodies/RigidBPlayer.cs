using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBPlayer : MonoBehaviour
{
    public float speed = 6f;
    Vector3 velocity;
    Rigidbody myRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 direction = input.normalized;
        velocity = direction * speed;
    }

    void FixedUpdate()
    {
        myRigidbody.position += velocity * Time.deltaTime;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
        }
    }
}
