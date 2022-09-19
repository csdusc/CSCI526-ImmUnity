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
    // For Analytics
    [SerializeField] private string URL;
    private long _sessionID;

    // Analytics
    public void Send()
    {
        _sessionID = DateTime.Now.Ticks;
        StartCoroutine(Post(_sessionID.ToString()));
    }
    private IEnumerator Post(string sessionID)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.929766647", sessionID);
        form.AddField("entry.1560238514", CoinCollection.totalCoins.ToString());
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
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
        Send(); // Send analytics of total coins collected by the player in this session
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

        if(Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.tag == "Water" || target.gameObject.tag == "Obstacle" || target.gameObject.tag == "Hinge")
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
