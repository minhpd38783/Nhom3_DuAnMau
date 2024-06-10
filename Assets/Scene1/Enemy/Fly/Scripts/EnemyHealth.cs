using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
            currentHealth -= damage;
            // Ki?m tra xem bot ?� h?t m�u ch?a
            if (currentHealth <= 0)
            {
                Die();
            }
    }

    void Die()
    {
        // Th?c hi?n c�c h�nh ??ng khi bot ch?t, v� d? nh? ph�t �m thanh, k�ch ho?t hi?u ?ng ch?t, ho?c g?i h�m ?? x�a bot kh?i scene.
        Destroy(gameObject);
        this.enabled = false;
    }
}

