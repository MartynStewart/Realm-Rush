using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    List<EnemyMover> Enemies = new List<EnemyMover>();              //List of all enemies in the scene
    public bool isGameSpawning = true;
    public Waypoint[] spawnPoints;

    public float spawnPulseRate = 2f;            //How often we check for new enemies to spawn

    void Start() {



      foreach(EnemyMover enemy in GetComponentsInChildren<EnemyMover>(true)) {
            Enemies.Add(enemy);
        }

        //StartCoroutine(CheckForRespawn());
    }

    IEnumerator CheckForRespawn() {
        do {

            foreach (EnemyMover enemy in Enemies) {
                if(enemy.isActiveAndEnabled == false) {
                    enemy.transform.position = spawnPoints[Random.Range(0,spawnPoints.Length)].transform.position;
                    enemy.gameObject.SetActive(true);
                    enemy.isAlive = true;
                    break;
                }
            }
            Debug.Log("pulse");
            yield return new WaitForSeconds(spawnPulseRate);
        } while (isGameSpawning);
    }

    public GameObject GetTarget(Vector3 shooter) {
        EnemyMover closestEnemy = default;
        float closestEnemyDistance = 500f;
        

        foreach (EnemyMover enemy in GetComponentsInChildren<EnemyMover>(true)) {
            float distanceTo = Vector3.Distance(shooter, enemy.transform.position);

            if (distanceTo < closestEnemyDistance) {
                closestEnemy = enemy;
                closestEnemyDistance = distanceTo;
            }

        }

        return closestEnemy.gameObject;
    }


}
