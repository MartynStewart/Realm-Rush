using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruePathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();         //Holds the grid itself
    List<Waypoint> visited = new List<Waypoint>();                                          //Holds a list of points we've already been to
    Dictionary<Waypoint, Waypoint> routeList = new Dictionary<Waypoint, Waypoint>();        //Holds how we got TO, FROM each grid pos
    Queue<Waypoint> queue = new Queue<Waypoint>();                                          //Holds the current "To Check" queue
    List<Waypoint> correctroute = new List<Waypoint>();                                     //Holds the route taken to get to the goal

    Vector2Int[] directions = {
                            Vector2Int.up,
                            Vector2Int.right,
                            Vector2Int.down,
                            Vector2Int.left, 
    };

    private Waypoint searchCentre;
    private Waypoint startWaypoint;
    private Waypoint goalWaypoint;
    private bool isRunning = true;

    private void Awake() {
        LoadBlocks();       //Put the blocks into a collection
    }

    public Waypoint GetGridPos(Vector2Int checkTransform) {
        Debug.Log("looking up for position: " + checkTransform);
        return grid[checkTransform];
    }

    public List<Waypoint> GeneratePath(Waypoint initialPoint, Waypoint endPoint) {

        if (initialPoint == endPoint) return null;

        startWaypoint = initialPoint;
        goalWaypoint = endPoint;
        isRunning = true;

        CleanLists();
        Pathfind();         //Find connections in the collection
        SetRoute2();         //Backwork the route based on connections
        //DebugRoute();
        ColourBlocks();     //Highlight the start/end positions     //TODO: Remove this
        return correctroute;
    }



    private void LoadBlocks() {
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        grid.Clear();

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

    void ColourBlocks() {
        startWaypoint.SetTopColour(Color.red);
        goalWaypoint.SetTopColour(Color.green);
    }

    private void Pathfind() {
        queue.Enqueue(startWaypoint);                                       //Load the start point into the queue

        while (queue.Count > 0 && isRunning) {          
            searchCentre = queue.Dequeue();                                 //Take the first instance out of the queue
            visited.Add(searchCentre);                                      //Added to the visited grid list
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
        if (visited.Contains(neighbour) == false && !queue.Contains(neighbour)) {          //If it's already been explored or added we don't do anything           //TODO - Clean this up
            queue.Enqueue(neighbour);                                       //Add it to the queue
            routeList.Add(neighbour, searchCentre);
        }
    }

    private void HaltIfEndFound() {                                 //Used to stop our search if we find the goal - no need to map the full grid
        if (searchCentre == goalWaypoint) {
            isRunning = false;
        }
    }

    private void SetRoute2() {                                       //This backworks the route from the found finishing position based on where the block was found from
        Waypoint currentGridPos = goalWaypoint;
        correctroute.Add(currentGridPos);
        do {
            currentGridPos = routeList[currentGridPos];             //Get the explored from from the dictionary
            correctroute.Add(currentGridPos);                   
        } while (currentGridPos != startWaypoint);
        correctroute.Reverse();                                     //Due to backworking our list in in the wrong order so fix this
    }

    private void CleanLists() {
        visited.Clear();
        routeList.Clear();
        queue.Clear();
        correctroute.Clear();
    }


    private void DebugRoute() {                                 //Use if we need to review the route taken
        for (int i = 0; i < correctroute.Count; i++) {
            correctroute[i].SetTopColour(Color.blue);
        }
    }

}
