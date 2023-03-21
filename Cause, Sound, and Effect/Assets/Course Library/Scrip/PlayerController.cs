using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject GameOver;
    public bool gameOver = false;
    Animator player;
    Rigidbody rb;
    public float JumpForce = 300f;
    public float gravity;
    bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Animator>();
        Physics.gravity *= gravity;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isOnGround = false;
            player.SetTrigger("Jump_trig");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            GameOver.SetActive(true);
            player.SetBool("Death_b", true);
            player.SetInteger("DeathType_int", 1);
        }
    }
}
