using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IBossHealth : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;
    private bool isDead = false; // Bi?n ki?m tra xem bot ?ã ch?t ch?a

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
            // Ki?m tra xem bot ?ã h?t máu ch?a
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isDead = true; // ?ánh d?u là bot ?ã ch?t
        // Th?c hi?n các hành ??ng khi bot ch?t, ví d? nh? phát âm thanh, kích ho?t hi?u ?ng ch?t, ho?c g?i hàm ?? xóa bot kh?i scene.
        Destroy(gameObject);
    }
}
