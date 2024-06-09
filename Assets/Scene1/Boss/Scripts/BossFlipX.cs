using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlipX : MonoBehaviour
{
    public Transform player;

    public bool isFlipX = true;

    public void LootAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && !isFlipX)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipX = true;
        }
        else if (transform.position.x < player.position.x && isFlipX)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipX = false;
        }
    }
}
