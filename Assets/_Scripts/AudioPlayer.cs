using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] [Range(0f, 1f)] float damageVolume = 1f;

    static AudioPlayer instance;

    void Awake(){
        ManageSingleton();
    }

    void ManageSingleton() {
        //GetType(): Gets the Type of the current instance
        //int instanceCount = FindObjectsOfType(GetType()).Length;
        //if (instanceCount > 1){
        if (instance != null) { 
            //other object may try to access this version audioplayer before destroy, so disactivate
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip() {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip(){
        PlayClip(damageClip, damageVolume);
    }

    void PlayClip(AudioClip clip, float volume) {
        if (clip != null){
            Vector3 camaraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, camaraPos, volume);
        }
    }
}
