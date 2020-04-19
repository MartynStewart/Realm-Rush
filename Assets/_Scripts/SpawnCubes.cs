using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCubes : MonoBehaviour
{
    public int CubeTotal_x = 5;
    public int CubeTotal_y = 5;
    public int CubeTotal_z = 5;
    [SerializeField] GameObject cube_prefab = default;

    public Color StartingColour;
    public Color FinishedColour;

    private Vector3 colorDelta;

    void Start() {

        Debug.Log(StartingColour + " | " + FinishedColour);

        colorDelta = new Vector3(FinishedColour.r - StartingColour.r, FinishedColour.g - StartingColour.g, FinishedColour.b - StartingColour.b);

        for (int i = 0; i < CubeTotal_x; i++) {
            for (int j = 0; j < CubeTotal_y; j++) {
                for (int k = 0; k < CubeTotal_z; k++) {

                    float xRatio = (i / (float)(CubeTotal_x - 1));
                    float yRatio = (j / (float)(CubeTotal_y - 1));
                    float zRatio = (k / (float)(CubeTotal_z - 1));

                    Color cubeColour = new Color(xRatio * colorDelta.x, yRatio * colorDelta.y, zRatio * colorDelta.z, 1) + StartingColour;
                    Vector3 pos = Vector3.right * i + Vector3.back * j + Vector3.down * k;

                    GameObject newCube = (GameObject)Instantiate(cube_prefab, pos, Quaternion.identity);
                    newCube.name = "Cube(" + i + ", " + j + ", " + k + ")";
                    newCube.GetComponent<MeshRenderer>().material.color = cubeColour;
                }
            }

        }
    }


    void Update() {
        
    }
}
