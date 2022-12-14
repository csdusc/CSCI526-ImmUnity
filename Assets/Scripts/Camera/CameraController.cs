using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    // Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	private Transform camTransform;
	
	// How long the object should shake for.
	private float shakeDuration = 0.5f;
    private float temp = 0.5f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	private float shakeAmount = 0.15f;
	private float decreaseFactor = 1f;
	
	Vector3 originalPos;

    private void Update(){
        transform.position = new Vector3(player.position.x + 5f, player.position.y + 1.5f, -10f);
        originalPos = transform.position;
    }
	
	void OnEnable()
	{
		originalPos = transform.position;
	}

   public IEnumerator Shake(){
        while(temp > 0){
            transform.position = originalPos + Random.insideUnitSphere * shakeAmount;
            temp -= Time.deltaTime * decreaseFactor;

            temp -= Time.deltaTime;
            yield return null;
        }

        temp = shakeDuration;
        transform.position = originalPos;
    }
}
