using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bridgeTxtScript : MonoBehaviour
{
    public TMP_Text tmptext;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Time.timeScale = 1;
            Destroy(gameObject);
        }

    }

    public float time=5;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
