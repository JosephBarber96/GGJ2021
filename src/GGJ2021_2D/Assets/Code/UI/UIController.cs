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

    public class WordUnlockedAnimator
    {
        public UIWordUnlockedPanel m_panel;
        public float m_elapsedTime;
    }

    public static UIController Instance { get; private set; }

    [Header("Letter sprites")]
    [UnityEngine.Serialization.FormerlySerializedAs("LettersList")]
    public List<LetterSprite> m_LettersList = new List<LetterSprite>();
    public Sprite m_spaceLetterSprite;

    [Header("NPC Conversation")]
    public UIConversationController m_conversationController;

    [Header("Inspect")]
    public UIInspectPanel m_inspectPanel;

    [Header("Word Unlocked")]
    public Transform m_WordUnlockedPanel;
    public GameObject WordUnlockedPrefab;
    public RectTransform m_wordUnlockedLowerAnchor;
    public RectTransform m_wordUnlockedHigherAnchor;

    private List<WordUnlockedAnimator> m_wordUnlockedAnimatorsList = new List<WordUnlockedAnimator>();
    private List<LanguageWord> m_wordUnlockedBufferList = new List<LanguageWord>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        // Words learned UI
        UpdateWordsLearned();
    }



    //------------------------------
    // Conversation

    public void StartConversation(NPC npc)
    {
        m_conversationController.StartConversation(npc);
    }




    //------------------------------
    // Inspect

    public Inspectable CurrentInspectable { get; private set;}

    public void InspectAlienItem(AlienInspectable inspectable)
    {
        CurrentInspectable = inspectable;
        m_inspectPanel.DisplayAlienInfo(inspectable.Information);
    }

    public void InspectEnglishItem(EnglishInspectable inspectable)
    {
        CurrentInspectable = inspectable;
        m_inspectPanel.DisplayEnglishInfo(inspectable.Information);
    }

    public void HideInspect()
    {
        m_inspectPanel.HideInfo();
    }


        

    //------------------------------
    // Unlocking a word

    public void WordLearned(LanguageWord word)
    {
        m_wordUnlockedBufferList.Add(word);
    }

    public void UpdateWordsLearned()
    {
        // Update the buffer 
        if (m_wordUnlockedBufferList.Count > 0)
        {
            bool make = false;
            if (m_wordUnlockedAnimatorsList.Count == 0)
            {
                make = true;
            }
            else
            {
                const float BUFFER_TIME = 1f;
                float lastT = m_wordUnlockedAnimatorsList[m_wordUnlockedAnimatorsList.Count - 1].m_elapsedTime;
                make = lastT > BUFFER_TIME;
            }

            if (make)
            {
                UIWordUnlockedPanel unlockedPanel = GameObject.Instantiate(WordUnlockedPrefab).GetComponent<UIWordUnlockedPanel>();
                unlockedPanel.Setup(m_wordUnlockedBufferList[0]);
                unlockedPanel.transform.SetParent(m_WordUnlockedPanel.transform, false);

                WordUnlockedAnimator anim = new WordUnlockedAnimator
                {
                    m_panel = unlockedPanel,
                    m_elapsedTime = 0
                };
                anim.m_panel.SetAlpha(0);
                m_wordUnlockedAnimatorsList.Add(anim);

                m_wordUnlockedBufferList.RemoveAt(0);
            }
        }


        // Lerp the UI elements

        const float FADE_IN_TIME = 1f;
        const float LERP_TIME = 3f;
        const float FADE_OUT_TIME = 1f;

        for (int i = m_wordUnlockedAnimatorsList.Count - 1; i >= 0; i--)
        {
            bool remove = false;

            m_wordUnlockedAnimatorsList[i].m_elapsedTime += Time.deltaTime;
            float t = m_wordUnlockedAnimatorsList[i].m_elapsedTime;

            float normalized = t / LERP_TIME;

            // Fading in
            if (t < FADE_IN_TIME)
            {
                float normalizedFadeInTime = t / FADE_IN_TIME;
                m_wordUnlockedAnimatorsList[i].m_panel.SetAlpha(normalizedFadeInTime);
            }
            // Fading out 
            else if (t > (LERP_TIME - FADE_OUT_TIME))
            {
                float normalizedFadeOutTime = t - (LERP_TIME - FADE_OUT_TIME);
                m_wordUnlockedAnimatorsList[i].m_panel.SetAlpha(1 - normalizedFadeOutTime);
            }

            if (normalized >= 1)
            {
                normalized = 1;
                remove = true;
            }


            m_wordUnlockedAnimatorsList[i].m_panel.m_rectTransform.anchoredPosition = 
                Vector2.Lerp(
                m_wordUnlockedLowerAnchor.anchoredPosition,
                m_wordUnlockedHigherAnchor.anchoredPosition,
                normalized);

            if (remove)
            {
                GameObject.Destroy(m_wordUnlockedAnimatorsList[i].m_panel.gameObject);
                m_wordUnlockedAnimatorsList.RemoveAt(i);
            }
        }
    }




    //------------------------------
    // Utils

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