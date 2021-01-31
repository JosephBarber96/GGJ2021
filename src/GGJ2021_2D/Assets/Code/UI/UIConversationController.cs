using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConversationController : MonoBehaviour
{
    public class UIWordPanelAnimator
    {
        public UIWordPanel m_panel;
        public float m_elapsedTime;
        public float m_delay;
        public bool m_done;
    }

    public enum eState
    {
        NONE,

        TransitionSpeechBubbleOn,
        TransitioningSentenceOn,
        Idle,
        TransitioningSentenceOff,
        TransitioningSpeechBubbleOff
    }

    public const float BUBBLE_TRANSITION_TIME = 0.25f;
    public const float LETTER_TRANSITION_TIME = 0.15f;

    [Header("Components")]
    public Transform m_NPCSpeechBubbleTransform;
    public Transform m_wordPanelParent;
    public Image m_NPCSpeechBubbleImage;
    public Image m_NPCChatImage;
    public Image m_chatBackImage;
    public GameObject UIWordPanelPrefab;

    // Private 
    private NPC m_currentNpc;
    private eState m_state;
    private List<UIWordPanelAnimator> m_currentSentence = new List<UIWordPanelAnimator>();
    private float m_stateTime;


    //------------------------------
    // Monobehaviour

    private void Awake()
    {
        m_state = eState.NONE;
    }

    private void Update()
    {
        m_stateTime += Time.deltaTime;

        switch (m_state)
        {
            case eState.TransitionSpeechBubbleOn:
                Update_TransitionSpeechBubbleOn();
                break;
            case eState.TransitioningSentenceOn:
                Update_TransitioningSentenceOn();
                break;
            case eState.Idle:
                Update_Idle();
                break;
            case eState.TransitioningSentenceOff:
                Update_TransitioningSentenceOff();
                break;
            case eState.TransitioningSpeechBubbleOff:
                Update_TransitioningSpeechBubbleOff();
                break;
        }
    }




    //------------------------------
    // Public calls 

    public void StartConversation(NPC npc)
    {
        ClearSentences();
        m_currentNpc = npc;
        m_NPCSpeechBubbleTransform.gameObject.SetActive(true);
        SetState(eState.TransitionSpeechBubbleOn);
    }

    public void AnimateOff()
    {
        SetState(eState.TransitioningSentenceOff);
    }


    //------------------------------
    // Priv

    private void SetState(eState newState)
    {
        // Exit
        switch (m_state)
        {
            case eState.TransitionSpeechBubbleOn:
                break;
            case eState.TransitioningSentenceOn:
                break;
            case eState.Idle:
                break;
            case eState.TransitioningSentenceOff:
                {
                    ClearSentences();
                }
                break;
            case eState.TransitioningSpeechBubbleOff:
                break;
        }


        m_stateTime = 0f;

        // Enter
        m_state = newState;
        switch (m_state)
        {
            case eState.TransitionSpeechBubbleOn:
                m_NPCChatImage.sprite = m_currentNpc.m_ChatIcon;
                Utils.SetAlpha(m_NPCChatImage, 0);
                Utils.SetAlpha(m_NPCSpeechBubbleImage, 0);
                Utils.SetAlpha(m_chatBackImage, 0);
                break;

            case eState.TransitioningSentenceOn:
                {
                    string[] words = m_currentNpc.m_chatSentence.Split(' ');
                    int index = 0;
                    foreach (string word in words)
                    {
                        UIWordPanel wordPanel = GameObject.Instantiate(UIWordPanelPrefab).GetComponent<UIWordPanel>();
                        wordPanel.Setup(word);
                        wordPanel.transform.SetParent(m_wordPanelParent.transform, false);

                        UIWordPanelAnimator anim = new UIWordPanelAnimator
                        {
                            m_panel = wordPanel,
                            m_elapsedTime = 0f,
                            m_delay = LETTER_TRANSITION_TIME * index,
                            m_done = false,
                        };
                        m_currentSentence.Add(anim);

                        index++;
                    }
                }
                break;

            case eState.Idle:
                break;

            case eState.TransitioningSentenceOff:
                {
                    for (int i = 0; i < m_currentSentence.Count; i++)
                    {
                        m_currentSentence[i].m_done = false;
                    }
                }
                break;

            case eState.TransitioningSpeechBubbleOff:
                break;

            case eState.NONE:
                ClearSentences();
                m_NPCSpeechBubbleTransform.gameObject.SetActive(false);
                break;
        }
    }




    // Updates 

    void Update_TransitionSpeechBubbleOn()
    {
        float normalized = m_stateTime / BUBBLE_TRANSITION_TIME;

        bool exit = false;
        if (normalized >= 1)
        {
            exit = true;
            normalized = 1;
        }

        Utils.SetAlpha(m_NPCSpeechBubbleImage, normalized);
        Utils.SetAlpha(m_NPCChatImage, normalized);
        Utils.SetAlpha(m_chatBackImage, normalized);

        if (exit)
        {
            SetState(eState.TransitioningSentenceOn);
        }
    }

    void Update_TransitioningSentenceOn()
    {
        bool allDone = true;

        for (int i = 0; i < m_currentSentence.Count; i++)
        {
            if (m_currentSentence[i].m_done) { continue; }

            allDone = false;

            m_currentSentence[i].m_elapsedTime += Time.deltaTime;
            float effective_elapsed = m_currentSentence[i].m_elapsedTime - m_currentSentence[i].m_delay;

            if (effective_elapsed > 0)
            {
                float normalized = effective_elapsed / LETTER_TRANSITION_TIME;

                if (normalized > 1)
                {
                    normalized = 1;
                    m_currentSentence[i].m_done = true;
                }

                Utils.SetAlpha(m_currentSentence[i].m_panel.m_alienText, normalized);
                Utils.SetAlpha(m_currentSentence[i].m_panel.m_englishText, normalized);
            }
        }

        if (allDone)
        {
            SetState(eState.Idle);
        }
    }

    void Update_Idle()
    {

    }

    void Update_TransitioningSentenceOff()
    {
        bool allDone = true;

        for (int i = 0; i < m_currentSentence.Count; i++)
        {
            if (m_currentSentence[i].m_done) { continue; }

            allDone = false;

            m_currentSentence[i].m_elapsedTime += Time.deltaTime;
            float effective_elapsed = m_currentSentence[i].m_elapsedTime - m_currentSentence[i].m_delay;

            if (effective_elapsed > 0)
            {
                float normalized = effective_elapsed / LETTER_TRANSITION_TIME;

                if (normalized > 1)
                {
                    normalized = 1;
                    m_currentSentence[i].m_done = true;
                }

                float a = 1 - normalized;
                Utils.SetAlpha(m_currentSentence[i].m_panel.m_alienText, a);
                Utils.SetAlpha(m_currentSentence[i].m_panel.m_englishText, a);
            }
        }

        if (allDone)
        {
            SetState(eState.TransitioningSpeechBubbleOff);
        }
    }

    void Update_TransitioningSpeechBubbleOff()
    {
        float normalized = m_stateTime / BUBBLE_TRANSITION_TIME;

        bool exit = false;
        if (normalized >= 1)
        {
            exit = true;
            normalized = 1;
        }

        float a = 1 - normalized;
        Utils.SetAlpha(m_NPCSpeechBubbleImage, a);
        Utils.SetAlpha(m_NPCChatImage, a);
        Utils.SetAlpha(m_chatBackImage, a);

        if (exit)
        {
            SetState(eState.NONE);
        }
    }



    //------------------------------
    // Util

    private void ClearSentences()
    {
        if (m_currentSentence.Count == 0) { return; }

        int childCount = m_wordPanelParent.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(m_wordPanelParent.transform.GetChild(i).gameObject);
        }
        m_currentSentence.Clear();
    }
}