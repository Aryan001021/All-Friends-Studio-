using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    static LevelHandler instance;
    int currency=0;
    int CurrentLevel = 0;
    int levelLength = 800;
    int curruntScore;
    int dificulty = 0;
    int noOfEnemiesToSpawn=1;
    Gate gateScript;
    private void Awake()
    {
        PlayerPrefs.SetInt("currentLevel", CurrentLevel);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public int GetCurrentNoOfEnemies()
    {
        return noOfEnemiesToSpawn;
    }

    public int GetLevelLength()
    {
        return levelLength;
    }
    public int GetCurrentLevel()
    {
        return CurrentLevel;
    }
    public int GetDifficultyLevel()
    {
        return dificulty;
    }
    public int GetLevelScore()
    {
        return curruntScore;
    }

    private void GateScript_OnGateEntered(object sender, System.EventArgs e)
    {
        Score score= FindObjectOfType<Score>();
        if (score != null)
        {
            curruntScore = score.GetScore();
            if (currency == 0)
            {
                currency = score.GetCurrency();
            }
            else
            {
                currency += score.GetCurrency();
            }
        }
        PlatformSpawner platformSpawner=FindObjectOfType<PlatformSpawner>();
        if (platformSpawner != null)
        {
            dificulty = platformSpawner.GetDifficulty();
            levelLength= platformSpawner.GetLevelLength();
            noOfEnemiesToSpawn=platformSpawner.GetNoOfEnemiesToSpawn();
        }
        CurrentLevel++;
        PlayerPrefs.SetInt("currentLevel", CurrentLevel);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
    void FixedUpdate()
    {
        print("curruncy in level Handler:" + currency);
        if (gateScript == null) 
        {
            gateScript = FindObjectOfType<Gate>();
            if (gateScript != null )
            {
                gateScript.OnGateEntered += GateScript_OnGateEntered;
            }
        }
    }
}
