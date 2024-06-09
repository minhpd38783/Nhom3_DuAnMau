using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 30;
    public int phase2AttackDamage = 50;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    [SerializeField] AudioSource painSource;
    [SerializeField] AudioClip painClip;

    public void AttackPhase1()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<Player>().TakeDamage(attackDamage);
            StartCoroutine(PlayPainSound());
        }
    }
    public void AttackPhase2()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            colInfo.GetComponent<Player>().TakeDamage(phase2AttackDamage);
            StartCoroutine(PlayPainSound());
        }
    }
    private IEnumerator PlayPainSound()
    {
        painSource.PlayDelayed(0.17f);
        yield return new WaitForSeconds(0.2f);
    }
    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
