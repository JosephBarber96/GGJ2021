using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [System.Serializable]
    public class NPCSentence
    {
        public List<LanguageWord> m_Words = new List<LanguageWord>();
    }

    [Header("Sentence")]
    public string m_chatSentence;
    public List<LanguageWord> m_unlockedWords;
    public Sprite m_ChatIcon;

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UIController.Instance.StartConversation(this);

            // Unlock the words
            for (int i = 0; i < m_unlockedWords.Count; i++)
            {
                LanguageManager.Instance.LearnWord(m_unlockedWords[i]);
            }

        }
    }
}