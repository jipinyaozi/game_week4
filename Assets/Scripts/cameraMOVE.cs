using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMOVE : MonoBehaviour
{
    private Vector3 cameraMOVEpos;

    void Start()
    {
        cameraMOVEpos = this.transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        float moveAmount = 20f;
        if(Input.GetKey(KeyCode.W))
        {
            cameraMOVEpos.y += moveAmount * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S))
        {
            cameraMOVEpos.y -= moveAmount * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A))
        {
            cameraMOVEpos.x -= moveAmount * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.D))
        {
            cameraMOVEpos.x += moveAmount * Time.deltaTime;
        }
        this.transform.position = cameraMOVEpos;
    }
}
