using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPlus : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("CollectedHeart");
            Destroy(gameObject, 0.2f);
        }
    }

}
