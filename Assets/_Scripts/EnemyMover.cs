using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    List<Waypoint> path = default;
    List<Waypoint> myPath = new List<Waypoint>();
    TruePathfinder pathFinder;
    Waypoint myCurrentGrid;

    public float stepRate = 1f;
    public Waypoint goalSeek;
    public bool isSeeking = false;

    void Start() {
        pathFinder = FindObjectOfType<TruePathfinder>();
    }

    private void Update() {
        if(!isSeeking) GetNewPath();
    }

    void FindMyGrid() {
        Vector3 myAdjPos = RoundThis(transform.position * (1 / goalSeek.GetGridSize()), 0);
        Vector2Int my2dPos = new Vector2Int((int)myAdjPos.x, (int)myAdjPos.z);
        myCurrentGrid = pathFinder.GetGridPos(my2dPos);

        if (myCurrentGrid == null) {
            Debug.LogError("Position not found: " + my2dPos);
            
        } else {
            Debug.Log("Found my grid@ " + myCurrentGrid);
        }
    }

    IEnumerator MoveWaypoint() {
        Debug.Log(gameObject.name + " is starting movement");
        foreach (Waypoint myStep in myPath) {
            Debug.Log(gameObject.name + " taking step to: " + myStep.GetGridPos());
            transform.position = myStep.transform.position;
            yield return new WaitForSeconds(stepRate);
        }
        Debug.Log("Done");
    }

    public void GetNewPath() {
        isSeeking = true;
        FindMyGrid();
        path = FindObjectOfType<TruePathfinder>().GeneratePath(myCurrentGrid, goalSeek);
        if (path != null && path.Count > 0) {
            foreach (Waypoint step in path) {
                myPath.Add(step);
            }
            StartCoroutine(MoveWaypoint());
        }
        path = null;
    }

    Vector3 RoundThis(Vector3 vector3, int decimalPlaces = 2) {
        float multiplier = 1;
        for (int i = 0; i < decimalPlaces; i++) {
            multiplier *= 10f;
        }
        return new Vector3(
            Mathf.Round(vector3.x * multiplier) / multiplier,
            Mathf.Round(vector3.y * multiplier) / multiplier,
            Mathf.Round(vector3.z * multiplier) / multiplier);
    }

}
