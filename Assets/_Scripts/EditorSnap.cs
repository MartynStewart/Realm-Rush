using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class EditorSnap : MonoBehaviour
{
    private Waypoint waypoint;
    private float gridSize;

    void Awake() {
        waypoint = GetComponent<Waypoint>();
    }

    void Start() {
        GameObject towerBaseWaypoint;
        towerBaseWaypoint = this.gameObject;
        Vector3 towerPos = towerBaseWaypoint.transform.position + Vector3.up * 3;
    }

    void Update() {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid() {
        gridSize = waypoint.GetGridSize();
        Vector2 gridPos = waypoint.GetGridPos();
        transform.position = new Vector3(gridPos.x * gridSize, 0 , gridPos.y * gridSize);
    }

    private void UpdateLabel() {
        TextMesh label = transform.GetComponentInChildren<TextMesh>();
        string PosText = transform.position.x / gridSize + "," + transform.position.z / gridSize;
        label.text = PosText;
        transform.name = "Cube (" + PosText + ")";
    }
}
