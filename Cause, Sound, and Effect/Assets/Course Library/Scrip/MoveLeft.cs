using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float initialSpeed = 20;
    GameObject player;
    float speed;
    PlayerController control;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = initialSpeed;
        control = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (control.gameOver == false && control.intro == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            if (Input.GetKey(KeyCode.LeftControl))
            {
                Time.timeScale = 0.25f;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        if (transform.position.x < -15 && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
