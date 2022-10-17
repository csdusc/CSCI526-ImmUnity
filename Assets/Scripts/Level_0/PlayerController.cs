using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For Analytics
using System;
using System.Globalization;
using Unity.Services.Core;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jump;
    public float move;
    public float currentPlatform;
    public Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private bool isJumping; //added code
    private Health playerHealth;
    private bool isShield;
    public GameObject playerShield;

    public GameOver_Manager gameOverManager;
    public GameOver_Manager levelCompleteScreen;

    // For Analytics
    public CoinBarScript coinBar;

    private long _sessionID;
    private string _obstacleAtWhichKilled;
    
    private bool _playerStarted = true;
    //private bool _playerCompletedLevel = false;
    
    private string _playerObstacleStartTime;
    private string _playerObstacleEndTime;
    private string _whichObstaclePassed;
    
    // Send analytics for which obstacle player dies at
    public void Send(string obstacleAtWhichKilled) 
    {
        _sessionID = DateTime.Now.Ticks;
        StartCoroutine(Upload(_sessionID.ToString(), obstacleAtWhichKilled));
    }
    
    // Upload analytics for which obstacle player dies at
    private IEnumerator Upload(string sessionID, string obstacleAtWhichKilled)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.238388247", sessionID); 
        form.AddField("entry.398560516", "0"); 
        form.AddField("entry.263405774", obstacleAtWhichKilled); 

        using (UnityWebRequest www = UnityWebRequest.Post("https://docs.google.com/forms/u/1/d/e/1FAIpQLSc3i-n9x8AJqcv515EK0m2QpnjE469JP2JZFj9kZ6feGUkXyQ/formResponse", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    // Send analytics for player started vs player completed
    public void Send2(bool playerCompletedLevel)
    {
        StartCoroutine(Upload2(_playerStarted.ToString(), playerCompletedLevel.ToString()));
    }

    // Upload analytics for player started vs player completed
    private IEnumerator Upload2(string playerStarted, string playerCompletedLevel)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1348894228", "0"); 
        form.AddField("entry.1296817409", playerStarted); 
        form.AddField("entry.1997032823", playerCompletedLevel); 

        using (UnityWebRequest www = UnityWebRequest.Post("https://docs.google.com/forms/u/1/d/e/1FAIpQLSfWrTbL7RnzfRwhPSt5qotC_VetYFLBERdKuWV-fk95mXjRCQ/formResponse", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
    
    // Send analytics for coins collected
    public void Send3(bool didPlayerCompleteLevel)
    {
        //StartCoroutine(Upload3(CoinCollection.totalCoins.ToString()));
        //StartCoroutine(Upload3(coinBar.totalCoinsCollected.ToString(), didPlayerCompleteLevel.ToString()));
        StartCoroutine(Upload3("0", didPlayerCompleteLevel.ToString()));
    }

    // Upload analytics for coins collected
    private IEnumerator Upload3(string coinsCollected, string didPlayerCompleteLevel)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1343059877", "0"); 
        form.AddField("entry.1440472328", coinsCollected); 
        form.AddField("entry.1758890323", didPlayerCompleteLevel); 
        

        using (UnityWebRequest www = UnityWebRequest.Post("https://docs.google.com/forms/u/1/d/e/1FAIpQLSeSiwt_tfJAKJMgfO3_XUM9Mcy4qAY0k1GZ9EZxFniEgQ65Sg/formResponse", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    // Send analytics for powerup collected
    public void Send4(string powerUp)
    {
        StartCoroutine(Upload4(powerUp));
    }
    
    // Upload analytics for powerup collected
    private IEnumerator Upload4(string powerUpCollected)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1967777262", "0"); 
        form.AddField("entry.977184578", powerUpCollected); 

        using (UnityWebRequest www = UnityWebRequest.Post("https://docs.google.com/forms/u/1/d/e/1FAIpQLScKKVkFtl5NUh2yYz2aLK0dlbEalRYyNjtUHoFZ0Un-aHLc-g/formResponse", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
    
    // Send analytics for how much health player finishes level with
    public void Send5()
    {
        StartCoroutine(Upload5(playerHealth.currenthealth.ToString()));
    }

    // Upload analytics for how much health player finishes level with
    private IEnumerator Upload5(string healthLeft)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.620600592", "0"); 
        form.AddField("entry.642392757", healthLeft); 

        using (UnityWebRequest www = UnityWebRequest.Post("https://docs.google.com/forms/u/1/d/e/1FAIpQLSfn9OCUbcktHgrI__y7ZQ_S5gdJlIJFcKgPkCsOSzEWeiNAQA/formResponse", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }

    // Send analytics for time taken at dynamic bridges
    public void Send6(string playerObstacleStartTime, string playerObstacleEndTime, string whichObstaclePassed)
    {
        StartCoroutine(Upload6(playerObstacleStartTime, playerObstacleEndTime, whichObstaclePassed));
    }

    // Upload analytics for time taken at dynamic bridges
    private IEnumerator Upload6(string playerObstacleStartTime, string playerObstacleEndTime, string whichObstaclePassed)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1889763671", playerObstacleStartTime); 
        form.AddField("entry.1329460763", playerObstacleEndTime); 
        form.AddField("entry.605989258", whichObstaclePassed); 
        form.AddField("entry.1055560394", "0"); 

        using (UnityWebRequest www = UnityWebRequest.Post("https://docs.google.com/forms/u/1/d/e/1FAIpQLSeUOUyRCnKKFf5VjsHPLuDckOEE88x4Dl2Fv6s5joUnzpb3Yw/formResponse", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        speed = 4f;
        jump = 350;
        currentPlatform = -1f;
        isShield = false;

        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<Health>();
    }

    void RestartGame()
    {
        CoinCollection.totalCoins = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            // //added code
            // if( isGrounded == true){
            //     rb.AddForce(new Vector2(rb.velocity.x, jump));
            // }
            // isGrounded = true;
            rb.AddForce(new Vector2(rb.velocity.x, jump));  //commented code
            isJumping = true;
        }

        UpdateAnimation();
    }

    private void UpdateAnimation(){
        if(move > 0f){
            anim.SetBool("running", true);
            sprite.flipX = false;
        }
        else if(move < 0f){
            anim.SetBool("running", true);
            sprite.flipX = true;
        }
        else{
            anim.SetBool("running", false);
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Water" || target.gameObject.tag == "Obstacle" || target.gameObject.tag == "Hinge")
        {
            if (target.gameObject.tag == "Water")
            {
                // Send where player loses health
                Send("JumpedIntoWater");
            }
            else if (target.gameObject.tag == "Obstacle")
            {
                if(isShield)
                {
                    Destroy(target.gameObject);
                }

                // Send where player loses health
                Send(target.gameObject.name);

            }
            else
            {
                // Send where player loses health
                Send(target.gameObject.tag);
            }
            //Send2(false);
            //Send3();
            // RestartGame();
            // gameOverManager.SetGameOver();
            Die();
        }
        if(target.gameObject.tag == "Floor" || target.gameObject.tag == "CoinPlatform" || target.gameObject.tag == "Platform_0" || target.gameObject.tag == "Platform_1") 
        {
            isJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "GameOver")
        {   
            // Send where player loses health
            Send("JumpedToDeath");

            //Send player started vs ended
            Send2(false);
            
            // Send coins collected on death
            //Send3(false);

            // RestartGame();
            // gameOverManager.SetGameOver();
            triggerDie();
        }
        else if(target.tag == "SetPlatform0")
        {
            currentPlatform = 0;
        }
        else if(target.tag == "SetPlatform1")
        {
            _playerObstacleStartTime = DateTime.Now.ToString("h:mm:ss");
            currentPlatform = 1;
        }
        else if(target.tag == "SetPlatform2")
        {
            _playerObstacleStartTime = DateTime.Now.ToString("h:mm:ss");
            currentPlatform = 2;
        }
        else if(target.tag == "EndPlatform1")
        {
            _playerObstacleEndTime = DateTime.Now.ToString("h:mm:ss");
            _whichObstaclePassed = "DynamicBridge1";
            Send6(_playerObstacleStartTime,_playerObstacleEndTime,_whichObstaclePassed);
        }
        else if(target.tag == "EndPlatform2")
        {
            _playerObstacleEndTime = DateTime.Now.ToString("h:mm:ss");
            _whichObstaclePassed = "DynamicBridge2";
            Send6(_playerObstacleStartTime,_playerObstacleEndTime,_whichObstaclePassed);
        }
        else if(target.tag == "LevelCompleted")
        {
            //Send player started vs ended
            Send2(true);
            
            //Send coins collected analytics
            Send3(true);
            
            // Send player health left when level completed
            Send5();

            // RestartGame();
            gameOverManager.SetLevelComplete();
        }
        else if(target.gameObject.tag == "Life_Powerup")
        {
            //Send powerup collected
            Send4("LifePowerup");

            playerHealth.AddLife(1);
            Destroy(target.gameObject);
        }
        else if (target.gameObject.tag == "Shield_Powerup")
        {
            //Send powerup collected
            Send4("ShieldPowerup");

            Destroy(target.gameObject);
            isShield = true;
            playerShield.SetActive(true);
            StartCoroutine(ResetShieldPowerup());
        }
    }

    private IEnumerator ResetShieldPowerup()
    {
        yield return new WaitForSeconds(5);
        isShield = false;
        playerShield.SetActive(false);
    }

    private void Die()
    {
        if(isShield)
            return;

        playerHealth.TakeDamage(1);

        if (playerHealth.currenthealth <= 0)
        {
            //Send player started vs ended
            Send2(false);
            
            triggerDie();
        }
        else
        {
            anim.SetTrigger("hurt");
        }
    }

    private void triggerDie()
    {
        // Send coins collected on death
        Send3(false);

        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        Invoke("callGameOver", 1f); 
    }

    private void callGameOver()
    {
        gameOverManager.SetGameOver();
    }
}
