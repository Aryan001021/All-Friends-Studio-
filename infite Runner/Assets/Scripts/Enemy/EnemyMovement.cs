using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
   [SerializeField] EnemyPlayerDetection playerDetection;
   GameObject player;
    Rigidbody2D enemyRigidbody;
    [SerializeField]int enemySpeed = 7;
    private void Awake()
    {
        player = GameObject.Find("Player");
        if (player == null)
        {
            print("Player not found");
        }
        enemyRigidbody= GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        EnemyMovementAndState();
    }

    private void EnemyMovementAndState()
    {
        if (playerDetection.PlayerInsideEnemyzone() == true)
        {
            float localScalea = transform.localScale.x;

            float enemySpeedUsed = enemySpeed * Time.deltaTime;
           // spriteRenderer.color = colorAtAwake;
            if (player.transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                enemyRigidbody.velocity = new Vector3(1 * enemySpeedUsed, 0);
            }
            if (player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                enemyRigidbody.velocity = new Vector3(-1 * enemySpeedUsed, 0);
            }
        }
        else
        {
          //  spriteRenderer.color = colorAtRest;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
