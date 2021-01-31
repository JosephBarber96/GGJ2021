using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Inspectable
{
    [Header("NPC")]
    public Sprite m_ChatIcon;
    public string m_chatSentence;
    public List<LanguageWord> m_unlockedWords;

    protected override void OnInteract(Player player)
    {
        UIController.Instance.StartConversation(this);

        // Unlock the words
        for (int i = 0; i < m_unlockedWords.Count; i++)
        {
            LanguageManager.Instance.LearnWord(m_unlockedWords[i]);
        }
    }

    protected override void OnPlayerExit()
    {
        if (UIController.Instance.CurrentNpc == this)
        {
            UIController.Instance.CloseConversation();
        }
    }
}