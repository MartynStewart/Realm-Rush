using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour
{
    const float gridSnapSize = 10.2f;
    private Waypoint waypoint;
    private TextMesh label;
    Vector3 gridPos;

    void Start() {
        label = transform.GetComponentInChildren<TextMesh>();
    }

    void Update() {

        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid() {
        
        gridPos.x = Mathf.RoundToInt(transform.position.x / gridSnapSize) * gridSnapSize;
        gridPos.y = Mathf.RoundToInt(transform.position.y / gridSnapSize) * gridSnapSize;
        gridPos.z = Mathf.RoundToInt(transform.position.z / gridSnapSize) * gridSnapSize;
        transform.position = gridPos;
    }

    private void UpdateLabel() {
        string PosText = transform.position.x / gridSnapSize + "," + transform.position.z / gridSnapSize;
        label.text = PosText;
        transform.name = "Cube (" + PosText + ")";
    }
}
