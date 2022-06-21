using System.Collections;
using UnityEngine;
using System;

public class Guard : MonoBehaviour
{
    public event Action OnDetectedPlayer;
    public Transform pathHolder;
    public LayerMask viewMask;
    public float playerDetectionTime = 0.5f;
    public float walkSpeed = 5;
    public float turnSpeed = 5;
    public float waitTime = 0.3f;
    public Light spotLight;
    public float viewDistance;


    Color originalSpotLightColor;
    IEnumerator currentCoroutine;
    IEnumerator detectingPlayerRoutine;
    Vector3[] waypoints;
    float viewAngle;
    int currentWaypointIndex = 0;
    float timer = 0.0f;
    Transform player;

    void Start()
    {
        originalSpotLightColor = spotLight.color;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotLight.spotAngle;
        InitWayPoints();
        currentCoroutine = FollowPath(waypoints);
        StartCoroutine(currentCoroutine);
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            if (detectingPlayerRoutine == null)
            {
                detectingPlayerRoutine = DetectPlayer();
                StartCoroutine(detectingPlayerRoutine);
            }
        }
        else
        {
            if (detectingPlayerRoutine != null)
            {
                StopCoroutine(detectingPlayerRoutine);
                timer = 0.0f;
                spotLight.color = originalSpotLightColor;
            }
            detectingPlayerRoutine = null;
        }
    }

    void InitWayPoints()
    {
        waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }
    }

    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator DetectPlayer()
    {

        while (CanSeePlayer())
        {
            spotLight.color = Color.red;
            timer += Time.deltaTime;
            if (timer > playerDetectionTime)
            {
                timer = 0.0f;
                if (OnDetectedPlayer != null)
                {
                    OnDetectedPlayer();
                }
            }
            yield return null;
        }
    }

    IEnumerator FollowPath(Vector3[] waypoints)
    {
        while (true)
        {
            currentCoroutine = Move(waypoints[currentWaypointIndex], walkSpeed);
            yield return new WaitForSeconds(waitTime);
            yield return StartCoroutine(TurnTowardsTarget(waypoints[currentWaypointIndex], turnSpeed));
            yield return StartCoroutine(currentCoroutine);
            currentWaypointIndex = (currentWaypointIndex + 1 >= waypoints.Length) ? 0 : currentWaypointIndex + 1;
        }
    }

    IEnumerator TurnTowardsTarget(Vector3 destination, float speed)
    {
        Quaternion lookRotation = Quaternion.LookRotation(destination - transform.position);
        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * speed;
            yield return null;
        }
    }

    IEnumerator Move(Vector3 destination, float speed)
    {
        while (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, 0.3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}
