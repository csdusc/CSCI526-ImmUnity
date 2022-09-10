using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jump;
    public float move;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        speed = 3.5f;
        jump = 140;
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * move, rb.velocity.y);

        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Water")
        {
            RestartGame();
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "GameOver")
        {
            RestartGame();
        }
    }
}
