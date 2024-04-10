using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBodyDelete : MonoBehaviour
{
    public GameObject leftLeg;
    public GameObject rightLeg;
    public GameObject leftlowerleg;
    public GameObject rightlowerleg;
    public GameObject head;
    public GameObject body;
    public GameObject rightupperarm;
    public GameObject rightlowerarm;
    public GameObject leftupperarm;
    public GameObject leftlowerarm;
    private SpriteRenderer leftLegSR;
    private SpriteRenderer rightLegSR;
    private SpriteRenderer leftlowerlegSR;
    private SpriteRenderer rightlowerlegSR;
    private SpriteRenderer headSR;
    private SpriteRenderer bodySR;
    private SpriteRenderer rightupperarmSR;
    private SpriteRenderer rightlowerarmSR;
    private SpriteRenderer leftupperarmSR;
    private SpriteRenderer leftlowerarmSR;

    public Animator anim;
    bool isHovering; 

    void Start()
    {
        leftLegSR = leftLeg.GetComponent<SpriteRenderer>();
        rightLegSR = rightLeg.GetComponent<SpriteRenderer>();
        leftlowerlegSR = leftlowerleg.GetComponent<SpriteRenderer>();
        rightlowerlegSR = rightlowerleg.GetComponent<SpriteRenderer>();
        headSR = head.GetComponent<SpriteRenderer>();
        bodySR = body.GetComponent<SpriteRenderer>();
        rightupperarmSR = rightupperarm.GetComponent<SpriteRenderer>();
        rightlowerarmSR = rightlowerarm.GetComponent<SpriteRenderer>();
        leftupperarmSR = leftupperarm.GetComponent<SpriteRenderer>();
        leftlowerarmSR = leftlowerarm.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isHovering)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("highlight"))
            {
                Debug.Log("Hover");
                anim.Play("highlight");
            }
            if (Input.GetKey(KeyCode.T)) {
                Debug.Log("Delete");
                Destroy(gameObject);
            }
        }
    }

    void OnMouseOver()
    {
        isHovering = true;
    }

    void OnMouseExit()
    {
        isHovering = false;
        anim.Play("death");
    }
}
