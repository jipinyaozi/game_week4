using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class legjumpcontrol : MonoBehaviour
{
    public GameObject player;
    Movement playercontrol;
    bool jumpable;
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
        playercontrol.jumpable = true;
        if(this.tag != "dead"){
            if(col.gameObject.CompareTag("Spike"))
            {
                playercontrol.kill();
            } 
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        playercontrol.jumpable = false;
    }

}
