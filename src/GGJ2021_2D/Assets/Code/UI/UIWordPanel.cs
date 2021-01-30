using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIWordPanel : MonoBehaviour
{
    [Header("English")]
    public TextMeshProUGUI m_englishText;
    public TextMeshProUGUI m_alienText;
    public Color m_unknownWordColour;
    public Color m_knownWordColour;

    public void Setup(string word)  
    {
        // Set the alien text 
        m_alienText.text = word;

        // Set english text if the translation is known
        if (LanguageManager.Instance.IsWordLearned(word))
        {
            m_englishText.text = word;
            m_alienText.color = m_knownWordColour;
        }
        else
        {
            m_englishText.text = "";
            m_alienText.color = m_unknownWordColour;
        }    
    }
}