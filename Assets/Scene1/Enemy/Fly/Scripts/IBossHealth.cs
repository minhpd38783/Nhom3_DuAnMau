using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBossHealth : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;
    private bool isDead = false; // Bi?n ki?m tra xem bot ?� ch?t ch?a

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!isDead) // Ch? th?c hi?n khi bot ch?a ch?t
        {
            currentHealth -= damage;
            // Ki?m tra xem bot ?� h?t m�u ch?a
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true; // ?�nh d?u l� bot ?� ch?t
        // Th?c hi?n c�c h�nh ??ng khi bot ch?t, v� d? nh? ph�t �m thanh, k�ch ho?t hi?u ?ng ch?t, ho?c g?i h�m ?? x�a bot kh?i scene.
        Destroy(gameObject);
    }
}
