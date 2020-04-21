using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private const float gridSize = 10.2f;
    private Vector2Int gridPos;
    
    void Start() {
        
    }

    public float  GetGridSize() {
        return gridSize;
    }

    public Vector2Int GetGridPos() {
        gridPos.x = Mathf.RoundToInt(transform.position.x / gridSize);
        gridPos.y = Mathf.RoundToInt(transform.position.z / gridSize);
        return gridPos;
    }

    public void SetTopColour(Color colour) {
        transform.Find("Quad - Top").GetComponent<Renderer>().material.color = colour;
    }

    void Update() {
        
    }
}
