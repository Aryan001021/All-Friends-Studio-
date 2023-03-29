using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] GameObject platform;
    [SerializeField] GameObject gate;
    [SerializeField] GameObject sinkingPlatform;
    [SerializeField] GameObject deathBed;
    [SerializeField] GameObject[] obstacles;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject[] powerUp;
    List<GameObject> instantitatedObjects = new List<GameObject>();
    //Level Stuff
    LevelHandler levelHandler;
    Score scoreScript;
    GameObject player;
    int currentLevel;
    //game imp bools
    bool gapHappen = false;
    bool firstSpawn = true;
    bool gateSpawned = false;
    bool increasedDifficulty = false;
    float latestPlatformPosX = 0;
    int gap = 8;
    int currentEnemy = 0;
    //int for procedural
    int levelLength;
    int score;
    int difficulty;
    int noOfEnemiesToSpawn;


    private void Awake()
    {
        scoreScript = FindObjectOfType<Score>();
        levelHandler = FindObjectOfType<LevelHandler>();
       
        player = GameObject.Find("Player");
        if (player == null)
        {
            print("find not Player");
        }
        
        difficulty = levelHandler.GetDifficultyLevel();
        levelLength = levelHandler.GetLevelLength();
        currentLevel = levelHandler.GetCurrentLevel();
        noOfEnemiesToSpawn = levelHandler.GetCurrentNoOfEnemies();
    }
    void Update()
    {
        ProceduralPrinter();
        ProceduralConditions();
        LevelHandler();
    }

    private void LevelHandler()
    {
        if (currentLevel == 0)
        {
            LevelZero(300);
        }
        else if (currentLevel > 0)
        {
            AllLevel(levelLength);
        }
    }

    private void ProceduralPrinter()
    {
        print("level Lenght:" + levelLength);
        print("score:" + score);
        print("difficulty:" + difficulty);
        print("no of enemies to spawn:"+ noOfEnemiesToSpawn);
    }

    private void ProceduralConditions()
    {
        score = scoreScript.GetScore();
        if (score % 500 == 0 && !increasedDifficulty)
        { 
           difficulty++;
           increasedDifficulty=true;
           if (difficulty % 2 == 0)
           {
               noOfEnemiesToSpawn++;
           }
           if (score % 1000 == 0)
           {
               levelLength += 200;
           }
        }
        if (score % 501 == 0)
        {
            increasedDifficulty = false;
        }  
    }

    void LevelZero(int levelLenght)
    {
        int spawnDistance=levelLenght/obstacles.Length;
        if (firstSpawn)
        {
            Instantiate(platform, new Vector3(latestPlatformPosX, 0f, 0f), Quaternion.identity, transform);
            latestPlatformPosX += 20;
            Instantiate(platform, new Vector3(latestPlatformPosX, 0f, 0f), Quaternion.identity, transform);
            latestPlatformPosX += 20;
            firstSpawn = false;
        }
        if (latestPlatformPosX < player.transform.position.x + 300 && !gateSpawned) 
        {
            if (latestPlatformPosX >= levelLenght)
            {
                Instantiate(gate, new Vector3(latestPlatformPosX, 5.4f, 0f), Quaternion.identity, transform);
                gateSpawned= true;
            }
            Instantiate(platform, new Vector3(latestPlatformPosX, 0f, 0f), Quaternion.identity, transform);
            if (currentEnemy < obstacles.Length && !ObjectInListChecker(obstacles[currentEnemy]))
            {
                Instantiate(obstacles[currentEnemy], new Vector3(latestPlatformPosX, 2f, 0f), Quaternion.identity, transform);
                instantitatedObjects.Add(obstacles[currentEnemy]);
                currentEnemy++;
            }
            if (instantitatedObjects.Count == 5)
            {
                latestPlatformPosX += gap;
            }
            if (instantitatedObjects.Count == obstacles.Length - 2)
            {
                latestPlatformPosX += gap;
                latestPlatformPosX += gap;
            }
             latestPlatformPosX += 20;
        }
    }
    void AllLevel (int levelLenght)
    {
        if (firstSpawn && latestPlatformPosX < player.transform.position.x + 300 && !gateSpawned)
        {
            for (int i=0; i < 3; i++)
            {
                Instantiate(platform, new Vector3(latestPlatformPosX, 0f, 0f), Quaternion.identity, transform);
                
                latestPlatformPosX+= 20;
            }
            firstSpawn= false;
        }
        else if(latestPlatformPosX<levelLenght && !gateSpawned && latestPlatformPosX < player.transform.position.x + 300 )
        {
            Instantiate(platform, new Vector3(latestPlatformPosX, 0f, 0f), Quaternion.identity, transform);
            ObstaclesSpawner("platform");
            if (OddEven())
            {
                latestPlatformPosX += gap;
                if (OddEven())
                {
                    if (OddEven())
                    {
                        latestPlatformPosX += gap;
                    }
                }
            }
            latestPlatformPosX += 20;
            InstantiateGate(levelLenght);
        }
    }

    private void InstantiateGate(int levelLenght)
    {
        if (latestPlatformPosX > levelLenght - 1  && !gateSpawned)
        {
            Instantiate(gate, new Vector3(latestPlatformPosX , 5.4f, 0f), Quaternion.identity, transform);
            for (int i = 0; i < 2; i++)
            {
                gateSpawned = true;
                Instantiate(platform, new Vector3(latestPlatformPosX, 0f, 0f), Quaternion.identity, transform);
                latestPlatformPosX += 20;
            }
        }
    }
    void ObstaclesSpawner(string type)
    {
        if (type == "platform")
        {
            int tempEnemyPlacement = Random.Range(-7, 7);
            for (int enemy = 0; enemy < noOfEnemiesToSpawn; enemy++)
            {
                if (latestPlatformPosX + tempEnemyPlacement < latestPlatformPosX + 9 && latestPlatformPosX + tempEnemyPlacement > latestPlatformPosX - 9)
                {
                    if (tempEnemyPlacement < 0)
                    {
                        tempEnemyPlacement += 2;
                    }
                    else
                    {
                        tempEnemyPlacement-= 1;
                    }
                    int tempObstacle = Random.Range(0, obstacles.Length);
                    Instantiate(obstacles[tempObstacle], new Vector3(latestPlatformPosX + tempEnemyPlacement, 2f, 0f), Quaternion.identity, transform);
                }
            }
        } 
        if (type == "gap")
        {

        }
    }
    public int GetLevelLength ()
    {
        return levelLength;
    }
    //public int GetScore()
    //{
    //    return score;
    //}
    public int GetDifficulty()
    {
        return difficulty;
    }
    public int GetNoOfEnemiesToSpawn()
    {
        return noOfEnemiesToSpawn;
    }
    private bool ObjectInListChecker(GameObject currentobj)
    {
        foreach (GameObject gmObj in instantitatedObjects)
        {
            if (gmObj == currentobj)
            {
                return true;
            }
        }
        return false;
    }
    private bool OddEven()
    {
        int a= Random.Range(1,1000);
        if (a%2 == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
