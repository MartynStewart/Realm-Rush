using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Block> path;

    void Start() {

        foreach(Block step in path) {
            Debug.Log(step.gameObject.name);
        }
        
    }


    void Update() {
        
    }
}
