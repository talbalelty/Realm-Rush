using UnityEngine;
using UnityEngine.InputSystem;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceable = true;
    [SerializeField] GameObject tower;

    Vector2 pointerPosition;
    Ray ray;
    RaycastHit hit;
    bool isClicked = false;

    void OnPoint(InputValue value)
    {
        pointerPosition = value.Get<Vector2>();
    }

    void OnClick(InputValue value)
    {
        isClicked = value.isPressed;
    }

    void Update()
    {
        ProcessClicking();
    }

    private void ProcessClicking()
    {
        if (isClicked)
        {
            if (isPlaceable)
            {
                ProcessPointing();
            }
            isClicked = false;
        }
    }

    private void ProcessPointing()
    {
        ray = Camera.main.ScreenPointToRay(pointerPosition); // Taking camera position and a Vector2 to cast a Ray.
        // hit pointer is the gameObject the ray hits
        if (Physics.Raycast(ray, out hit))
        {
            // if is true when the hit GameObject equals this script's GameObject (the Tile)
            if (gameObject.Equals(hit.transform.gameObject))
            {
                Instantiate(tower, transform.position, Quaternion.identity);
                isPlaceable = false;
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
