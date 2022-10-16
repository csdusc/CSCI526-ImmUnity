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
    //[SerializeField] private string URL; // URL for ObstacleDeathTrackingAnalytics
    //[SerializeField] private string URL2; // URL for LevelCompletionAnalytics
    private long _sessionID;
    private string _obstacleAtWhichKilled;
    
    private bool _playerStarted = true;
    //private bool _playerCompletedLevel = false;
    
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
    public void Send3()
    {
        StartCoroutine(Upload3(CoinCollection.totalCoins.ToString()));
    }

    // Upload analytics for coins collected
    private IEnumerator Upload3(string coinsCollected)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1343059877", "0"); 
        form.AddField("entry.1440472328", coinsCollected); 

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
                Send("JumpedIntoWater");
            }
            else if (target.gameObject.tag == "Obstacle")
            {
                if(isShield)
                {
                    Destroy(target.gameObject);
                }

                Send(target.gameObject.name);
            }
            else
            {
                Send(target.gameObject.tag);
            }
            Send2(false);
            Send3();
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
            //Send(target.gameObject.tag);
            Send2(false);
            Send3();
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
            currentPlatform = 1;
        }
        else if(target.tag == "SetPlatform2")
        {
            currentPlatform = 2;
        }
        else if(target.tag == "LevelCompleted")
        {
            //Send(target.gameObject.tag);
            Send2(true);
            Send3();
            // RestartGame();
            gameOverManager.SetLevelComplete();
        }
        else if(target.gameObject.tag == "Life_Powerup")
        {
            playerHealth.AddLife(1);
            Destroy(target.gameObject);
        }
        else if (target.gameObject.tag == "Shield_Powerup")
        {
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
            triggerDie();
        }
        else
        {
            anim.SetTrigger("hurt");
        }
    }

    private void triggerDie()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        Invoke("callGameOver", 1f); 
    }

    private void callGameOver()
    {
        gameOverManager.SetGameOver();
    }
}
