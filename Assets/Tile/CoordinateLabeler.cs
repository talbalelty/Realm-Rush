using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.black;
    [SerializeField] Color blockedColor = Color.red;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;
    bool toggleLabels = false;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.color = defaultColor;
        label.enabled = false;
        waypoint = GetComponentInParent<Waypoint>();
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
        if (waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text = $"({coordinates.x},{coordinates.y})";
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
