using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform2_Script : MonoBehaviour
{
    private float min_x_left = 0.5f, max_x_right = 4.0f, speed = 500f, scaleRate = 0.03f;
    private bool canMove;
    private Rigidbody2D body;
    public PlayerController playerController;
    public GameOver_Manager gameOverManager;
    public CameraShake cameraShake;
    public bool textFieldEnabled2 = false;
    public CoinScore cs;
    public GameObject Insuff_coins_panel;
    public GameObject Panel_coins_all;
    public string textFieldText2 = "Insufficient coins to drop bridge";

    //Checkpoint
    private Vector3 respawnPoint;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0f;
        //Checkpoint
        respawnPoint = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }


    void OnGUI() 
	{
         if (textFieldEnabled2) 
	  {
             textFieldText2 = GUI.TextField(new Rect(500, 180, 195, 23), textFieldText2);
         }
	}

    
    // Update is called once per frame
    void Update()
    {
        MovePlatform();
        body.constraints = RigidbodyConstraints2D.None;
        if(Input.GetKeyDown(KeyCode.DownArrow) && playerController.currentPlatform == 3)
        {
            if(!CoinCollection.canDropLastBridge)
            {
                Insuff_coins_panel.SetActive(true);
                StartCoroutine(cameraShake.Shake());
            }
            else
            {
                Insuff_coins_panel.SetActive(false);		
                DropPlatform();
            }
        }
    }

    void DropPlatform()
    {
        canMove = false;
        body.gravityScale = 1f;
    }

    void MovePlatform()
    {
        if(canMove)
        {
            Vector3 pos = transform.localScale;

            // if(pos.x > max_x_right || pos.x < min_x_left){
            //     speed *= -1f; // change direction
            // }

            if(transform.localScale.x > max_x_right){
                speed = -200f; // change direction
            }
            else if(transform.localScale.x < min_x_left){
                speed = 200f; // change direction
            }

            transform.localScale = new Vector3(
                transform.localScale.x + (scaleRate * speed * Time.deltaTime), 
                transform.localScale.y, 
                transform.localScale.z);
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Water")
        {
            // Send where player loses health
            playerController.Send("Bridge2dup");
            
            //Send player started vs ended
            playerController.Send2(false);
            
            //Send coins collected on death
            playerController.Send3(false);
            // RestartGame();
            
            //Changing for checkpoint
            //gameOverManager.SetGameOver();

            if(playerController.playerHealth.currenthealth >= 1)
            { 
                playerController.Die();
                canMove = false;
                
                body.angularVelocity = 0f;
                body.velocity = Vector2.zero;

                body.transform.eulerAngles = Vector3.zero;
                
                body.gravityScale = 0f;

                transform.position = respawnPoint;

                canMove = true;
            }
            else
            {
                playerController.triggerDie();
            }
        }

        if(target.gameObject.tag == "Hinge")
        {
		Panel_coins_all.SetActive(false);
            StartCoroutine(ScaleDownAnimation(0.5f));
        }
    }

    IEnumerator ScaleDownAnimation(float time)
    {
        float i = 0;
        float rate = 1 / time;

        Vector3 fromScale = transform.localScale;
        Vector3 toScale = new Vector3(max_x_right, fromScale.y, fromScale.z);
        while (i<1)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(fromScale, toScale, i);
            yield return 0;
        }

        body.isKinematic = false;
        body.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    void RestartGame()
    {
        //playerController.Send();
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}
