using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	private Transform camTransform;
	
	// How long the object should shake for.
	private float shakeDuration = 0.5f;
    private float temp = 0.5f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	private float shakeAmount = 0.05f;
	private float decreaseFactor = 1f;
	
	Vector3 originalPos;
	
	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = GetComponent(typeof(Transform)) as Transform;
            originalPos = camTransform.localPosition;
		}
	}

	private void Update(){
        originalPos = transform.localPosition;
    }
	
	void OnEnable()
	{
		originalPos = camTransform.localPosition;
	}

   public IEnumerator Shake(){
        while(temp > 0){
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            temp -= Time.deltaTime * decreaseFactor;

            temp -= Time.deltaTime;
            yield return null;
        }

        temp = shakeDuration;
        camTransform.localPosition = originalPos;
    }
}
