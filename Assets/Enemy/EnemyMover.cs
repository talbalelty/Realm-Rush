using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float stepSpeed = 1f;
    [SerializeField] List<Waypoint> path = new List<Waypoint>();

    Vector3 startPosition;
    Vector3 endPosition;
    float travelPercent;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            startPosition = transform.position;
            endPosition = waypoint.transform.position;
            travelPercent = 0f;
            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * stepSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
