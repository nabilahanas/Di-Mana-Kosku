using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    ScoreManager sm;

    public float speed = 5.0f;
    public AudioSource audioPlayer;
    public GameObject barrier;
    public GameObject endGame;

    AudioManager audioManager;

    private bool isGameWon = false; // Udah menang atau belum?

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
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
                SceneManager.LoadScene(1);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }

    private void HandlePlayerMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
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