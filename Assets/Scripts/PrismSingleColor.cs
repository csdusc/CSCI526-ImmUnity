using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrismSingleColor : MonoBehaviour
{
    public GameObject[] toDisable;
    public GameObject[] toEnable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag == "Player")
        {
            for (int i = 0; i < toDisable.Length; i++)
            {
                toDisable[i].SetActive(false);
            }

            for (int i = 0; i < toEnable.Length; i++)
            {
                toEnable[i].SetActive(true);
            }

            Destroy(gameObject.transform.parent.gameObject, 0);
        }
    }
}
