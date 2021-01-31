using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Font AlienFont;
    public Font EnglishFont;

    public void PlayGame()
    {
        GameController.Instance.LoadScene(GameController.eScenes.Tutorial);
    }

    public void SetTextAlien(Text text)
    {
        text.font = AlienFont;
    }

    public void SetTextEnglish(Text text)
    {
        text.font = EnglishFont;
    }
}