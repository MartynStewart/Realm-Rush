﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireBlast : MonoBehaviour
{

    public Waypoint myWaypoint = default;
    public GameObject myTarget;
    private ParticleSystem myParticle;
    private EnemySpawner enemySpawner;

    [Range(1,20)]public float range = 3;        //Adj range based on grid size

    void Start() {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        myParticle = GetComponentInChildren<ParticleSystem>();
        range *= FindObjectOfType<Waypoint>().GetGridSize();
    }


    void Update() {
        if (myTarget == null || myTarget.activeSelf == false) {
            myParticle.Play(false);
            myTarget = enemySpawner.GetTarget(transform.position);
        } else {
            myParticle.gameObject.transform.LookAt(myTarget.transform);
            Debug.DrawLine(transform.position, myTarget.transform.position,Color.black);
            if(Vector3.Distance(transform.position,myTarget.transform.position) > range) {
                myTarget = null;
            }

        }
    }
}
