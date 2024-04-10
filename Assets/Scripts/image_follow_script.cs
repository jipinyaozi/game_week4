using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class image_follow_script : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPos);
        transform.position = worldPosition;

        
    }
}
