using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickHero : MonoBehaviour
{
    // public float min_x_left = 1f, max_x_right = 5f, speed = 5f, scaleRate = 1f;
    private float min_x_left = 1.5f, max_x_right = 6.6f, speed = 300f, scaleRate = 0.03f;
    private float platformspeed = 300f;
    // private float currSpeed = 5f;
    public bool canMove;
    private Rigidbody2D body;
    public PlayerController_L1 playerController;
    public GameOver_Manager gameOverManager;
    public Transform temp;
    public Quaternion newRotation;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void Start()
    {
        canMove = true;
        newRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90f);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
        if(Input.GetKeyDown(KeyCode.DownArrow) && playerController.currentPlatform == 2)
        {
            // DropPlatform();
            StartCoroutine(FallStick());
          
        }
    }

    IEnumerator FallStick(){
        canMove = false;
        // float i = 0;
        // float rate = 1 / 0.5f;

        Vector3 rotationToAdd = new Vector3(0, 0, -90);
        var x = Rotate(transform, temp, 0.5f);
        body.isKinematic = false;
        body.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return x;


        yield return null;
    }


    public IEnumerator Rotate(Transform currentTransform, Transform target, float time)
	{
		// time = time == 0 ? animationTime : time;
        Debug.Log("Here");

		var passed = 0f;
		var init = currentTransform.transform.rotation;
		while (passed < time)
		{
            Debug.Log("time " + passed);
			passed += Time.deltaTime;
			var normalized = passed / time;
			var current = Quaternion.Lerp(init, newRotation, normalized);
			currentTransform.rotation = current;
			yield return null;
        }
    }


    // void DropPlatform()
    // {
    //     canMove = false;
    //     body.gravityScale = 1f;
    // }

    void MovePlatform()
    {
        if(canMove)
        {
            Vector3 pos = transform.localScale;


            if(transform.localScale.y > max_x_right){
                platformspeed = -speed; // change direction
            }
            else if(transform.localScale.y < min_x_left){
                platformspeed = speed; // change direction
            }

            transform.localScale = new Vector3(
                transform.localScale.x , 
                transform.localScale.y + (scaleRate * platformspeed * Time.deltaTime), 
                transform.localScale.z);
        }
    }

    // void OnCollisionEnter2D(Collision2D target)
    // {
    //     // if(target.gameObject.tag == "Water")
    //     // {
    //     //     playerController.Send("Bridge1");
    //     //     playerController.Send2(false);
    //     //     playerController.Send3();
    //     //     // RestartGame();
    //     //     gameOverManager.SetGameOver();
    //     // }

    //     if(target.gameObject.tag == "Square")
    //     {
    //         // StartCoroutine(ScaleDownAnimation(0.5f));
    //         body.gravityScale = 1f;
    //     }
    // }

    // IEnumerator ScaleDownAnimation(float time)
    // {
    //     float i = 0;
    //     float rate = 1 / time;

    //     Vector3 fromScale = transform.localScale;
    //     Vector3 toScale = new Vector3(max_x_right, fromScale.y, fromScale.z);
    //     while (i<1)
    //     {
    //         i += Time.deltaTime * rate;
    //         transform.localScale = Vector3.Lerp(fromScale, toScale, i);
    //         yield return 0;
    //     }

    //     body.isKinematic = false;
    //     body.constraints = RigidbodyConstraints2D.FreezePosition;
    // }
}
