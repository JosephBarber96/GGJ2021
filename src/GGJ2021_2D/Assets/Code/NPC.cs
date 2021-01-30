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
    public NPCSentence m_Sentence;
    public Sprite m_ChatIcon;

    public void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UIController.Instance.StartConversation(this);
        }
    }
}