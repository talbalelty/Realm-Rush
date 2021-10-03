using UnityEngine;

// This script handle the different tower types with GUI buttons
public class ButtonManager : MonoBehaviour
{
    Tile tile;

    void Start()
    {
        tile = FindObjectOfType<Tile>();
    }

    public void SelectTower(int index)
    {
        if (tile != null)
        {
            // Tower index is a static variable, all the Tiles will spawn the new tower type
            tile.SetTowerIndex(index);
        }
    }
}
