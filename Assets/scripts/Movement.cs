using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
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
    public GameObject leftFist;
    public GameObject rightFist;
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
    Rigidbody2D bodyRB;

    public bodybalance bodybalance;
    public GameObject dead;
    private SpriteRenderer deadRenderer;
    private Color deadColor;
    public AudioSource audio;
    public bool isDead = false;
    GameObject clone;
    private int deathCount = 0;
    private DeathCounter deathCounter;
    private bool isHovering = false;
    public DeadBodyDelete delete;
    bool crouch = false;

    private int level1Deaths = 0;
    private int level2Deaths = 0;

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
        bodyRB = body.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ps = FindObjectOfType<ParticleSystem>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        deadRenderer = dead.GetComponent<SpriteRenderer>();
        deadColor = deadRenderer.color;
        deathCounter = GameObject.Find("DeathCounter").GetComponent<DeathCounter>();
        delete.enabled = false;
    }
    void FixedUpdate()
    {
        if(this.tag != "dead")
        {
            if (crouch)
            {
                rightLegRB.AddForce(Vector2.down * 200);
                rightLegRB.AddForce(Vector2.right * 40);
                leftLegRB.AddForce(Vector2.down * 200);
                leftLegRB.AddForce(Vector2.left * 40);
            }
        }
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
                    // leftLegRB.AddForce(Vector2.up * (jumpHeight*1000));
                    // rightLegRB.AddForce(Vector2.up * (jumpHeight * 1000));
                    bodyRB.AddForce(Vector2.up * (jumpHeight * 2500));

                }
            }
            if(Input.GetKeyDown(KeyCode.Z))
            {
                kill();
            }
            //Crouch
            if (Input.GetKeyDown(KeyCode.S))
            {
                crouch = true;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                crouch = false;
            }
        } 
        
        if(body.tag == "dead"){
            bodybalance.force = 0;
        }
    }
    public void padjump()
    {
        bodyRB.AddForce(Vector2.up * 2000);
    }
    public void changetag()
    {
        this.tag = "dead";
        leftLeg.tag = "dead";
        leftlowerleg.tag = "dead";
        rightLeg.tag = "dead";
        rightlowerleg.tag = "dead";
        head.tag = "dead";
        body.tag = "dead";
        leftlowerarm.tag = "dead";
        leftupperarm.tag = "dead";
        rightlowerarm.tag = "dead";
        rightupperarm.tag = "dead";
        leftFist.tag = "dead";
        rightFist.tag = "dead";
    }

    public void changelayer()
    {
        leftLeg.layer = LayerMask.NameToLayer("Default");
        leftlowerleg.layer = LayerMask.NameToLayer("Default");
        rightLeg.layer = LayerMask.NameToLayer("Default");
        rightlowerleg.layer = LayerMask.NameToLayer("Default");
        head.layer = LayerMask.NameToLayer("Default");
        body.layer = LayerMask.NameToLayer("Default");
        leftlowerarm.layer = LayerMask.NameToLayer("Default");
        leftupperarm.layer = LayerMask.NameToLayer("Default");
        rightlowerarm.layer = LayerMask.NameToLayer("Default");
        rightupperarm.layer = LayerMask.NameToLayer("Default");
        leftFist.layer = LayerMask.NameToLayer("Default");
        rightFist.layer = LayerMask.NameToLayer("Default");
    }
    public void changemass()
    {
        leftLeg.GetComponent<Rigidbody2D>().mass = 0.05f;
        leftlowerleg.GetComponent<Rigidbody2D>().mass = 0.05f;
        rightLeg.GetComponent<Rigidbody2D>().mass = 0.05f;
        rightlowerleg.GetComponent<Rigidbody2D>().mass = 0.05f;
        head.GetComponent<Rigidbody2D>().mass = 0.05f;
        body.GetComponent<Rigidbody2D>().mass = 0.05f;
        leftlowerarm.GetComponent<Rigidbody2D>().mass = 0.05f;
        leftupperarm.GetComponent<Rigidbody2D>().mass = 0.05f;
        rightlowerarm.GetComponent<Rigidbody2D>().mass = 0.05f;
        rightupperarm.GetComponent<Rigidbody2D>().mass = 0.05f;
    }

    public void kill()
    {
        if (this.tag != "dead")
        {
            this.tag = "dead";
            delete.enabled = true;
            if (heldBody)
            {
                ThrowBody();
            }
            audio.Play();
            anim.Play("death");
            ps.Play();

            IC.enabled = false;
            deathCount++;
            deathCounter.PlayerDied();

            // Find the active checkpoint, if any
            Checkpoint activeCheckpoint = FindActiveCheckpoint();

            // Instantiate the clone at the active checkpoint or spawn point
            clone = InstantiateAtPosition(activeCheckpoint);
            leftFist.SetActive(false);
            rightFist.SetActive(false);

            changelayer();
            changemass();

            StartCoroutine(wait());
        }
    }

    // Utility method to find the active checkpoint
    private Checkpoint FindActiveCheckpoint()
    {
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (checkpoint.isActive)
            {
                return checkpoint;
            }
        }
        return null; // No active checkpoint found
    }

    // Utility method to instantiate the clone at the correct position
    private GameObject InstantiateAtPosition(Checkpoint activeCheckpoint)
    {
        if (activeCheckpoint != null)
        {
            Debug.Log("Clone spawned at checkpoint");
            return Instantiate(newplayer, activeCheckpoint.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Clone spawned at spawn point");
            return Instantiate(newplayer, SpawnPoint.position, Quaternion.identity);
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        changetag();
        clone.tag = "Player";

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
        float pickupDistance = 1.5f; // Adjust as needed
        if (Vector2.Distance(transform.position, deadBody.transform.position) <= pickupDistance)
        {
            IgnoreCollision ignorecol;
            heldBody = deadBody;
            isHoldingBody = true;
            heldBody.transform.SetParent(this.transform);
            ignorecol = heldBody.transform.parent.GetComponent<IgnoreCollision>();
            ignorecol.enabled = true;

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
