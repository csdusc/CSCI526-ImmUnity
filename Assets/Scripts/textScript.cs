using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textScript : MonoBehaviour
{
    public TMP_Text tmptext;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }
}
