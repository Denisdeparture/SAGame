using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Springing : MonoBehaviour
{
    private Animator animspr;
    private bool contactTrigger = true;
    public float downForce = 500f;
    public float yOffset = 1f;
    public float xOffset = 0;
    public AudioClip clip;
    private AudioSource source;
    private void Awake()
    {
        animspr = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        {
            if (collision.gameObject.name.ToLower() == "player".ToLower() | collision.gameObject.name.ToLower() == "enemy".ToLower())
            {
                source.PlayOneShot(clip);

                animspr.SetBool("contact", contactTrigger);

                var direction = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90) * new Vector2(xOffset, yOffset);

                var rb_2D = collision.gameObject.GetComponent<Rigidbody2D>();

                rb_2D.AddForce(direction * downForce, ForceMode2D.Impulse);
            }
           
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        animspr.SetBool("contact", !contactTrigger);
    }
}