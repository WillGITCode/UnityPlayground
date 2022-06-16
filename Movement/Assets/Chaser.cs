using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    public Transform target;
    public float speed = 7;

    void Update()
    {
        Vector3 displacementFromTarget = target.position - transform.position;
        Vector3 directionToTarget = displacementFromTarget.normalized;
        Vector3 velocity = directionToTarget * speed;

        float distanceToTarget = displacementFromTarget.magnitude;

        if (distanceToTarget > 1f)
        {
            transform.Translate(velocity * Time.deltaTime);
        }
    }
}
