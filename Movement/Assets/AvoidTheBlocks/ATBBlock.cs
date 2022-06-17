using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATBBlock : MonoBehaviour
{
    public float speed = 7;
    public FallAngle fallAngle;
    float screenBottomBorderInWorldUnits;

    void Start()
    {
        screenBottomBorderInWorldUnits = -Camera.main.orthographicSize - transform.localScale.y;
    }

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (fallAngle == FallAngle.left)
        {
            transform.Translate(Vector2.left * (speed / 5) * Time.deltaTime);
        }
        else if (fallAngle == FallAngle.right)
        {
            transform.Translate(Vector2.right * (speed / 5) * Time.deltaTime);
        }

        if (transform.position.y < screenBottomBorderInWorldUnits)
        {
            Destroy(gameObject);
        }
    }
}
