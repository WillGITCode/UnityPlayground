using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpinner : MonoBehaviour
{
    public float speed = 1;

    void Update()
    {
       transform.Rotate(0, speed, 0 * Time.deltaTime);
    }
}
