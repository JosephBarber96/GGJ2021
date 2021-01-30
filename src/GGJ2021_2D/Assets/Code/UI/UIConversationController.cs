using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConversationController : MonoBehaviour
{
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
    public GameObject UIWordPanelPrefab;

    // Private 
    private NPC m_currentNpc;
    private eState m_state;
    private List<UIWordPanel> m_currentSentence = new List<UIWordPanel>();
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
                Utils.SetImageAlpha(m_NPCChatImage, 0);
                Utils.SetImageAlpha(m_NPCSpeechBubbleImage, 0);
                break;
            case eState.TransitioningSentenceOn:
                {
                    // For each word
                    foreach (LanguageWord word in m_currentNpc.m_Sentence.m_Words)
                    {
                        UIWordPanel wordPanel = GameObject.Instantiate(UIWordPanelPrefab).GetComponent<UIWordPanel>();
                        wordPanel.Setup(word.m_Word);
                        wordPanel.transform.SetParent(m_wordPanelParent.transform, false);
                        m_currentSentence.Add(wordPanel);

                        // Unlock the word 
                        if (!LanguageManager.Instance.IsWordLearned(word))
                        {
                            LanguageManager.Instance.LearnWord(word);
                        }
                    }
                }
                break;
            case eState.Idle:
                break;
            case eState.TransitioningSentenceOff:
                break;
            case eState.TransitioningSpeechBubbleOff:
                break;
        }
    }


    private void DoSentence(NPC npc)
    {

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

        Utils.SetImageAlpha(m_NPCSpeechBubbleImage, normalized);
        Utils.SetImageAlpha(m_NPCChatImage, normalized);

        if (exit)
        {
            SetState(eState.TransitioningSentenceOn);
        }
    }

    void Update_TransitioningSentenceOn()
    {
        bool allDone = true;

        //for (int i = 0; i < m_currentSentence.Count; i++)
        //{
        //    if (m_currentSentence[i].m_done) { continue; }

        //    allDone = false;

        //    m_currentSentence[i].m_elapsedTime += Time.deltaTime;   
        //    float effective_elapsed = m_currentSentence[i].m_elapsedTime - m_currentSentence[i].m_delay;

        //    if (effective_elapsed > 0)
        //    {
        //        float normalized = effective_elapsed / LETTER_TRANSITION_TIME;

        //        if (normalized > 1)
        //        {
        //            normalized = 1;
        //            m_currentSentence[i].m_done = true;
        //        }

        //        SetImageAlpha(m_currentSentence[i].m_image, normalized);
        //    }
        //}

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

    }

    void Update_TransitioningSpeechBubbleOff()
    {

    }



    //------------------------------
    // Util

    private void ClearSentences()
    {
        if (m_currentSentence.Count == 0) { return; }

        int childCount = m_NPCSpeechBubbleTransform.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(m_wordPanelParent.transform.GetChild(i).gameObject);
        }
        m_currentSentence.Clear();
    }
}