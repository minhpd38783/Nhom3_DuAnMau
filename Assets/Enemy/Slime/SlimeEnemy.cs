using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(WaitToFlip());
    }
    void FixedUpdate()
    {
        if(facingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    IEnumerator WaitToFlip()
    {
        yield return new WaitForSeconds(3f);
        facingRight = !facingRight;
        StartCoroutine(WaitToFlip());
    }
}
