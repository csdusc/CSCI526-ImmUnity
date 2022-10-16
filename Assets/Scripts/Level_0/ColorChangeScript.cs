using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeScript : MonoBehaviour
{
    public SpriteRenderer spriteRendererToChangeColor;
 
    private void Awake(){
        spriteRendererToChangeColor = GetComponent<SpriteRenderer>();
    }

    public void ChangeColor(){
        // spriteRendererToChangeColor.color = new Color (0.1f, 0.8f, 0.2f, 1);
    }
}
