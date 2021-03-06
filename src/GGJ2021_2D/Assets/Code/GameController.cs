﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public enum eScenes
    {
        MainMenu,
        Tutorial,
        Game,
        EndCredits
    }

    public static GameController Instance { get; private set; }

    public delegate void SceneLoadEvent(eScenes scene);
    public static event SceneLoadEvent OnSceneLoad;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnUnitySceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnUnitySceneLoaded;
    }



    //------------------------
    // Pubilc calls

    public void Quit()
    {
        Application.Quit();
    }



    //------------------------
    // Scene loading

    private eScenes m_nextScene;

    public void LoadScene(eScenes newScene)
    {
        m_nextScene = newScene;

        switch (newScene)
        {
            case eScenes.Tutorial:
                StartCoroutine(LoadScene(1));
                break;

            case eScenes.Game:
                StartCoroutine(LoadScene(2));
                break;

            case eScenes.EndCredits:
                StartCoroutine(LoadScene(3));
                break;
        }
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        yield return null;

        const float FADE_T = 0.5F;

        // Fade to black
        UIController.Instance.FadeToBlack(FADE_T);
        yield return new WaitForSeconds(FADE_T);

        // Async scene load
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        // Unfade 
        // Fade to black
        UIController.Instance.FadeFromBlack(FADE_T);
    }

    private void OnUnitySceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnSceneLoad.Invoke(m_nextScene);
    }
}