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
    [SerializeField] private float throwStrength = 10f; // Adjust according to your game's needs.
    [SerializeField] private float upwardThrowStrength = 5f; // Adjust for the vertical component of the throw.

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
            if (Input.GetKeyDown(KeyCode.E)&&!heldBody)
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
        heldBody.transform.SetParent(this.transform);

        // Determine if the body is on the player's left or right by comparing their positions
        float xOffset = Mathf.Abs(1.5f); // Absolute value for the horizontal offset
        float yOffset = -1.5f; // Vertical offset, adjust as needed
        if (heldBody.transform.position.x < transform.position.x)
        {
            // Body is to the player's left
            xOffset = -xOffset;
        }

        heldBody.transform.localPosition = new Vector2(xOffset, yOffset);

        Rigidbody2D rb = heldBody.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
        }
    }



    void ThrowBody()
    {
        if (heldBody != null)
        {
            Rigidbody2D rb = heldBody.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = false;

                // Use the local position since the body's global position is affected by being a child
                Vector2 throwDirection = heldBody.transform.localPosition.x < 0 ? Vector2.left : Vector2.right;
                rb.AddForce(throwDirection * throwStrength + Vector2.up * upwardThrowStrength, ForceMode2D.Impulse);
            }

            heldBody.transform.SetParent(null);
            heldBody = null;
            isHoldingBody = false;
        }
    }


}
