using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour{

    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

    void Awake(){
        /*  Object.FindObjectOfType
         *  The first active loaded object that matches the specified type.
         *  It returns null if no Object matches the type
        */
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start(){
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].position;
    }

    void Update(){
        FollowPath();
    }

    void FollowPath() {
        if (waypointIndex < waypoints.Count){
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float delta = waveConfig.getMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition){
                waypointIndex++;
            }
        }
        else {
            Destroy(gameObject);
        }
    }
}
