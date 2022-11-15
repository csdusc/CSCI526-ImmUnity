using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShieldAnimation : MonoBehaviour
{
    public float shieldAnimationSpeed;
    public Transform target;
    public GameObject Shield_Prefab;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void startShieldPowerup(Vector3 _initial) 
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, cam.transform.position.z * -1);
        GameObject shield = Instantiate(Shield_Prefab, transform);
        StartCoroutine(MoveShield(shield.transform, _initial, targetPos));
        // StartCoroutine(MoveShieldTrans(life.transform, _initial, target, onComplete));
    }

    IEnumerator MoveShield(Transform obj, Vector3 startPos, Vector3 endPos){
        float time = 0;
        while(time < 1){
            time += shieldAnimationSpeed * Time.deltaTime;
            obj.position = Vector3.Lerp(startPos, endPos, time);

            yield return new WaitForEndOfFrame();
        }

        Destroy(obj.gameObject);
    }

    // IEnumerator MoveShieldTrans(Transform obj, Vector3 startPos, Transform endPos, Action onComplete){
    //     Vector3 endPoint = new Vector3(endPos.position.x, endPos.position.y, cam.transform.position.z * -1);
    //     obj.position = startPos;
        
    //     while((endPoint - obj.position).magnitude > 0.5f){
    //         obj.Translate((endPoint - obj.position).normalized * shieldAnimationSpeed * Time.deltaTime);
    //         yield return new WaitForEndOfFrame();

    //         endPoint = new Vector3(endPos.position.x, endPos.position.y, cam.transform.position.z * -1);
    //     }

    //     Destroy(obj.gameObject);
    //     onComplete.Invoke();
    // }
}
