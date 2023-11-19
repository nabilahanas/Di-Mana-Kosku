using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text wallHitText;
    public int wallHit;
    public TMP_Text keyAmountText;
    public int keyAmount = 5;

    void Start()
    {
        // Mengecek apakah PlayerPrefs memiliki nilai untuk wallHit
        if (PlayerPrefs.HasKey("WallHit"))
        {
            wallHit = PlayerPrefs.GetInt("WallHit");
        }

        // Mengecek apakah PlayerPrefs memiliki nilai untuk keyAmount
        if (PlayerPrefs.HasKey("KeyAmount"))
        {
            keyAmount = PlayerPrefs.GetInt("KeyAmount");
        }

        UpdateUIText(); // Memastikan teks UI sudah diatur dengan nilai awal
    }

    void Update()
    {
        UpdateUIText();
    }

    private void UpdateUIText()
    {
        // Fungsi untuk memastikan teks UI sudah diatur dengan nilai awal
        if (wallHitText != null && keyAmountText != null)
        {
            wallHitText.text = "Wall Hit: " + wallHit.ToString();
            keyAmountText.text = "Key Amount: " + keyAmount.ToString();
        }
    }
}