using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            tile.SetTowerIndex(index);
        }
    }
}
