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
    public bool isShield;
    private int gravityDirection;
    private bool temp = false;
    private bool canSawHit;
    private bool canSpikeHit;
    private bool canEnemyHit;
    private bool shieldActivate;
    private bool heartActivate;
    
    public Rigidbody2D rb;
    private Animator anim;
    public GameObject goldenBridge;
    private bool isGoldenBridgeActivated;

    private SpriteRenderer sprite;
    public GameOver_Manager gameOverManager;
    public GameOver_Manager levelCompleteScreen;
    
    // Changing for checkpoint
    //private Health playerHealth;
    public Health playerHealth;
    
    public VerticalBridgeUp vbu;
    public VerticalBridgeDown vbd;
    public GameObject playerShield;
    public CoinBarScript coinBar;
    public CoinCollectAnimation coinCollect;
    public HeartAnimation heartAnimate;
    public ShieldAnimation shieldAnimate;
    public float shieldThreshold;
    public float heratThreshold;
    public CameraController cameraController;
    [SerializeField] private AudioSource coinCollectSound;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private AudioSource healthSound;
    [SerializeField] private AudioSource hurtSound;
    [SerializeField] private AudioSource freezePowerupSound;

    // For Analytics
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
        form.AddField("entry.398560516", "2"); 
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
        form.AddField("entry.1348894228", "2"); 
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
        StartCoroutine(Upload3(coinBar.totalCoinsCollected.ToString(), didPlayerCompleteLevel.ToString()));
        //StartCoroutine(Upload3("0", didPlayerCompleteLevel.ToString()));
    }

    // Upload analytics for coins collected
    private IEnumerator Upload3(string coinsCollected, string didPlayerCompleteLevel)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1343059877", "2"); 
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
        form.AddField("entry.1967777262", "2"); 
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
        form.AddField("entry.620600592", "2"); 
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
        form.AddField("entry.1055560394", "2"); 

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
        gravityDirection = 1; // 1 means normal, -1 means inverted
        rb.gravityScale = gravityDirection * 2f;
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<Health>();
        Init();
        
    }

    void Init()
    {
        speed = 4.7f;
        jump = 350;
        currentPlatform = -1f;
        isShield = false;
        shieldActivate = false;
        heartActivate = false; 
        coinBar.Init();
        isGoldenBridgeActivated = false;
        canSawHit = true;
        canSpikeHit = true;
        canEnemyHit = true;
    }

    void RestartGame()
    {
        Init();
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
            rb.AddForce(new Vector2(rb.velocity.x, gravityDirection * jump));  //commented code
            isJumping = true;
            jumpSound.Play();
        }

        UpdateAnimation();
    }

    private void UpdateAnimation(){
        if(move > 0f)
        {
            anim.SetBool("running", true);

            if (gravityDirection == 1){
                sprite.flipX = false;
            }
            else{
                sprite.flipX = true;
            }
            
        }
        else if(move < 0f)
        {
            anim.SetBool("running", true);
            
            if (gravityDirection == -1){
                sprite.flipX = false;
            }
            else{
                sprite.flipX = true;
            }
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {

        if (
            target.gameObject.tag == "SpikeSet1" ||
            target.gameObject.tag == "SpikeSet2" ||
            target.gameObject.tag == "SpikeSet3" ||
            target.gameObject.tag == "SpikeSet4" ||
            target.gameObject.tag == "SpikeSet5" ||
            target.gameObject.tag == "SpikeSet6" ||
            target.gameObject.tag == "SpikeSet7"
        ){
            if(canSpikeHit)
            {
                canSpikeHit = false;
                StartCoroutine(ResetAnimSpikeHit());
                // Send where player loses health
                Send(target.gameObject.tag);
                isJumping = false;
                Die();
            }
        }
        else if (
            target.gameObject.tag == "Saw1" ||
            target.gameObject.tag == "Saw2" ||
            target.gameObject.tag == "Saw3" ||
            target.gameObject.tag == "Saw4"
        ){
            if(canSawHit)
            {
                canSawHit = false;
                StartCoroutine(ResetSawHit());
                // Send where player loses health
                Send(target.gameObject.tag);
                Die();
            }
        }
        else if (
            target.gameObject.tag == "Enemy1" ||
            target.gameObject.tag == "Enemy2" ||
            target.gameObject.tag == "Enemy3" ||
            target.gameObject.tag == "Enemy4" ||
            target.gameObject.tag == "Enemy5" ||
            target.gameObject.tag == "Enemy6" ||
            target.gameObject.tag == "Enemy7" ||
            target.gameObject.tag == "Enemy8" ||
            target.gameObject.tag == "BulletEnemy"
        ){
            if(!isShield)
            {
                if(canEnemyHit)
                {
                    canEnemyHit = false;
                    StartCoroutine(ResetEnemyHit());
                    // Send where player loses health
                    Send(target.gameObject.tag);
                    Die();
                }
            }
        }
        else if (target.gameObject.tag == "Hinge"){
            // Send where player loses health
            Send(target.gameObject.tag);
            Die();
        }
        else if (target.gameObject.tag == "SpikeBall"){
            // Send where player loses health
            Send(target.gameObject.tag);
            Die();
        }

        if(
            target.gameObject.tag == "Floor" || 
            target.gameObject.tag == "Platform_1_L3" || 
            target.gameObject.tag == "Platform_4_L3" || 
            target.gameObject.tag == "Platform_3_L3"
        ){
            isJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Coin")
        {
            coinBar.AddCoins(1);
            
            if (!shieldActivate && coinBar.currentCoins >= shieldThreshold)
            {
                shieldActivate = true;
                shieldAnimate.startShieldPowerup(target.transform.position);
                
                isShield = true;
                playerShield.SetActive(true);
                StartCoroutine(ResetShieldPowerup());
            }

            if(!heartActivate && coinBar.currentCoins >= heratThreshold)
            {
                heartActivate = true;
                heartAnimate.startHeartPowerup(target.transform.position);
                playerHealth.AddLife(1);
                healthSound.Play();
            }
       
            coinCollect.startCoinMove(target.transform.position, ()=>{
                
            });

            Destroy(target.gameObject);
            coinCollectSound.Play();
        }
        else if(target.tag == "GameOver")
        {
            // Send where player loses health
            Send("JumpedToDeath");
            
            //Send3();
            // RestartGame();
            // gameOverManager.SetGameOver();
            triggerDie();
        }
        else if(target.tag == "SetPlatform0")
        {
            _playerObstacleStartTime = DateTime.Now.ToString("h:mm:ss");
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
            _playerObstacleStartTime = DateTime.Now.ToString("h:mm:ss");
            currentPlatform = 3;
        }
        else if (target.tag == "SetPlatform_4")
        {
            _playerObstacleStartTime = DateTime.Now.ToString("h:mm:ss");
            currentPlatform = 4;
        }
        else if(target.tag == "EndPlatform0")
        {
            _playerObstacleEndTime = DateTime.Now.ToString("h:mm:ss");
            _whichObstaclePassed = "DynamicBridge1";
            Send6(_playerObstacleStartTime,_playerObstacleEndTime,_whichObstaclePassed);
        }
        else if(target.tag == "EndPlatform3")
        {
            _playerObstacleEndTime = DateTime.Now.ToString("h:mm:ss");
            _whichObstaclePassed = "DynamicBridge2";
            Send6(_playerObstacleStartTime,_playerObstacleEndTime,_whichObstaclePassed);
        }
        else if(target.tag == "EndPlatform4")
        {
            _playerObstacleEndTime = DateTime.Now.ToString("h:mm:ss");
            _whichObstaclePassed = "DynamicBridge3";
            Send6(_playerObstacleStartTime,_playerObstacleEndTime,_whichObstaclePassed);
        }
        else if(target.tag == "SpikyVerticalPencil")
        {
            // Send where player loses health
            Send(target.gameObject.tag);
            
            //Send3();
            // gameOverManager.SetGameOver();
            Die();
        }

        else if (target.tag == "Bullet") 
        {
            Die(); 
        }
        
        else if(target.tag == "LevelCompleted")
        {   
            //Send player started vs ended
            Send2(true);
            
            //Send coins collected when level complete
            Send3(true);

            // Send player health left when level completed
            Send5();
            // RestartGame();
            gameOverManager.SetLevelComplete();
        }
        else if(target.gameObject.tag == "Freeze_PowerUp") {
            // var player = other.GetComponent<PlayerScript>();
            vbu.canMove = false;
            vbd.canMove = false;
            
            freezePowerupSound.Play();

            //Send powerup collected
            Send4("FreezePowerup");
        }
        else if(
            target.gameObject.tag == "Life_Powerup_1" || 
            target.gameObject.tag == "Life_Powerup_2" ||
            target.gameObject.tag == "Life_Powerup_3" ||
            target.gameObject.tag == "Life_Powerup_4" ||
            target.gameObject.tag == "Life_Powerup_5" 
        ){
            //Send powerup collected
            //Send4("LifePowerup");
            
            //Send powerup collected
            Send4(target.gameObject.tag);

            playerHealth.AddLife(1);
            healthSound.Play();

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
        else if (target.tag == "Lever")
        {
            if (coinBar.currentCoins >= coinBar.maxCoins)
            {
                if(isGoldenBridgeActivated)
                    return;
                
                isGoldenBridgeActivated = true;

                SpriteRenderer sr = target.gameObject.GetComponent<SpriteRenderer>(); 
                sr.flipX = true;

                StartCoroutine(BridgeScaleUpAnimation(1.5f));
            }
            else
            {
                StartCoroutine(cameraController.Shake());
            }
        }
        else if(target.tag == "GravityPad")
        {
            if(!temp){
                temp = true;
                gravityDirection *= -1;
            
                if(gravityDirection == 1)
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    // transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0f), Time.deltaTime * 2f);

                    // Vector3 direction = new Vector3(0, 0, 0);
                    // Quaternion targetRotation = Quaternion.Euler(direction);
                    // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 20f);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                    // transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180f), Time.deltaTime * 2f);

                    // Vector3 direction = new Vector3(180, 180, 180);
                    // Quaternion targetRotation = Quaternion.Euler(direction);
                    // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 20f);
                }

                rb.gravityScale = gravityDirection * 2f;
                StartCoroutine(ResetGravityPad());
            }
        }
    }

    IEnumerator BridgeScaleUpAnimation(float time)
    {
        float i = 0;
        float rate = 1 / time;

        Vector3 fromScale = goldenBridge.transform.localScale;
        // Vector3 toScale = new Vector3(4.0f, fromScale.y, fromScale.z);
        Vector3 toScale = new Vector3(6.0f, fromScale.y, fromScale.z);
        while (i<1)
        {
            i += Time.deltaTime * rate;
            goldenBridge.transform.localScale = Vector3.Lerp(fromScale, toScale, i);
            yield return 0;
        }
    }

    private IEnumerator ResetGravityPad()
    {
        yield return new WaitForSeconds(0.5f);
        temp = false;
    }

    private IEnumerator ResetShieldPowerup()
    {
        yield return new WaitForSeconds(5);
        isShield = false;
        playerShield.SetActive(false);
    }

    private IEnumerator ResetSawHit()
    {
        yield return new WaitForSeconds(0.5f);
        canSawHit = true;
    }
    
    private IEnumerator ResetAnimSpikeHit()
    {
        yield return new WaitForSeconds(0.5f);
        canSpikeHit = true;
    }
    
    private IEnumerator ResetEnemyHit()
    {
        yield return new WaitForSeconds(0.5f);
        canEnemyHit = true;
    }


    //Changing for checkpoints
    //private void Die()
    public void Die()
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

    //Changing for checkpoints
    //private void triggerDie()
    public void triggerDie()
    {
        //Send player started vs ended
        Send2(false);
        
        //Send coins collected on death
        Send3(false);
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
