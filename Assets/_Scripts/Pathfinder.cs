using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    List<Waypoint> correctroute = new List<Waypoint>();

    Vector2Int[] directions = {
                            Vector2Int.up,
                            Vector2Int.right,
                            Vector2Int.down,
                            Vector2Int.left, 
    };

    private Queue<Waypoint> queue = new Queue<Waypoint>();
    private Waypoint searchCentre;

    public bool isRunning = true;
    public Waypoint startWaypoint;
    public Waypoint goalWaypoint;

    void Start() {
        LoadBlocks();
        ColourBlocks();
        Pathfind();
        SetRoute();
        DebugRoute();
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

    private void Pathfind() {
        queue.Enqueue(startWaypoint);                                       //Load the start point into the queue

        while (queue.Count > 0 && isRunning) {          
            searchCentre = queue.Dequeue();                        //Take the first instance out of the queue
            Debug.Log("Searching from " + searchCentre);
            searchCentre.isExplored = true;                                 //and set it to having been explored
            HaltIfEndFound();                                   //check if this point is the goal waypoint
            ExplorNeighbours();                                 //check neighbours
        }
    }

    void ExplorNeighbours() {
        if (!isRunning) { return; }                                                     //Stop if we've found the end point already
        foreach (Vector2Int direction in directions) {                                  //For each possible direction
            Vector2Int neighbourGrid = searchCentre.GetGridPos() + direction;         //Defining which neighbour we're talking about

            if (grid.ContainsKey(neighbourGrid)) {                                      //Check if it exists
                QueueNewNeighbour(neighbourGrid);                                       //If it does add it to the queue
            }
        }
    }

    private void QueueNewNeighbour(Vector2Int neighbourGrid) {
        Waypoint neighbour = grid[neighbourGrid];                           //Define the waypoint as per grid pos
        if (!neighbour.isExplored && !queue.Contains(neighbour)) {          //If it's already been explored or added we don't do anything           //TODO - Clean this up
            queue.Enqueue(neighbour);                                       //Add it to the queue
            neighbour.exploredFrom = searchCentre;
            Debug.Log("Queuing " + neighbour);                              //Let me know it's already in the queue
        }
    }

    private void HaltIfEndFound() {
        if (searchCentre == goalWaypoint) {
            Debug.Log("I found finishline " + goalWaypoint);
            isRunning = false;
        }
    }

    private void SetRoute() {
        Waypoint currentGridPos = goalWaypoint;
        correctroute.Add(currentGridPos);
        do {
            currentGridPos = currentGridPos.exploredFrom;
            correctroute.Add(currentGridPos);
        } while (currentGridPos != startWaypoint);
        correctroute.Reverse();
    }

    public List<Waypoint> GetRoute() {
        return correctroute;
    }

    private void DebugRoute() {
        for (int i = 0; i < correctroute.Count; i++) {
            Debug.Log("Found Route: " + correctroute[i]);
            correctroute[i].SetTopColour(Color.blue);
        }
    }

}
