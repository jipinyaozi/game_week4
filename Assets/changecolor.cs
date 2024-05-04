using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changecolor : MonoBehaviour
{
    public SpriteRenderer sprd;
    public KeyCode mousebutton;
    // Start is called before the first frame update
    void Start()
    {
        sprd = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(mousebutton))
            {
                sprd.color = Color.yellow;
            }
        else
        {
            sprd.color = Color.white;
        }

        
    }
}
