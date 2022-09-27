using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scriptrightleft : MonoBehaviour
{
     //public TMP_Text tmptext;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) ||  Input.GetKeyDown(KeyCode.LeftArrow))
	  {
            Debug.Log("here");
            Destroy(gameObject);
        }
    }

     public float time = 5; //Seconds to read the text

 
     IEnumerator Start ()
     {
         yield return new WaitForSeconds(time);
         Destroy(gameObject);
     }
}
