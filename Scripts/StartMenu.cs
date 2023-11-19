using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public AudioManager audioManager;
    public GameObject startScene;
    public GameObject score;

    Player pl;

    public void Start()
    {
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (score != null)
        {
            score.SetActive(false);
        }
    }

    public void Update()
    {
        // Enable the Player script
        if (pl != null)
        {
            pl.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Memainkan suara sebelum pindah ke scene berikutnya
            PlayTransitionSound();

            // Menyembunyikan UI setelah suara selesai
            StartCoroutine(HideUI());
        }
    }

    void PlayTransitionSound()
    {
        if (audioManager != null)
        {
            audioManager.PlaySFX(audioManager.start);
        }
    }

    private IEnumerator HideUI()
    {
        // Menunggu beberapa detik sebelum pindah ke scene berikutnya
        yield return new WaitForSeconds(0.2f);

        // Menonaktifkan GameObject UI
        if (startScene != null)
        {
            startScene.SetActive(false);
        }

        if (pl != null)
        {
            pl.enabled = true;
        }

        if (score != null)
        {
            score.SetActive(true);
        }
    }
}