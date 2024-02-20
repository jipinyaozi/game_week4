using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumppad : MonoBehaviour
{
    [SerializeField] private float bounce = 20f;
    bool active = true;
    public AudioSource sound;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(active)
        {
            sound.Play();
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            active = false;
        }

        else
        {
            StartCoroutine(wait());
            active = true;

        }
    }
    
    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
    }

}
