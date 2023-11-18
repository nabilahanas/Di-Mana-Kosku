using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    ScoreManager sm;
    AudioManager audioManager;

    public float speed = 5.0f;
    public AudioSource audioPlayer;
    public GameObject barrier;
    public GameObject endGame;
    public Sprite[] sprites; // Masukkan sprite-sprite ke sini melalui Inspector
    private SpriteRenderer spriteRenderer;

    private bool isGameWon = false; // Udah menang atau belum?

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        sm = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Hentikan gerakan jika pemain sudah menang
        if (!isGameWon)
        {
            HandlePlayerMovement();
        }
        // Handle input jika pemain sudah menang
        if (isGameWon)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    private void HandlePlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(movement * speed * Time.deltaTime);

        if (horizontalInput > 0)
        {
            // Ke kanan
            ChangeSprite(0);
        }
        else if (horizontalInput < 0)
        {
            // Ke kiri
            ChangeSprite(1);
        }
        else if (verticalInput > 0)
        {
            // Ke atas
            ChangeSprite(2);
        }
        else if (verticalInput < 0)
        {
            // Ke bawah
            ChangeSprite(3);
        }
    }

    private void ChangeSprite(int spriteIndex)
    {
        if (spriteIndex >= 0 && spriteIndex < sprites.Length)
        {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Kalo dapet kunci
        if (collision.gameObject.tag == "Keys")
        {
            Destroy(collision.gameObject);
            sm.keyAmount--;
            audioManager.PlaySFX(audioManager.key);
        }
        // Kalo nabrak dinding
        if (collision.gameObject.tag == "Walls")
        {
            sm.wallHit--;
            audioManager.PlaySFX(audioManager.wallHit);
        }
        // Kalo kuncinya udah lengkap
        if (sm.keyAmount == 0)
        {
            Destroy(barrier);
        }
        // Kalo sampe kos
        if (collision.gameObject.tag == "Kos")
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        endGame.gameObject.SetActive(true);
        audioManager.PlaySFX(audioManager.finish);
        isGameWon = true;
    }
}