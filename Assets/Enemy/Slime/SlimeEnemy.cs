using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    // Speed of the enemy movement
    public float speed = 5.0f;

    // Distance the enemy will move from the starting position
    public float distance = 5.0f;

    // Reference to the Rigidbody2D component
    private Rigidbody2D rb;

    // Starting position of the enemy
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        // Save the starting position of the enemy
        startPos = transform.position;

        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new position using PingPong to move back and forth
        float newPos = Mathf.PingPong(Time.time * speed, distance) - (distance / 2);

        // Set the new position
        rb.MovePosition(new Vector2(startPos.x + newPos, startPos.y));
    }
}
