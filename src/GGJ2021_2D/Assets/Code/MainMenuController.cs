using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        GameController.Instance.LoadScene(GameController.eScenes.Tutorial);
    }
}