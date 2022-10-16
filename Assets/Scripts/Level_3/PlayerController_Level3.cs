using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For Analytics
using System;
using System.Globalization;
using Unity.Services.Core;
using UnityEngine.Networking;

public class PlayerController_Level3 : MonoBehaviour
{
    public float speed;
    public float jump;
    public float move;
    public float currentPlatform;
    private bool isJumping;
    private bool isShield;

    public Rigidbody2D rb;
    private Animator anim;
    public GameObject goldenBridge;
    private bool isGoldenBridgeActivated;

    private SpriteRenderer sprite;
    public GameOver_Manager gameOverManager;
    public GameOver_Manager levelCompleteScreen;
    private Health playerHealth;
    public VerticalBridgeUp vbu;
    public VerticalBridgeDown vbd;
    public GameObject playerShield;
    public CoinBarScript coinBar;
    public CameraController cameraController;
    [SerializeField] private AudioSource coinCollectSound;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource healthSound;
    [SerializeField] private AudioSource hurtSound;

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
        form.AddField("entry.398560516", "1"); 
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
        form.AddField("entry.1348894228", "1"); 
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
        form.AddField("entry.1343059877", "1"); 
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

    // Send analytics for powerup collected
    public void Send4(string powerUp)
    {
        StartCoroutine(Upload4(powerUp));
    }

    // Upload analytics for coins collected
    private IEnumerator Upload4(string powerUpCollected)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1967777262", "1"); 
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

        coinBar.Init();
        isGoldenBridgeActivated = false;
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
            rb.AddForce(new Vector2(rb.velocity.x, jump));  //commented code
            isJumping = true;
            jumpSound.Play();
        }

        UpdateAnimation();
    }

    private void UpdateAnimation(){
        if(move > 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = false;
        }
        else if(move < 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = true;
        }
        else
        {
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
            
            Die();
        }
        if(target.gameObject.tag == "Floor" || target.gameObject.tag == "Obstacle" || target.gameObject.tag == "CoinPlatform" || target.gameObject.tag == "Platform_1_L3" ||  target.gameObject.tag == "Platform_3_L3" || target.gameObject.tag == "Platform_4_L3") 
        {
            isJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Coin")
        {
            Destroy(target.gameObject);
            coinBar.AddCoins(1);
            coinCollectSound.Play();
        }
        else if(target.tag == "GameOver")
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
        else if(target.tag == "UpDownPlatform")
        {
            currentPlatform = 2;
        }
        else if (target.tag == "SetPlatform_3")
        {
            currentPlatform = 3;
        }
        else if (target.tag == "SetPlatform_4")
        {
            currentPlatform = 4;
        }
        else if(target.tag == "SpikyVerticalPencil")
        {
            Send(target.gameObject.tag);
            Send2(false);
            Send3();
            // gameOverManager.SetGameOver();
            triggerDie();
        }
        else if(target.tag == "LevelCompleted")
        {
            //Send(target.gameObject.tag);
            Send2(true);
            Send3();
            // RestartGame();
            gameOverManager.SetLevelComplete();
        }
        else if(target.gameObject.tag == "Freeze_PowerUp") {
            // var player = other.GetComponent<PlayerScript>();
            vbu.canMove = false;
            vbd.canMove = false;
            Send4(target.gameObject.tag.ToString());
        }
        else if(target.gameObject.tag == "Life_Powerup")
        {
            playerHealth.AddLife(1);
            healthSound.Play();

            Destroy(target.gameObject);
        }
        else if (target.gameObject.tag == "Shield_Powerup")
        {
            Destroy(target.gameObject);
            isShield = true;
            playerShield.SetActive(true);
            StartCoroutine(ResetShieldPowerup());
        }
        else if (target.tag == "Lever")
        {
            if (coinBar.currentCoins >= coinBar.maxCoins)
            {
                if(isGoldenBridgeActivated)
                    return;
                
                isGoldenBridgeActivated = true;

                coinCollectSound.Play();
                SpriteRenderer sr = target.gameObject.GetComponent<SpriteRenderer>(); 
                sr.flipX = true;

                StartCoroutine(BridgeScaleUpAnimation(1.5f));
            }
            else
            {
                StartCoroutine(cameraController.Shake());
            }
        }
    }

    IEnumerator BridgeScaleUpAnimation(float time)
    {
        float i = 0;
        float rate = 1 / time;

        Vector3 fromScale = goldenBridge.transform.localScale;
        Vector3 toScale = new Vector3(4.0f, fromScale.y, fromScale.z);
        while (i<1)
        {
            i += Time.deltaTime * rate;
            goldenBridge.transform.localScale = Vector3.Lerp(fromScale, toScale, i);
            yield return 0;
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
            hurtSound.Play();
            anim.SetTrigger("hurt");
        }
    }

    private void triggerDie()
    {
        playerShield.SetActive(false);
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
        deathSound.Play();

        Invoke("callGameOver", 1f); 
    }

    private void callGameOver()
    {
        gameOverManager.SetGameOver();
    }
}
