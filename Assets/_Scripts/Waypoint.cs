using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private const float gridSize = 10.2f;
    private Vector2Int gridPos;
    private TowerFactory towerFactory;

    public bool isDefenceGrid = false;
    public bool isPlaceable = true;

    void Start() {
        towerFactory = FindObjectOfType<TowerFactory>();
    }

    public float  GetGridSize() {
        return gridSize;
    }

    public Vector2Int GetGridPos() {
        gridPos.x = Mathf.RoundToInt(transform.position.x / gridSize);
        gridPos.y = Mathf.RoundToInt(transform.position.z / gridSize);
        return gridPos;
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0) && isDefenceGrid && isPlaceable) {
            CreateTower();
        }
    }

    private void CreateTower() {
        towerFactory.RequestTower(this);
    }
}
