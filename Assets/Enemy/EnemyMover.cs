using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] float stepSpeed = 1f;
    
    List<Node> path = new List<Node>();
    Enemy enemy;
    Vector2Int coordinates;
    Vector3 startPosition;
    Vector3 endPosition;
    float travelPercent;

    GridManager gridManager;
    Pathfinder pathfinder;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RecalculatePath(bool resetPath)
    {
        if (resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            startPosition = transform.position;
            endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            travelPercent = 0f;
            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * stepSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);

                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }

    void FinishPath()
    {
        enemy.Penalty();
        gameObject.SetActive(false);
    }
}
