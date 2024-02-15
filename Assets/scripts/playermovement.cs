using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    private Rigidbody2D rb;
    bool jumpable;
    private ParticleSystem ps;
    public bool dead = false;
    public GameObject player;
    public Transform SpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ps = FindObjectOfType<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!dead){
            float dirX = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(dirX * 7f, rb.velocity.y);
            if(Input.GetKeyDown("space"))
            {
                if(jumpable)
                {
                jumpable = false;
                rb.velocity = new Vector2(rb.velocity.x, 4f);
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        jumpable = true;
        if(!dead){
            if(col.gameObject.CompareTag("Spike"))
            {
                kill();
            } 
        }
    }

    private void kill()
    {
        Instantiate(player, SpawnPoint.position, Quaternion.identity);
        dead = true;
        ps.Play();
    }

}
