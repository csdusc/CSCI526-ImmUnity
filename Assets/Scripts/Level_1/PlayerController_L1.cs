using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For Analytics
using System;
using System.Globalization;
using Unity.Services.Core;
using UnityEngine.Networking;

public class PlayerController_L1 : MonoBehaviour
{
    public float speed;
    public float jump;
    public float move;
    public float currentPlatform;
    public Rigidbody2D rb;
    private bool isJumping; //added code

    public GameOver_Manager gameOverManager;
    public GameOver_Manager levelCompleteScreen;

    public VerticalBridgeUp vbu;
    public VerticalBridgeDown vbd;

    // For Analytics
 
    //[SerializeField] private string URL; // URL for ObstacleDeathTrackingAnalytics
    //[SerializeField] private string URL2; // URL for LevelCompletionAnalytics
    private long _sessionID;
    private string _obstacleAtWhichKilled;
    
    private bool _playerStarted = true;
    //private bool _playerCompletedLevel = false;
    
    public void Send(string obstacleAtWhichKilled)
    {
        _sessionID = DateTime.Now.Ticks;
        StartCoroutine(Upload(_sessionID.ToString(), obstacleAtWhichKilled));
    }
    
    private IEnumerator Upload(string sessionID, string obstacleAtWhichKilled)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.238388247", sessionID); 
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

    public void Send2(bool playerCompletedLevel)
    {
        StartCoroutine(Upload2(_playerStarted.ToString(), playerCompletedLevel.ToString()));
    }

    private IEnumerator Upload2(string playerStarted, string playerCompletedLevel)
    {
        WWWForm form = new WWWForm();
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
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 3.5f;
        jump = 215;
        currentPlatform = -1f;
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
        move = Input.GetAxis("Horizontal");
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
                Send(target.gameObject.name);
            }
            else
            {
                Send(target.gameObject.tag);
            }
            Send2(false);
            // RestartGame();
            gameOverManager.SetGameOver();
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
            Send(target.gameObject.tag);
            Send2(false);
            // RestartGame();
            gameOverManager.SetGameOver();
        }
        else if(target.tag == "SetPlatform0")
        {
            currentPlatform = 0;
        }
        else if(target.tag == "SetPlatform1")
        {
            currentPlatform = 1;
        }
        else if(target.tag == "VerticalBridgeDown" || target.tag == "VerticalBridgeUp")
        {
            gameOverManager.SetGameOver();
        }
        else if(target.tag == "LevelCompleted")
        {
            //Send(target.gameObject.tag);
            Send2(true);
            // RestartGame();
            gameOverManager.SetLevelComplete();
        }
        else if(target.gameObject.tag == "Freeze_PowerUp") {
            // var player = other.GetComponent<PlayerScript>();
            vbu.canMove = false;
            vbd.canMove = false;
        }
    }
}
