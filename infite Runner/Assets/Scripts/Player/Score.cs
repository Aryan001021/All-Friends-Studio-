using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    int score=0;
    int curruncy;
    int currentTime;
    int maxCurruntPosition;
    LevelHandler levelHandler;
    private void Awake()
    {
        currentTime=(int)Time.time;
        levelHandler = FindObjectOfType<LevelHandler>();
        if ( levelHandler != null )
        {
            score=levelHandler.GetLevelScore();
        }
    }
    public int GetScore()
    {
        return score;
    }
    public int GetCurrency()
    {
        return curruncy;
    }
    void Update()
    {
        if (transform.position.x>maxCurruntPosition)
        {  
            maxCurruntPosition += 1;
            score += 1;
            if ((int)Time.time - currentTime >= 1)
            {
                curruncy++;
                currentTime = (int)Time.time;
            }
        }
    }

}
