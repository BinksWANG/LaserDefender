using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Wave Config", fileName ="New Wave Config")]
public class WaveConfigSO : ScriptableObject{

    [SerializeField] List<GameObject> enemyPrefab;
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float timeBetweenEnemySpawns = 1f;
    [SerializeField] float spawnTimeVariance = 0f; //fangcha
    [SerializeField] float minimumSpawnTime = 0.2f;

    public int GetEenmyCount() {
        return enemyPrefab.Count;
    }

    public GameObject GetEnemyPrefab(int index) {
        return enemyPrefab[index];
    }

    public Transform GetStartingWaypoint() {
        //return first child
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints() {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in pathPrefab) {
            waypoints.Add(child);
        }
        return waypoints;
    }

    public float getMoveSpeed() {
        return moveSpeed;
    }

    public float GetRandomSpawnTime() {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance,
                                        timeBetweenEnemySpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
