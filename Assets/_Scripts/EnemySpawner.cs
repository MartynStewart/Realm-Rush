using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public bool isGameSpawning = true;
    public GameObject[] enemyPrefabs;
    public Waypoint[] spawnPoints;
    public Waypoint targetLocation;

    public float spawnPulseRate = 2f;            //How often we check for new enemies to spawn
    [Range(1,20)]public int maxActive = 4;
    public GameObject counter;
    private Text enemyCounter;

    void Start() {
        enemyCounter = counter.GetComponent<Text>();
        StartCoroutine(CheckForRespawn());
    }

    private void Update() {
        enemyCounter.text = transform.childCount.ToString();
    }

    IEnumerator DestroyParticle(ParticleSystem myParticle) {
        yield return new WaitForSeconds(1f);
        myParticle.Stop();
        Destroy(myParticle.gameObject);
    }

    IEnumerator CheckForRespawn() {
        do {           
            if (transform.childCount < maxActive) SpawnEnemy();
            yield return new WaitForSeconds(spawnPulseRate);
        } while (isGameSpawning);
    }


    void SpawnEnemy() {
        GameObject enemyToSpawn;
        Waypoint enemySpawnPoint;
        GameObject spawnedEnemy;

        enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.GetLength(0))];
        enemySpawnPoint = spawnPoints[Random.Range(0, spawnPoints.GetLength(0))];

        spawnedEnemy = Instantiate(enemyToSpawn, enemySpawnPoint.transform.position, Quaternion.identity);
        spawnedEnemy.transform.parent = this.gameObject.transform;
        spawnedEnemy.GetComponent<EnemyMover>().goalSeek = targetLocation;
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
