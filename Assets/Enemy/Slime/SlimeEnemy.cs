using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    // Speed of the enemy movement
    public float speed = 2.0f;

    // Distance the enemy will move from the starting position
    public float distance = 5.0f;

    // Starting position of the enemy
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        // Save the starting position of the enemy
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new position using PingPong to move back and forth
        float newPos = Mathf.PingPong(Time.time * speed, distance);

        // Set the new position
        transform.position = startPos + new Vector3(newPos, 0, 0);
    }
}