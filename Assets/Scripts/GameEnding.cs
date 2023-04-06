using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 2f;
    public CanvasGroup imageGanarGame;
    public CanvasGroup caughtBackgroundImageCanvasGroup;

    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject gameOverCaughtCanvas;
    [SerializeField] private AudioSource audioSourceWin;
    [SerializeField] private AudioSource audioSourceGameOver;
    [SerializeField] private AudioSource audioSourceBGStopping;
    [SerializeField] private PlayerMove playerController;

    [SerializeField] bool m_IsPlayerAtExit;
    [SerializeField] bool m_IsPlayerCaught;
    bool m_HasAudioPlayed;

    float m_timer;

    private void Start()
    {
        gameCanvas.SetActive(false);
        gameOverCaughtCanvas.SetActive(false);
    }

    private void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel(imageGanarGame, gameCanvas, audioSourceWin);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, gameOverCaughtCanvas, audioSourceGameOver);
        }
    }

    private void EndLevel(CanvasGroup imageCanvasGroup, GameObject objCanvasMenu, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSourceBGStopping.Stop();
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
        Cursor.lockState = CursorLockMode.None;
        playerController.notCanMove = true;
        m_timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_timer / fadeDuration;
        if (m_timer > fadeDuration + displayImageDuration)
        {
            imageCanvasGroup.alpha = 0;
            objCanvasMenu.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }
}
