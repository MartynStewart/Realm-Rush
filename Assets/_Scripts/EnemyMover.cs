using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    List<Waypoint> path = default;
    [SerializeField] float stepRate = 1f;

    void Start() {
        path = FindObjectOfType<Pathfinder>().GetRoute();
        if(path.Count > 0) StartCoroutine(MoveWaypoint());
    }

    IEnumerator MoveWaypoint() {
        foreach (Waypoint step in path) {
            transform.position = step.transform.position;
            yield return new WaitForSeconds(stepRate);
        }
        Debug.Log("Done");
    }


    void Update() {  
    }
}
