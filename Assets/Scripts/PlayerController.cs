using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem dirtParticle;
    public ParticleSystem explosionParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 5;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI instructionsText;

    public bool isGameActive = true;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {

            instructionsText.gameObject.SetActive(false);

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim = GetComponent<Animator>();
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
            score++;
            scoreText.text = "Score: " + score;
        }
        else if (collision.gameObject.CompareTag("Boundary"))
        {
             Debug.Log("Game Over");
              gameOver = true;
               playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                explosionParticle.Play();
            gameOverText.gameObject.SetActive(true);
            isGameActive = false;
        }

    }
}
