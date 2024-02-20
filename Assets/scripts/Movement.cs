using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


    public GameObject leftLeg;
    public GameObject rightLeg;
    public GameObject leftlowerleg;
    public GameObject rightlowerleg;
    public GameObject head;
    public GameObject body;
    public GameObject rightuperarm;
    public GameObject rightlowerarm;
    public GameObject leftupperarm;
    public GameObject leftlowerarm;
    Rigidbody2D leftLegRB;
    Rigidbody2D rightLegRB;
    public bool jumpable = false;
    private ParticleSystem ps;
    public GameObject newplayer;
    public Transform SpawnPoint;
    public IgnoreCollision IC;
    private GameObject heldBody = null;
    private bool isHoldingBody = false;
    private SpriteRenderer playerSpriteRenderer;


    Animator anim;
    [SerializeField] float speed = 2f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float legWait = .5f;
    // Start is called before the first frame update
    void Start()
    {
        leftLegRB = leftLeg.GetComponent<Rigidbody2D>();
        rightLegRB = rightLeg.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ps = FindObjectOfType<ParticleSystem>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        if(this.tag != "dead")
        {
            if(Input.GetAxisRaw("Horizontal") != 0)
            {
                if(Input.GetAxis("Horizontal") > 0)
                {
                    anim.Play("WalkRight");
                    StartCoroutine(MoveRight(legWait));
                }
                else
                {
                    anim.Play("WalkLeft");
                    StartCoroutine(MoveLeft(legWait));
                
                }
                
            }
            else
            {
                anim.Play("idle");
            }
            if(jumpable)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    jumpable = false;
                    leftLegRB.AddForce(Vector2.up * (jumpHeight*1000));
                    rightLegRB.AddForce(Vector2.up * (jumpHeight * 1000));

                }
            }
            // Check if E key is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E key pressed"); // Debug message for key press

                // Check for dead bodies in a certain radius
                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 5f); // Adjust the radius as needed
                bool deadBodyDetected = false;

                foreach (var hit in hits)
                {
                    // Check if we hit a dead body
                    if (hit.CompareTag("dead"))
                    {
                        Debug.Log("Dead body detected!"); // Debug message for detection
                        PickUpBody(hit.gameObject);
                        deadBodyDetected = true;
  
                        break;
                    }
                }

                if (!deadBodyDetected)
                {
                    Debug.Log("No dead body detected"); // Debug message if nothing is detected

                }


            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("F key pressed");
                if (isHoldingBody)
                {
                    Debug.Log("ready to throw");
                    ThrowBody();
                }
            }

        }
       
    }

    public void changetag()
    {
        this.tag = "dead";
        leftLeg.tag = "dead";
        leftlowerleg.tag = "dead";
        rightLeg.tag = "dead";
        rightlowerleg.tag = "dead";
        head.tag = "dead";
        body.tag = "Door";
        leftlowerarm.tag = "dead";
        leftupperarm.tag = "dead";
        rightlowerarm.tag = "dead";
        rightuperarm.tag = "dead";
    }

    public void kill()
    {
        if(this.tag != "dead")
        {
            if (heldBody)
            {
                ThrowBody();
            }
            IC.enabled = false;
            Instantiate(newplayer, new Vector3(-8, 0, 0), Quaternion.identity);
            leftLeg.GetComponent<Balance>().force = 0;
            rightLeg.GetComponent<Balance>().force = 0;
            leftlowerleg.GetComponent<Balance>().force = 0;
            rightlowerleg.GetComponent<Balance>().force = 0;
            body.GetComponent<Balance>().force = 0;
            head.GetComponent<Balance>().force = 0;
            ps.Play();
            changetag();
        }
    }

    IEnumerator MoveRight(float seconds)
    {
        leftLegRB.AddForce(Vector2.right * (speed*1000) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        rightLegRB.AddForce(Vector2.right * (speed * 1000) * Time.deltaTime);
    }

    IEnumerator MoveLeft(float seconds)
    {
        rightLegRB.AddForce(Vector2.left * (speed * 1000) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        leftLegRB.AddForce(Vector2.left * (speed * 1000) * Time.deltaTime);
    }
    void PickUpBody(GameObject deadBody)
    {
    heldBody = deadBody;
    isHoldingBody = true;
    // Make the dead body a child of the player to carry it
    heldBody.transform.SetParent(this.transform);
        

    // Set an offset for the dead body position relative to the player
    // Adjust the values to position the dead body where you want it relative to the player
    float yOffset = -1.0f; // Half the character's height or whatever looks good
    heldBody.transform.localPosition = new Vector3(2.0f, yOffset, 2.0f);
        
    // Optionally, disable the Rigidbody2D to prevent physics forces from affecting it while being carried
    Rigidbody2D rb = heldBody.GetComponent<Rigidbody2D>();
    if (rb != null)
    {
        rb.isKinematic = true;
        rb.velocity = Vector2.zero; // Stop any movement
    }
    }

    void ThrowBody()
    {
        if (heldBody != null)
        {
            Rigidbody2D rb = heldBody.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = false; // Make sure physics applies again

                // Apply a force to simulate throwing
                rb.AddForce(transform.up * 10f + transform.right * 10f, ForceMode2D.Impulse); // Adjust direction/force as needed
            }

            // Unparent the dead body
            heldBody.transform.SetParent(null);

            // Reset these so the player can pick up again later
            heldBody = null;
            isHoldingBody = false;
        }
    }

}
