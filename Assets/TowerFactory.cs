using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    public int maxTowers = 3;
    GameObject tower;
    List<GameObject> towerRing = new List<GameObject>();

    void Start() {
        tower = Resources.Load<GameObject>("Tower");
    }

    public void RequestTower(Waypoint waypoint) {
        if(towerRing.Count < maxTowers) {
            CreateNewTower(waypoint);
        } else {
            MoveTower(waypoint);
        }
    }

    private void CreateNewTower(Waypoint waypoint) {
        GameObject newTower = (GameObject)Instantiate(tower, waypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = GameObject.Find("PlayerDefence").transform;
        waypoint.isPlaceable = false;
        newTower.GetComponent<FireBlast>().myWaypoint = waypoint;
        towerRing.Add(newTower);
    }

    private void MoveTower(Waypoint waypoint) {
        GameObject currentTower = towerRing[0];
        FireBlast currentFireBlast = currentTower.GetComponent<FireBlast>();

        currentFireBlast.myWaypoint.isPlaceable = true; //Tell the old waypoint it can be placed on again
        currentFireBlast.myWaypoint = waypoint;         //Move the waypoint reference
        waypoint.isPlaceable = false;

        currentTower.transform.position = waypoint.transform.position;
        towerRing.Remove(currentTower);
        towerRing.Add(currentTower);                    //Move the tower out
    }

    public void ResetTowers() {
        foreach (GameObject currentTower in towerRing) {

            FireBlast currentFireBlast = currentTower.GetComponent<FireBlast>();
            currentFireBlast.myWaypoint.isPlaceable = true;
            Destroy(currentTower.gameObject);
        }

        towerRing.Clear();
    }

}
