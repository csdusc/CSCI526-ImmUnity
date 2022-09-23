using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jump;
    public float move;
    public float currentPlatform;
    public Rigidbody2D rb;
    private bool isJumping; //added code

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
            RestartGame();
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
            RestartGame();
        }
        else if(target.tag == "SetPlatform0")
        {
            currentPlatform = 0;
        }
        else if(target.tag == "SetPlatform1")
        {
            currentPlatform = 1;
        }
    }
}
