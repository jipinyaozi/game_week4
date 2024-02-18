using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisioncontrol : MonoBehaviour
{
    public GameObject player;
    Movement playercontrol;
    // Start is called before the first frame update
    void Start()
    {
        playercontrol = player.GetComponent<Movement>();
    }
    void Update()
    {
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        //Debug.Log("triggered");
        if(this.tag != "dead"){
            if(col.gameObject.CompareTag("Spike"))
            {
                playercontrol.kill();
            } 
        }
    }

}
