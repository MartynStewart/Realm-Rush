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
    private bool isDefenceGrid = false;

    public GameObject Model1;
    public GameObject Model2;
    public GameObject Model3;

    private void OnCollisionEnter(Collision collision) {
        
    }

    void Awake() {
        waypoint = GetComponent<Waypoint>();
        
    }

    void Start() {
    }

    void Update() {
        SnapToGrid();
        UpdateLabel();
        isDefenceGrid = waypoint.isDefenceGrid;
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

        if (isDefenceGrid) {
            Model1.SetActive(false);
            Model2.SetActive(false);
            Model3.SetActive(true);
        } else {

            int gridNo = Mathf.RoundToInt(gridPos.x + gridPos.y);
            if (gridNo % 2 == 0) {
                Model1.SetActive(true);
                Model2.SetActive(false);
                Model3.SetActive(false);
            } else {
                Model1.SetActive(false);
                Model2.SetActive(true);
                Model3.SetActive(false);
            }
        }
    }
}
