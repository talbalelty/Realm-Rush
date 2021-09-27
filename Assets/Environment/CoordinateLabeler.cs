using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

// Move to Editor Folder before Building
[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color blockedColor = Color.red;
    [SerializeField] Color pathColor = Color.blue;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color defaultColor = Color.white;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
    bool toggleLabels = false;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.color = defaultColor;
        label.enabled = false;
        
        DisplayCoordinates();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnToggleTileLabels(InputValue value)
    {
        toggleLabels = value.isPressed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        ToggleLabels();
        SetLabelColor();
    }

     void ToggleLabels()
    {
        if (toggleLabels)
        {
            label.enabled = !label.IsActive();
            toggleLabels = false;
        }
    }

    void SetLabelColor()
    {
        if (gridManager == null)
        {
            return;
        }

        Node node = gridManager.GetNode(coordinates);
        if (node == null)
        {
            return;
        }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }

    void DisplayCoordinates()
    {
        if (gridManager == null)
        {
            return;
        }
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        label.text = $"({coordinates.x},{coordinates.y})";
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
