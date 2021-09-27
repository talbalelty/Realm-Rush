using UnityEngine;
using UnityEngine.InputSystem;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable = true;
    [SerializeField] Tower tower;

    Vector2 pointerPosition;
    Ray ray;
    RaycastHit hit;
    bool isClicked = false;

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();
    Node node;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void OnPoint(InputValue value)
    {
        pointerPosition = value.Get<Vector2>();
    }

    void OnClick(InputValue value)
    {
        isClicked = value.isPressed;
    }

    void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void Update()
    {
        ProcessClicking();
    }

    void ProcessClicking()
    {
        if (isClicked)
        {
            node = gridManager.GetNode(coordinates);
            if (node != null)
            {
                if (node.isWalkable && !pathfinder.WillBlockPath(coordinates))
                {
                    ProcessPointing();
                }
            }
            isClicked = false;
        }
    }

    void ProcessPointing()
    {
        bool isSuccessful;
        ray = Camera.main.ScreenPointToRay(pointerPosition); // Taking camera position and a Vector2 to cast a Ray.
        // hit pointer is the gameObject the ray hits
        if (Physics.Raycast(ray, out hit))
        {
            // if is true when the hit GameObject equals this script's GameObject (the Tile)
            if (gameObject.Equals(hit.transform.gameObject))
            {
                isSuccessful = tower.CreateTower(tower, transform.position);
                if (isSuccessful)
                {
                    gridManager.BlockNode(coordinates);
                    pathfinder.NotifyReceivers();
                }
            }
        }
    }

    public bool IsPlaceable
    {
        get
        {
            return isPlaceable;
        }
    }
}
