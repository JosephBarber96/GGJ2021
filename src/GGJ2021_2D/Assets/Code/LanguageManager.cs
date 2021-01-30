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
        Instance = this;
        Init();
    }




    //---------------------------
    // Public 

    public void LearnWord(LanguageWord word)
    {

    }

    public void IsWordLearned(LanguageWord word)
    {

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
