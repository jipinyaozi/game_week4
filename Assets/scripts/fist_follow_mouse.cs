using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fist_follow_mouse : MonoBehaviour
{
    // Start is called before the first frame update
    int speed = 300;
    public Rigidbody2D rb;
    public Camera cam;
    public KeyCode mousebutton;

    void Update()
    {
        if (this.tag != "dead")
        {
        Vector3 playerpos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x,cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector3 difference = playerpos - transform.position;
        float rotationZ = Mathf.Atan2(difference.x , -difference.y) * Mathf.Rad2Deg;
        if(Input.GetKey(mousebutton))
        {
            rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotationZ, speed*Time.fixedDeltaTime));
        }
        } 
    }
}
