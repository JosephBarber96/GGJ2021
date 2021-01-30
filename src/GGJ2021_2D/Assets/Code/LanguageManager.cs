using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    //---------------------------
    // Class definitions 

    public class WordProgression
    {
        public LanguageWord m_word;
        public bool m_isUnlocked;
    }




    //---------------------------
    // Vars

    public static LanguageManager Instance { get; private set; }

    [Header("Language Manager")]
    public LanguageDictionary m_Dictionary;

    private List<WordProgression> m_wordProgression = new List<WordProgression>();




    //---------------------------
    // Monobehaviour 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        Init();
    }




    //---------------------------
    // Public 

    public void LearnWord(LanguageWord word)
    {
        for (int i = 0; i < m_wordProgression.Count; i++)
        {
            if (m_wordProgression[i].m_word == word)
            {
                if (!m_wordProgression[i].m_isUnlocked)
                {
                    m_wordProgression[i].m_isUnlocked = true;
                    UIController.Instance.DisplayMessageWordLearned(word);
                }
            }
        }
    }

    public bool IsWordLearned(LanguageWord word)
    {
        for (int i = 0; i < m_wordProgression.Count; i++)
        {
            if (m_wordProgression[i].m_word == word)
            {
                return (m_wordProgression[i].m_isUnlocked);
            }
        }
        return false;
    }

    public bool IsWordLearned(string word)
    {
        string toLower = word.ToLower();

        for (int i = 0; i < m_wordProgression.Count; i++)
        {
            if (m_wordProgression[i].m_word.m_Word.ToLower().Equals(toLower))
            {
                return (m_wordProgression[i].m_isUnlocked);
            }
        }
        return false;
    }




    //---------------------------
    // Private

    private void Init()
    {
        foreach (LanguageWord word in m_Dictionary.Words)
        {
            WordProgression prog = new WordProgression
            {
                m_word = word,
                m_isUnlocked = false
            };

            m_wordProgression.Add(prog);
        }
    }
}
