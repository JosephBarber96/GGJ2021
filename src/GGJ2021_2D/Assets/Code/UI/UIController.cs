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

    public class MessageAnimator
    {
        public MessagePanel m_panel;
        public float m_elapsedTime;
    }

    /// <summary>
    /// This should use abstraction but, jam code lol
    /// </summary>
    public class MessageBuffer
    {
        public enum eType
        {
            NewWord,
            ObjectiveUnlocked,
        }

        public eType m_type;
        public LanguageWord m_word;
        public ObjectiveData m_objective;
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

    [Header("Objectives")]
    public UIObjectiveController m_objectiveController;

    [Header("Misc Assets")]
    public Sprite m_RedCross;
    public Sprite m_greenTick;

    private List<MessageAnimator> m_wordUnlockedAnimatorsList = new List<MessageAnimator>();
    private List<MessageBuffer> m_messageBufferList = new List<MessageBuffer>();

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
        UpdateMessages();
    }



    //------------------------------
    // Conversation

    public NPC CurrentNpc { get; private set; }

    public void StartConversation(NPC npc)
    {
        if (CurrentNpc == npc)
        {
            CloseConversation();
        }
        else
        {
            CurrentNpc = npc;
            m_conversationController.StartConversation(npc);
        }
    }

    public void CloseConversation()
    {
        CurrentNpc = null;
        m_conversationController.AnimateOff();
    }




    //------------------------------
    // Inspect

    public Inspectable CurrentInspectable { get; private set;}

    public void InspectAlienItem(AlienInspectable inspectable)
    {
        if (CurrentInspectable == inspectable)
        {
            HideInspect();
        }
        else
        {
            CurrentInspectable = inspectable;
            m_inspectPanel.DisplayAlienInfo(inspectable.Information);
        }
    }

    public void InspectEnglishItem(EnglishInspectable inspectable)
    {
        if (CurrentInspectable == inspectable)
        {
            HideInspect();
        }
        else
        {
            CurrentInspectable = inspectable;
            m_inspectPanel.DisplayEnglishInfo(inspectable.Information);
        }
    }

    public void HideInspect()
    {
        CurrentInspectable = null;
        m_inspectPanel.AnimateOff();
    }




    //------------------------------
    // Objectives

    public void ToggleObjectives()
    {
        m_objectiveController.ToggleObjectives();
    }

    public void OpenObjectives()
    {
        m_objectiveController.Open();
    }

    public void CloseObjectives()
    {
        m_objectiveController.Close();
    }




    //------------------------------
    // Displaying a message

    const float FADE_IN_TIME = 1f;
    const float LERP_TIME = 7f;
    const float FADE_OUT_TIME = 1f;
    const float LEARNED_WORD_BUFFER_TIME = 2.5f;

    public void DisplayMessageWordLearned(LanguageWord word)
    {
        m_messageBufferList.Add(new MessageBuffer
        {
            m_type = MessageBuffer.eType.NewWord,
            m_word = word,
        });
    }

    public void DisplayMessageObjectiveUnlocked(ObjectiveData objective)
    {
        m_messageBufferList.Add(new MessageBuffer
        {
            m_type = MessageBuffer.eType.ObjectiveUnlocked,
            m_objective = objective,
        });
    }

    public void UpdateMessages()
    {
        // Update the buffer 
        if (m_messageBufferList.Count > 0)
        {
            bool make = false;
            if (m_wordUnlockedAnimatorsList.Count == 0)
            {
                make = true;
            }
            else
            {
                float lastT = m_wordUnlockedAnimatorsList[m_wordUnlockedAnimatorsList.Count - 1].m_elapsedTime;
                make = lastT > LEARNED_WORD_BUFFER_TIME;
            }

            if (make)
            {
                MessagePanel unlockedPanel = GameObject.Instantiate(WordUnlockedPrefab).GetComponent<MessagePanel>();
                unlockedPanel.Setup(m_messageBufferList[0]);
                unlockedPanel.transform.SetParent(m_WordUnlockedPanel.transform, false);

                MessageAnimator anim = new MessageAnimator
                {
                    m_panel = unlockedPanel,
                    m_elapsedTime = 0
                };
                anim.m_panel.SetAlpha(0);
                m_wordUnlockedAnimatorsList.Add(anim);

                m_messageBufferList.RemoveAt(0);
            }
        }


        // Lerp the UI elements

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