using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildCollision : MonoBehaviour
{
    private Movement playerscript;
    // Start is called before the first frame update
    void Start()
    {
        playerscript = transform.parent.GetComponent<Movement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
