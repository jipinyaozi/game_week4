using UnityEngine;

public class TrapWall : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasTipped = false;
    private bool canKill = true;
    private SpriteRenderer sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasTipped || collision.gameObject.CompareTag("Door") && !hasTipped)
        {
            Vector2 contactPoint = collision.contacts[0].point;
            Vector2 center = rb.worldCenterOfMass;
            float direction = contactPoint.x > center.x ? -1f : 1f;

            rb.AddTorque(direction * 5f, ForceMode2D.Impulse);

            hasTipped = true;

            Invoke("FreezeWall", 1.5f);
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void FreezeWall()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        canKill = false;
        sprite.color = new Color(1,1,1,1);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && canKill)
        {
            Movement movement = collision.gameObject.GetComponent<Movement>();
            if (movement != null)
            {
                movement.kill();
            }
            else
            {
                Debug.LogWarning("Movement component not found on the player object!");
            }
        }
    }
}
