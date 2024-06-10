using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject portal;
    private GameObject player;
    [SerializeField] AudioSource teleportSource;
    [SerializeField] AudioClip teleportClip;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        teleportSource.clip = teleportClip;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Dịch chuyển đến vị trí mới đã đặt trước
            player.transform.position = new Vector2(portal.transform.position.x, portal.transform.position.y);
            // Phát âm thanh dịch chuyển
            teleportSource.PlayOneShot(teleportSource.clip);
        }
    }
}
