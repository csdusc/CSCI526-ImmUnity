using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class messageBox : MonoBehaviour
{
    public GameObject messagebox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            messagebox.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            messagebox.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
