using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] Slider bossHealthBar;
    [SerializeField] Collider2D bossCol;
    [SerializeField] Rigidbody2D bossRb;
    [SerializeField] GameObject bossHealth;
    [SerializeField] GameObject enablePortal;

    public Animator bossAnim;
    public int health = 1000;
    public int currentHealth;
    public bool isInvunerable = false;

    void Start()
    {
        currentHealth = health;
        bossHealthBar.maxValue = health;
        bossHealthBar.value = currentHealth;
    }
    public void TakeDamage(int damage)
    {
        if (isInvunerable)  return; 

        currentHealth -= damage;
        bossHealthBar.value = currentHealth;

        if (currentHealth <= 400)
        {
            GetComponent<Animator>().SetBool("Phase 2", true);
        }

        if (currentHealth <= 0)
        {
            Die();
            bossAnim.SetBool("Death", true);
            StartCoroutine(WaitToOpenPortal());
            Debug.Log("Defeated Boss. Wait to enable Portal ...");
        }
    }
    public void Die()
    {
        bossHealth.SetActive(false);
        bossRb.simulated = false;
        bossCol.enabled = false;
    }
    IEnumerator WaitToOpenPortal()
    {
        yield return new WaitForSeconds(3f);
        enablePortal.SetActive(true);
        Debug.Log("Portal enabled");
    }
}
