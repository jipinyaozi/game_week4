using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_Spike : MonoBehaviour
{
    [SerializeField]
    public float speed = 5.0f;
    public GameObject player;
    private bool moveable = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player.transform.position.y >= 12 && player != null)
        {
            moveable = true;        
        }
        if (moveable)
        {
            movespike();
        }
    }
    void movespike()
    {
        transform.Translate(new Vector2 (0f, 1f) * speed * Time.deltaTime);        
    }
}
