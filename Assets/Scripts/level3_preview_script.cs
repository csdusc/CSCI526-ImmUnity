using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level3_preview_script : MonoBehaviour
{
    


    public GameObject main_cam;	
    public GameObject Level3_preview_camera;
    public GameObject player;

    void Start()
    {
        
		StartCoroutine(Preview());
    }


	IEnumerator Preview()

          {
		  
             yield return new WaitForSeconds (8);
		  main_cam.SetActive(true);
		  player.SetActive(true);
		  Level3_preview_camera.SetActive(false);
		    
           }

    
}
