using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeartAnimation : MonoBehaviour
{
    public float lifeAnimationSpeed;
    public Transform target;
    public GameObject Life_Prefab;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void startHeartPowerup(Vector3 _initial) 
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, cam.transform.position.z * -1);
        GameObject life = Instantiate(Life_Prefab, transform);
        StartCoroutine(MoveHeart(life.transform, _initial, targetPos));
        // StartCoroutine(MoveHeartTrans(life.transform, _initial, target));
    }

    IEnumerator MoveHeart(Transform obj, Vector3 startPos, Vector3 endPos){
        float time = 0;
        while(time < 1){
            time += lifeAnimationSpeed * Time.deltaTime;
            obj.position = Vector3.Lerp(startPos, endPos, time);

            yield return new WaitForEndOfFrame();
        }

        Destroy(obj.gameObject);
    }

    // IEnumerator MoveHeartTrans(Transform obj, Vector3 startPos, Transform endPos){
    //     Vector3 endPoint = new Vector3(endPos.position.x, endPos.position.y, cam.transform.position.z * -1);
    //     obj.position = startPos;
        
    //     while((endPoint - obj.position).magnitude > 0.5f){
    //         obj.Translate((endPoint - obj.position).normalized * lifeAnimationSpeed * Time.deltaTime);
    //         yield return new WaitForEndOfFrame();

    //         endPoint = new Vector3(endPos.position.x, endPos.position.y, cam.transform.position.z * -1);
    //     }

    //     Destroy(obj.gameObject);
    //     // onComplete.Invoke();
    // }
}
