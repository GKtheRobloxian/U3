using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public GameObject lighting;
    public GameObject GameOver;
    public bool intro = true;
    float introTimer = 2.5f;
    float intTimer;
    public float score;
    float scoreTimer = 1.0f;
    float timer;
    public int jumpAmount;
    int jump;
    public GameObject cans;
    UltraInstinct ultra;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirt;
    public AudioClip jumpSound;
    public AudioClip crash;
    AudioSource playerAudio;
    public bool gameOver = false;
    Animator player;
    Rigidbody rb;
    public float JumpForce = 300f;
    public float gravity;
    bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        intTimer = introTimer;
        ultra = cans.GetComponent<UltraInstinct>();
        timer = scoreTimer;
        jump = jumpAmount;
        playerAudio = GetComponent<AudioSource>();
        player = GetComponent<Animator>();
        Physics.gravity *= gravity;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        intTimer -= Time.deltaTime;
        if (intTimer > 0)
        {
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, -3.3f, 0.01f), transform.position.y, transform.position.z);
        }
        if (intTimer < 0)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                lighting.SetActive(false);
            }
            else
            {
                lighting.SetActive(true);
            }
            intro = false;
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                score += 1;
                timer = scoreTimer;
            }
        }
        if (!gameOver)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isOnGround)
            {
                rb.AddForce(Vector3.down * 50f, ForceMode.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                isOnGround = false;
                player.SetTrigger("Jump_trig");
                dirt.Stop();
                playerAudio.PlayOneShot(jumpSound, 1.0f);
                jump -= 1;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && isOnGround == false && jump > 0)
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
                isOnGround = false;
                dirt.Stop();
                playerAudio.PlayOneShot(jumpSound, 1.0f);
                jump -= 1;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && isOnGround == false && jump == 0)
            {
                return;
            }
            ultra.SetValue(score);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirt.Play();
            jump = jumpAmount;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            GameOver.SetActive(true);
            player.SetBool("Death_b", true);
            player.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirt.Stop();
            playerAudio.PlayOneShot(crash, 1.0f);
        }
    }
}
