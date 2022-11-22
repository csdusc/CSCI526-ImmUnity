using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CoinSnatcherAnimation : MonoBehaviour
{
    public float snatcherAnimationSpeed;
    public Transform target;
    public GameObject Coin_Prefab;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void startCoinSnatcher(Vector3 _initial) 
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, cam.transform.position.z * -1);
        GameObject snatcher = Instantiate(Coin_Prefab, transform);
        StartCoroutine(CoinSnatcher(snatcher.transform, _initial, targetPos));
    }

    IEnumerator CoinSnatcher(Transform obj, Vector3 startPos, Vector3 endPos){
        float time = 0;
        while(time < 1){
            time += snatcherAnimationSpeed * Time.deltaTime;
            obj.position = Vector3.Lerp(startPos, endPos, time);

            yield return new WaitForEndOfFrame();
        }

        Destroy(obj.gameObject);
    }
}
