using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] int movementSpeed = 10;
    [SerializeField] int movementPowerUpSpeed = 20;
    [SerializeField] int jumpSpeed = 20;
    [SerializeField] Color32 SpeedColor;
    [SerializeField] GameObject timerCanvas;
    [SerializeField] CinemachineVirtualCameraBase followCamera;
    [SerializeField] CinemachineVirtualCameraBase deathCamera;
    bool canMove = true;
    bool uiMovementButtonPressed = false;
    bool onGround = true;
    int jumpCount = 0;
    int defaultSpeed;
    int CurrentLevel;
    static string idle = "idle";
    static string run = "run";
    static string jump = "jump";
    static string death = "death";
    Rigidbody2D playerRigidBody;
    SpriteRenderer playerSpriteRenderer;
    Vector2 movementVector = new Vector2(0, 0);
    Vector3 playerNewSpawnPoint;
    LevelHandler levelHandler;
    Gate gateScript;
    PlayerAnimationScript playerAnimationScript;


    private void Awake()
    {
        levelHandler=FindObjectOfType<LevelHandler> ();
        CurrentLevel = levelHandler.GetCurrentLevel();
        defaultSpeed = movementSpeed;
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimationScript = FindObjectOfType<PlayerAnimationScript>();
        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerNewSpawnPoint = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spawnpoint")
        {
            playerNewSpawnPoint = collision.transform.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacles" || collision.gameObject.tag == "deathbed")
        {
            canMove = false;
            playerRigidBody.Sleep();
            followCamera.Priority = 0;
            deathCamera.Priority = 1;
            playerAnimationScript.ChangeAnimation(death);

        }
        if (collision.gameObject.tag == "Platform")
        {
            onGround = true;
            jumpCount = 0;
        }
        if (collision.gameObject.tag == "Powerup")
        {
            string powerUpType = collision.gameObject.GetComponent<PowerUpHandler>().ReturnPowerUpType();
            if (powerUpType == "Speed")
            {
                if (movementSpeed < movementPowerUpSpeed)
                {
                    timerCanvas.SetActive(true);
                    Destroy(collision.gameObject);
                    playerSpriteRenderer.color = SpeedColor;
                    movementSpeed = movementPowerUpSpeed;
                    StartCoroutine(DecreaseSpeed());
                }
                else if (movementSpeed == movementPowerUpSpeed)
                {
                    StopCoroutine(DecreaseSpeed());
                    PowerUpTimer timer = timerCanvas.GetComponentInChildren<PowerUpTimer>();
                    timer.Resettimer();
                    Destroy(collision.gameObject);
                    playerSpriteRenderer.color = SpeedColor;
                    movementSpeed = movementPowerUpSpeed;
                    StartCoroutine(DecreaseSpeed());
                }
            }
        }
    }
    IEnumerator DecreaseSpeed()
    {


        PowerUpTimer timer = timerCanvas.GetComponentInChildren<PowerUpTimer>();
        while (timer.ReturnTimeLeft() >= 0)
        {

            yield return new WaitForSeconds(1);
            if (timer.ReturnTimeLeft() <= 0)
            {
                playerSpriteRenderer.color = Color.white;
                timer.Resettimer();
                timerCanvas.SetActive(false);
                movementSpeed = defaultSpeed;
                StopCoroutine(DecreaseSpeed());
            }
        }

    }

    [System.Obsolete]
    private void Update()
    {
        if (canMove)
        {
            PlayerMovementFunc();
            AnimationFunc();
            /* comment this whole line when using android*/movementVector = new Vector2(0, 0);

        }
        if (!canMove)
        {
            if (timerCanvas.active == true)
            {
                PowerUpTimer timer = timerCanvas.GetComponentInChildren<PowerUpTimer>();
                timer.Resettimer();
                playerSpriteRenderer.color = Color.white;
                movementSpeed = defaultSpeed;
                timerCanvas.SetActive(false);
            }
            playerSpriteRenderer.color = Color.white;

            StartCoroutine(Resurection());

        }
    }
    IEnumerator Resurection()
    {
        yield return new WaitForSeconds(4);

        transform.position = playerNewSpawnPoint;
        playerRigidBody.WakeUp();
        if (playerRigidBody.IsSleeping() == false)
        {
            deathCamera.Priority = 0;
            followCamera.Priority = 1;
            canMove = true;
        }
    }
    private void AnimationFunc()
    {
        if (onGround)
        {
            if (Mathf.Abs(playerRigidBody.velocity.x) > 0.1f)
            {
                playerAnimationScript.ChangeAnimation(run);
            }
            else
            {
                playerAnimationScript.ChangeAnimation(idle);
            }
        }
        else
        {

        }
    }
    //mobile Ui stuff bellow
    //public void LeftMovement()
    //{

    //    if (transform.localScale.x == 1)
    //    {
    //        transform.localScale = new Vector3(-1, 1, 1);
    //    }
    //    uiMovementButtonPressed = true;
    //    movementVector = new Vector2(-1, 0);
    //}
    //public void UIMovementButtonUp()
    //{
    //    playerRigidBody.velocity = new Vector2(0, playerRigidBody.velocity.y);
    //    uiMovementButtonPressed = false;
    //}
    //public void RightMovement()
    //{

    //    if (transform.localScale.x == -1)
    //    {
    //        transform.localScale = new Vector3(1, 1, 1);
    //    }
    //    uiMovementButtonPressed = true;

    //    movementVector = new Vector2(1, 0);
    //}
    //public void JumpButton()
    //{
    //    onGround = false;

    //    if (jumpCount < 2)
    //    {
    //        jumpCount++;
    //        if (jumpCount == 0)
    //        {
    //            playerAnimationScript.ChangeAnimation(jump);
    //            playerRigidBody.velocity = (new Vector2(playerRigidBody.velocity.x, (jumpSpeed / 2)));
    //        }
    //        else if (jumpCount == 1)
    //        {

    //            playerAnimationScript.ChangeAnimation(jump);
    //            playerRigidBody.velocity = (new Vector2(playerRigidBody.velocity.x, 0));
    //            playerRigidBody.velocity = (new Vector2(playerRigidBody.velocity.x, jumpSpeed));
    //        }
    //    }
    //}
    //mobile ui stuff above

    void PlayerMovementFunc()
    {
        if (Input.GetKey(KeyCode.D))
        {
            uiMovementButtonPressed = true;
            if (transform.localScale.x == -1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            movementVector = new Vector2(1, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            uiMovementButtonPressed = true;
            if (transform.localScale.x == 1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            movementVector = new Vector2(-1, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            onGround = false;

            if (jumpCount < 2)
            {
               
                if (jumpCount == 0)
                {
                    jumpCount++;
                    playerAnimationScript.ChangeAnimation(jump);
                    playerRigidBody.velocity = (new Vector2(playerRigidBody.velocity.x, (jumpSpeed)));
                }
                else if (jumpCount == 1 && CurrentLevel!=0)
                {
                    jumpCount++;
                    playerAnimationScript.ChangeAnimation(jump);
                    playerRigidBody.velocity = (new Vector2(playerRigidBody.velocity.x, 0));
                    playerRigidBody.velocity = (new Vector2(playerRigidBody.velocity.x, jumpSpeed ));
                    
                }
            }
        }
            if (uiMovementButtonPressed)
            {
                playerRigidBody.velocity = new Vector2((movementVector.x * movementSpeed), playerRigidBody.velocity.y);
            }
        }
    
}
