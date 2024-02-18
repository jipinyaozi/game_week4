using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public float targetRotation;
    public Rigidbody2D rb;
    public float force;
    private bool isActive = true; // Now the variable is private
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return; // If not active, don't apply rotation
        rb.MoveRotation(Mathf.LerpAngle(rb.rotation, targetRotation, force * Time.deltaTime));
    }
    public void SetActiveBalance(bool active)
    {
        isActive = active;
        if (!active)
        {
            rb.angularVelocity = 0; // Optionally reset angular velocity when deactivated
        }
    }
}
