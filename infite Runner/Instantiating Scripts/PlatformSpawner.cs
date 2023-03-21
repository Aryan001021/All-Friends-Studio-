using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{    
    [SerializeField]GameObject platform;
    [SerializeField] GameObject sinkingPlatform;
    [SerializeField] GameObject deathBed;
    [SerializeField]GameObject[] obstacles;
    [SerializeField]GameObject[] obstaclesTwo;
    [SerializeField]GameObject[] obstaclesGap;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject[] powerUp;
    GameObject player;
    bool gapHappen = false;
    float latestPlatformPosX = 0;
    int gap = 8;
    private void Awake()
    {
        player = GameObject.Find("Player");
        if (player==null)
        {
            print("find not Player");
        }
        Instantiate(platform, new Vector3(latestPlatformPosX, 0f, 0f), Quaternion.identity, transform);
        latestPlatformPosX += 20;
    }
    void Update()
    { 
            Main();
    }
    private void Main()
    {
        if (latestPlatformPosX < player.transform.position.x + 300)
        {
            if (OddEven())
            {
                Spawner();
            }
            else
            {
                if (!gapHappen)
                {
                    if (OddEven())
                    {
                        if (OddEven())
                        {
                            Instantiate(sinkingPlatform, new Vector3(latestPlatformPosX, 0f, 0f), Quaternion.identity, transform);
                            Instantiate(deathBed,new Vector3(latestPlatformPosX-5,-30f,0f), Quaternion.identity, transform);
                            Instantiate(deathBed, new Vector3(latestPlatformPosX+4, -30f, 0f), Quaternion.identity, transform);
                            latestPlatformPosX += 20;
                            Spawner();
                        }
                    }
                    else
                    {
                        //if we fall then we land on deathbed and die.
                        float tempSpawnPos = latestPlatformPosX - gap;
                        Instantiate(deathBed, new Vector3(latestPlatformPosX-6, -8f, 0f), Quaternion.identity, transform);
                        latestPlatformPosX += gap;
                        gapHappen = true;
                        if (OddEven())
                        {
                            if (OddEven())
                            {
                                int tempObstacle = Random.Range(0, obstaclesGap.Length);
                                Instantiate(obstaclesGap[tempObstacle], new Vector3(tempSpawnPos - 4, 0f, 0f), Quaternion.identity, transform);
                            }
                            else
                            {
                                Instantiate(obstaclesGap[1], new Vector3(tempSpawnPos, 0f, 0f), Quaternion.identity, transform);
                            }
                            Instantiate(deathBed, new Vector3(latestPlatformPosX-6, -8f, 0f), Quaternion.identity, transform);
                            latestPlatformPosX += gap;

                        }
                        else
                        {
                            if (!OddEven())
                            {
                                //below is obsticle generator from obstacles list
                                int tempEnemyPlacement = Random.Range(-4, 4);
                                int tempObstacle = Random.Range(0, obstaclesGap.Length);
                                Instantiate(obstaclesGap[tempObstacle], new Vector3(tempSpawnPos + tempEnemyPlacement, 0f, 0f), Quaternion.identity, transform);
                            }   //above is obsticle generator from obstacles list

                        }
                    }
                }
            }
        }
    }

    private void Spawner()
    {
        Instantiate(platform, new Vector3(latestPlatformPosX, 0f, 0f), Quaternion.identity,transform);
        //below is obsticle generator from obstacles list
        if (OddEven())
        {
            int tempEnemyPlacement = Random.Range(-6, 9);
            int tempObstacle = Random.Range(0, obstacles.Length);
            float enemyPlacementY = 2f;
            int tempPowerUpPlacement = Random.Range(tempEnemyPlacement + 2, 9);
            int SpawnPointPos = -9;
          /*this like set spawn point*/  Instantiate(spawnPoint, new Vector3(latestPlatformPosX + SpawnPointPos, 2.5f, 0f), Quaternion.identity, transform);
            //below is powerup
            if (OddEven())
            {
                if (9 - tempEnemyPlacement > 1)
                {
                    int tempPowerUp = Random.Range(0, powerUp.Length);
                    Instantiate(powerUp[tempPowerUp], new Vector3(latestPlatformPosX + tempPowerUpPlacement, 2.5f, 0f), Quaternion.identity, transform);
                }
            }
            //above is powerup
            Instantiate(obstacles[tempObstacle], new Vector3(latestPlatformPosX + tempEnemyPlacement, enemyPlacementY, 0f), Quaternion.identity, transform);
            //above is obsticle generator from obstacles list
        }
        else
        {
            if (OddEven())
            {
                //below is obsticle generator from obstaclesTwo list
                int tempEnemyPlacement = Random.Range(-6, 9);
                int tempObstacle = Random.Range(0, obstaclesTwo.Length);
                float enemyPlacementY = 0f;
                Instantiate(obstaclesTwo[tempObstacle], new Vector3(latestPlatformPosX + tempEnemyPlacement, enemyPlacementY, 0f), Quaternion.identity, transform);
                //below is obsticle generator from obstaclesTwo list         
            }
        }
        latestPlatformPosX += 20;
        gapHappen= false;
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
