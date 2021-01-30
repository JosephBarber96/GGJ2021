using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [System.Serializable]
    public class LetterSprite
    {
        public char m_letter;
        public Sprite m_sprite;
    }

    public static UIController Instance { get; private set; }

    [Header("Letter sprites")]
    [UnityEngine.Serialization.FormerlySerializedAs("LettersList")]
    public List<LetterSprite> m_LettersList = new List<LetterSprite>();
    public Sprite m_spaceLetterSprite;

    [Header("NPC Conversation")]
    public UIConversationController m_conversationController;




    private void Awake()
    {
        Instance = this;
    }

    public void StartConversation(NPC npc)
    {
        m_conversationController.StartConversation(npc);
    }




    //------------------------------
    // 

    public Sprite GetSpriteForLetter(char letter)
    {
        if (letter == ' ')
        {
            return m_spaceLetterSprite;
        }

        string target = letter.ToString().ToLower();
        for (int i = 0; i < m_LettersList.Count; i++)
        {
            if (m_LettersList[i].m_letter == target.ToCharArray()[0])
            {
                return m_LettersList[i].m_sprite;
            }
        }

        return null;
    }
}