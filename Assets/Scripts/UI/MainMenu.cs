﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Scene Management")]
    public string gameSceneName;

    [Header("Music Settings")]
    public Button toggleMusicButton;
    public Image musicButtonImage;
    public Sprite musicOn;
    public Sprite musicOff;

    [Header("SFX Settings")]
    public Button toggleSfxButton;
    public Image sfxButtonImage;
    public Sprite sfxOn;
    public Sprite sfxOff;

    [Header("Modal Settings")]
    public float panelDropDuration;
    public float panelDropHeight;

    [Header("How to play")]
    public GameObject tutorialParent;
    public Transform tutorialPanel;
    public Button openTutorialButton;
    public Button closeTutorialButton;

    [Header("Leaderboard")]
    public GameObject leaderboardParent;
    public Transform leaderboardPanel;
    public Button openLeaderboardButton;
    public Button closeLeaderboardButton;

    [Header("About US")]
    public GameObject aboutUsParent;
    public Transform aboutUsPanel;
    public Button openAboutUsButton;
    public Button closeAboutUsButton;

    private void Start()
    {
        toggleMusicButton.onClick.AddListener(() => ToggleMusic());
        toggleSfxButton.onClick.AddListener(() => ToggleSfx());
        openTutorialButton.onClick.AddListener(() => OpenTutorial());
        closeTutorialButton.onClick.AddListener(() => CloseTutorial());
        openLeaderboardButton.onClick.AddListener(() => OpenLeaderboard());
        closeLeaderboardButton.onClick.AddListener(() => CloseLeaderboard());
        openAboutUsButton.onClick.AddListener(() => OpenAboutUs());
        closeAboutUsButton.onClick.AddListener(() => CloseAboutUs());

        UpdateMusicButton();
        UpdateSfxButton();

        AudioManager.instance?.StopAll();
        AudioManager.instance?.Play("MenuTheme");

        tutorialParent.SetActive(false);
        leaderboardParent.SetActive(false);
        aboutUsParent.SetActive(false);
    }

    public void OpenGameScene() {
        AudioManager.instance?.Stop("MenuTheme");
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }

    public void ToggleMusic()
    {
        AudioManager.instance?.ToggleMusic();
        UpdateMusicButton();
    }

    public void ToggleSfx()
    {
        AudioManager.instance?.ToggleSfx();
        UpdateSfxButton();
    }

    private void UpdateMusicButton()
    {
        if((bool) AudioManager.instance?.IsMusicOn)
        {
            musicButtonImage.sprite = musicOn;
        }
        else
        {
            musicButtonImage.sprite = musicOff;
        }
    }

    private void UpdateSfxButton()
    {
        if((bool) AudioManager.instance?.AreSfxOn)
        {
            sfxButtonImage.sprite = sfxOn;
        }
        else
        {
            sfxButtonImage.sprite = sfxOff;
        }
    }

    private void OpenTutorial()
    {
        tutorialParent.SetActive(true);
        StartCoroutine(SimpleAnimations.instance.Translate(tutorialPanel, panelDropDuration, new Vector3(0f, -1 * panelDropHeight, 0f)));
    }

    private void CloseTutorial()
    {
        StartCoroutine(SimpleAnimations.instance.Translate(tutorialPanel, panelDropDuration, new Vector3(0f, panelDropHeight, 0f), 1f, () => tutorialParent.SetActive(false)));
    }

    private void OpenLeaderboard()
    {
        leaderboardParent.SetActive(true);
        StartCoroutine(SimpleAnimations.instance.Translate(leaderboardPanel, panelDropDuration, new Vector3(0f, -1 * panelDropHeight, 0f)));
        leaderboardParent.GetComponent<LeaderboardController>().ResetLeaderboard();
    }

    private void CloseLeaderboard()
    {
        StartCoroutine(SimpleAnimations.instance.Translate(leaderboardPanel, panelDropDuration, new Vector3(0f, panelDropHeight, 0f), 1f, () => leaderboardParent.SetActive(false)));
    }

    private void OpenAboutUs()
    {
        aboutUsParent.SetActive(true);
        StartCoroutine(SimpleAnimations.instance.Translate(aboutUsPanel, panelDropDuration, new Vector3(0f, -1 * panelDropHeight, 0f)));
    }

    private void CloseAboutUs()
    {
        StartCoroutine(SimpleAnimations.instance.Translate(aboutUsPanel, panelDropDuration, new Vector3(0f, panelDropHeight, 0f), 1f, () => aboutUsParent.SetActive(false)));
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
