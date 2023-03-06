using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projecttilrPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;

    [Header("AI")] 
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;
    Coroutine fireingCoroutine;
    AudioPlayer audioPlayer;

    void Awake(){
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start(){
        if (useAI) {
            isFiring = true;
        }
    }

    void Update(){
        Fire();
    }
    void Fire() {
        if (isFiring && fireingCoroutine == null){
            fireingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && fireingCoroutine != null){
            StopCoroutine(fireingCoroutine);
            fireingCoroutine = null;
        }      
    }

    IEnumerator FireContinuously() {

        while (true) {
            GameObject instance = Instantiate(projecttilrPrefab, 
                                             transform.position, 
                                             Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null) {
                //follow green arrow * speed
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifetime); 

            float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance,
                                                   baseFiringRate + firingRateVariance);
            //make sure the time not negative
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();
            //audioPlayer.GetInstance().PlayShootingClip();
            
            yield return new WaitForSeconds(timeToNextProjectile);
        }
        
    }
}
