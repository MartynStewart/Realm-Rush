using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path;
    [SerializeField] float stepRate = 1f;

    void Start() {

        StartCoroutine(MoveWaypoint());
        
    }

    IEnumerator MoveWaypoint() {
        foreach (Waypoint step in path) {
            Debug.Log(step.gameObject.name);
            transform.position = step.transform.position;
            yield return new WaitForSeconds(stepRate);
        }
        Debug.Log("Done");

    }


    void Update() {
        
    }
}
