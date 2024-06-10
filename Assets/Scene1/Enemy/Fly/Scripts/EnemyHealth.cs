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
            // Ki?m tra xem bot ?ã h?t máu ch?a
            if (currentHealth <= 0)
            {
                Die();
            }
    }

    void Die()
    {
        // Th?c hi?n các hành ??ng khi bot ch?t, ví d? nh? phát âm thanh, kích ho?t hi?u ?ng ch?t, ho?c g?i hàm ?? xóa bot kh?i scene.
        Destroy(gameObject);
        this.enabled = false;
    }
}

