using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Vector2Int[] directions = {
                            Vector2Int.up,
                            Vector2Int.down,
                            Vector2Int.left,
                            Vector2Int.right,
    };

    public Waypoint startWaypoint;
    public Waypoint goalWaypoint;

    void Start() {
        LoadBlocks();
        ColourBlocks();
        ExplorNeighbours(startWaypoint.GetGridPos());
    }

    private void LoadBlocks() {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();

        foreach(Waypoint waypoint in waypoints) {

            Vector2Int gridPos = waypoint.GetGridPos();
            bool isOverlapping = grid.ContainsKey(gridPos);

            if (isOverlapping) {
                Debug.LogWarning("Overlapping grid in: " + gridPos + ", skipping it");
            } else {
                grid.Add(gridPos, waypoint);
            }
        }
    }

    void ColourBlocks() {
        startWaypoint.SetTopColour(Color.red);
        goalWaypoint.SetTopColour(Color.green);
    }

    void ExplorNeighbours(Vector2Int checkGrid) {
        foreach(Vector2Int direction in directions) {

            Vector2Int neighbourGrid = checkGrid + direction;
            Debug.Log("Checking neighbour: " + neighbourGrid);
            if(grid.ContainsKey(neighbourGrid)) {
                grid[neighbourGrid].SetTopColour(Color.blue);
            }
        }
    }

    void Update() {
        
    }
}
