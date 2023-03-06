using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour{

    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool isLooping;
    WaveConfigSO currentWave;

    void Start(){
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave() {
        return currentWave;
    }

    IEnumerator SpawnEnemyWaves(){
        do{
            foreach (WaveConfigSO wave in waveConfigs){
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEenmyCount(); i++){
                    Instantiate(currentWave.GetEnemyPrefab(i),
                               currentWave.GetStartingWaypoint().position,
                               //Quaternion.identity means "no rotation"
                               //Quaternion.identity,
                               //change Quaternion.identity to z :180 
                               // instantiate opposite to make shooting direction right
                               Quaternion.Euler(0, 0, 180),
                               //parents: inside this gameobject
                               transform);
                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        while (isLooping);      
    }
}
