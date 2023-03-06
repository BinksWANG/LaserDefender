using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour{

    int score;
    static ScoreKeeper instance;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        //GetType(): Gets the Type of the current instance
        //int instanceCount = FindObjectsOfType(GetType()).Length;
        //if (instanceCount > 1){
        if (instance != null)
        {
            //other object may try to access this version audioplayer before destroy, so disactivate
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore() {
        return score;
    }

    public void ModifyScore(int value) {
        score += value;
        //prevent it lower than 0
        Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log(score);
    }

    public void ResetScore(){
        score = 0;
    }
}
