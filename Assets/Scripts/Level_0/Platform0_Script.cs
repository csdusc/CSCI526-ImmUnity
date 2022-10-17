using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform0_Script : MonoBehaviour
{
    private float min_x_left = 0.2f, max_x_right = 4.0f, speed = 0f, scaleRate = 0.1f;
    private bool canMove;
    private Rigidbody2D body;
    public PlayerController playerController;
    public GameOver_Manager gameOverManager;
    public bool textFieldEnabled = false;
    public string textFieldText = "Good Job, Now Try Dynamic Bridge Ahead";
    public GameObject Panel2;
    public GameObject DropBridgeText;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        // MovePlatform(); // First platform does not move

        if(Input.GetKeyDown(KeyCode.DownArrow) && playerController.currentPlatform == 0)
        {
            DropPlatform();
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

            if(pos.x > max_x_right || pos.x < min_x_left){
                speed *= -1f; // change direction
            }

            transform.localScale = new Vector3(
                transform.localScale.x + (scaleRate * speed * Time.deltaTime), 
                transform.localScale.y, 
                transform.localScale.z);
        }
    }

     void OnGUI() 
	{
         if (textFieldEnabled) 
	  {
             textFieldText = GUI.TextField(new Rect(280, 100, 255, 25), textFieldText);
         }
	}


    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Water")
        {
            // Send where player loses health
            playerController.Send("Bridge1");
            
            //Send player started vs ended
            playerController.Send2(false);
            
            //Send coins collected on death
            playerController.Send3(false);
            // RestartGame();
            gameOverManager.SetGameOver();

        }

        if(target.gameObject.tag == "Hinge")
        {
		Destroy(DropBridgeText);
            if (Panel2 != null)
		{
                 Panel2.SetActive(true);
		}
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}