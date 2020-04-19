using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EditorSnap : MonoBehaviour
{
    [Range(1f, 20f)] public float gridSnapSize = 10f;
    private TextMesh label;

    void Start() {
        label = transform.GetComponentInChildren<TextMesh>();
    }

    void Update() {
        Vector3 snapPos;

        snapPos.x = Mathf.RoundToInt(transform.position.x / gridSnapSize) * gridSnapSize;
        snapPos.y = Mathf.RoundToInt(transform.position.y / gridSnapSize) * gridSnapSize;
        snapPos.z = Mathf.RoundToInt(transform.position.z / gridSnapSize) * gridSnapSize;

        transform.position = snapPos;
        string PosText = snapPos.x / gridSnapSize + "," + snapPos.z / gridSnapSize;
        label.text = PosText;
        transform.name = "Cube (" + PosText + ")";
    }

    void TopText(string input) {
        label.text = input;
    }
}
