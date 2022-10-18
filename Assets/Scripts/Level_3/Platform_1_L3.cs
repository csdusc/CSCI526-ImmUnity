using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_1_L3 : MonoBehaviour
{
    private float min_x_left = 0.8f, max_x_right = 3.85f, speed = 150f, scaleRate = 0.03f;
    private float max_right_animation = 4.0f;
    private float platformspeed = 300f;
    private bool canMove;
    private Rigidbody2D body;
    public PlayerController_Level3 playerController;
    public GameOver_Manager gameOverManager;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();

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

            // if(pos.x > max_x_right || pos.x < min_x_left){
            //     speed *= -1f; // change direction
            // }

            if(transform.localScale.x > max_x_right){
                platformspeed = -speed; // change direction
            }
            else if(transform.localScale.x < min_x_left){
                platformspeed = speed; // change direction
            }

            transform.localScale = new Vector3(
                transform.localScale.x + (scaleRate * platformspeed * Time.deltaTime), 
                transform.localScale.y, 
                transform.localScale.z);
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Hinge")
        {
            StartCoroutine(ScaleDownAnimation(0.5f));
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag == "GameOver")
        {
            // Send where player loses health
            playerController.Send("DynamicBridge1");
            
            //Send player started vs ended
            playerController.Send2(false);
            
            //Send coins collected on death
            playerController.Send3(false);
            // RestartGame();
            gameOverManager.SetGameOver();
        }
    }

    IEnumerator ScaleDownAnimation(float time)
    {
        float i = 0;
        float rate = 1 / time;

        Vector3 fromScale = transform.localScale;
        Vector3 toScale = new Vector3(max_right_animation, fromScale.y, fromScale.z);
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
