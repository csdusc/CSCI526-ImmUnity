using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_10_Script : MonoBehaviour
{
    // private float min_x_left = 0.2f, max_x_right = 4.0f, speed = 0f, scaleRate = 0.1f;
    private float min_x_left = 0.2f, max_x_right = 4.0f, speed = 230f, scaleRate = 0.03f;
    private float platformspeed = 300f;
    private bool canMove;
    private Rigidbody2D body;
    public PlayerController_L1 playerController;
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
        if(target.gameObject.tag == "Water")
        {
            playerController.Send("Bridge1");
            playerController.Send2(false);
            //playerController.Send3();
            // RestartGame();
            gameOverManager.SetGameOver();
        }

        if(target.gameObject.tag == "Hinge")
        {
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
