using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoinCollectAnimation : MonoBehaviour
{
     public float coinAnimationSpeed;
     public Transform target;
     public GameObject CoinPrefab;
     public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        // if(cam == null){
        //     cam = Camera.main;
        // }
    }

    public void startCoinMove(Vector3 _initial, Action onComplete){
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, cam.transform.position.z * -1);
        GameObject _coin = Instantiate(CoinPrefab, transform);

        //StartCoroutine(MoveCoin(_coin.transform, _initial, targetPos, onComplete));
        StartCoroutine(MoveCoinTrans(_coin.transform, _initial, target, onComplete));
    }

    IEnumerator MoveCoin(Transform obj, Vector3 startPos, Vector3 endPos, Action onComplete){
        float time = 0;
        while(time < 1){
            time += coinAnimationSpeed * Time.deltaTime;
            obj.position = Vector3.Lerp(startPos, endPos, time);

            yield return new WaitForEndOfFrame();
        }
        onComplete.Invoke();
        Destroy(obj.gameObject);
    }


    IEnumerator MoveCoinTrans(Transform obj, Vector3 startPos, Transform endPos, Action onComplete){
        Vector3 endPoint = new Vector3(endPos.position.x, endPos.position.y, cam.transform.position.z * -1);
        obj.position = startPos;

        while((endPoint - obj.position).magnitude > 0.5f){
            obj.Translate((endPoint - obj.position).normalized * coinAnimationSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();

            endPoint = new Vector3(endPos.position.x, endPos.position.y, cam.transform.position.z * -1);
        }
        onComplete.Invoke();
        Destroy(obj.gameObject);
    }

}
