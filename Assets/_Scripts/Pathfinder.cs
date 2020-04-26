using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{/*
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
        LoadBlocks();       //Put the blocks into a collection
        Pathfind();         //Find connections in the collection
        SetRoute();         //Backwork the route based on connections
    }



    private void LoadBlocks() {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();

        foreach(Waypoint waypoint in waypoints) {

            Vector2Int gridPos = waypoint.GetGridPos();
            bool isOverlapping = grid.ContainsKey(gridPos);

            if (isOverlapping) {
                Debug.LogWarning("Overlapping grid in: " + gridPos + ", skipping it");          //Avoid duplication of blocks in our grid
            } else {
                grid.Add(gridPos, waypoint);
            }
        }
    }


    private void Pathfind() {
        queue.Enqueue(startWaypoint);                                       //Load the start point into the queue

        while (queue.Count > 0 && isRunning) {          
            searchCentre = queue.Dequeue();                                 //Take the first instance out of the queue
            searchCentre.isExplored = true;                                 //and set it to having been explored
            HaltIfEndFound();                                               //check if this point is the goal waypoint
            ExplorNeighbours();                                             //check neighbours
        }
    }

    void ExplorNeighbours() {
        if (!isRunning) { return; }                                                     //Stop if we've found the end point already
        foreach (Vector2Int direction in directions) {                                  //For each possible direction
            Vector2Int neighbourGrid = searchCentre.GetGridPos() + direction;           //Defining which neighbour we're talking about

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
        }
    }

    private void HaltIfEndFound() {                                 //Used to stop our search if we find the goal - no need to map the full grid
        if (searchCentre == goalWaypoint) {
            isRunning = false;
        }
    }

    private void SetRoute() {                                       //This backworks the route from the found finishing position based on where the block was found from
        Waypoint currentGridPos = goalWaypoint;
        correctroute.Add(currentGridPos);
        do {
            currentGridPos = currentGridPos.exploredFrom;
            correctroute.Add(currentGridPos);
        } while (currentGridPos != startWaypoint);
        correctroute.Reverse();                                     //Due to backworking our list in in the wrong order so fix this
    }

    public List<Waypoint> GetRoute() {
        return correctroute;                                        //Called by anyone needing the route  //TODO: Rewrite class to take in current and goal to return a route
    }

*/}
