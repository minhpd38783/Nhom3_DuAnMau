using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour

{
    public float speed = 3.0f;
    public float moveDistance = 5.0f;

    private Vector3 startPos;
    private float destinationX;
    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position;
        destinationX = startPos.x + moveDistance; // Xác định điểm đích
    }

    void Update()
    {
        // Di chuyển bot
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        // Kiểm tra xem bot đã đến điểm đích chưa
        if ((movingRight && transform.position.x >= destinationX) || (!movingRight && transform.position.x <= startPos.x))
        {
            // Nếu đến điểm đích, đổi hướng
            movingRight = !movingRight;
            // Lật hướng
            Flip();
        }
    }

    // Hàm lật hướng (Flip)
    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

