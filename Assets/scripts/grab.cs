using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grab : MonoBehaviour
{
    private bool hold;
    public KeyCode mousebutton;
    void Update()
    {
    if (this.tag != "dead")
        {

        if(Input.GetKey(mousebutton))
        {
            hold = true;
        }
        else
        {
            hold = false;
            Destroy(GetComponent<FixedJoint2D>());
        }
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(hold)
        {
            Rigidbody2D rb = col.transform.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
                fj.connectedBody = rb;
            }
            else
            {
                FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
            }
        }
    }
}
