using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public AudioSource startSound;

    public void Start()
    {
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Memainkan suara sebelum pindah ke scene berikutnya
            PlayTransitionSound();

            // Pindah ke scene berikutnya setelah suara selesai
            StartCoroutine(LoadNextScene());
        }
    }

    void PlayTransitionSound()
    {
        if (startSound != null)
        {
            startSound.Play();
        }
    }

    private IEnumerator LoadNextScene()
    {
        // Menunggu beberapa detik sebelum pindah ke scene berikutnya
        yield return new WaitForSeconds(startSound.clip.length);

        // Pindah ke scene berikutnya
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}