using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(Waypoint))]
public class EditorSnap : MonoBehaviour
{
    private Waypoint waypoint;
    public float gridSize;

    public GameObject Model1;
    public GameObject Model2;

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
        SetModelUsed(gridPos);
    }

    private void UpdateLabel() {
        TextMesh label = transform.GetComponentInChildren<TextMesh>();
        string PosText = transform.position.x / gridSize + "," + transform.position.z / gridSize;
        label.text = PosText;
        transform.name = "Cube (" + PosText + ")";
    }

    private void SetModelUsed(Vector2 gridPos) {
        int gridNo = Mathf.RoundToInt(gridPos.x + gridPos.y);

        if (gridNo % 2 == 0) {
            Model1.SetActive(true);
            Model2.SetActive(false);
        } else {
            Model1.SetActive(false);
            Model2.SetActive(true);
        }
    }
}
