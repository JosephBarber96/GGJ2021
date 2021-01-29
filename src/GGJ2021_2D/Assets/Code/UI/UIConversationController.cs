using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIConversationController : MonoBehaviour
{
    [System.Serializable]
    public class LetterAnimator
    {
        public Image m_image;
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
    public Transform m_NPCSpeechBubble;
    public Image m_NPCSpeechBubbleImage;
    public GameObject LetterSpritePrefab;

    // Private 
    private NPC m_currentNpc;
    private eState m_state;
    [SerializeField]
    private List<LetterAnimator> m_currentSentence = new List<LetterAnimator>();

    private float m_stateTime;


    //------------------------------
    // Monobehaviour

    private void Awake()
    {
        m_state = eState.NONE;
    }

    private void Start()
    {
        EnableSpeechBubble(false);
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
                EnableSpeechBubble(true);
                SetImageAlpha(m_NPCSpeechBubbleImage, 0);
                break;
            case eState.TransitioningSentenceOn:
                {
                    int char_index = 0;

                    // For each word
                    foreach (LanguageWord word in m_currentNpc.m_Sentence.m_Words)
                    {
                        // Generate new letter animators
                        for (int i = 0; i < word.m_Word.Length; i++)
                        {
                            // Get char
                            char this_char = word.m_Word[i];

                            // Calc target time for transition end
                            float targetTime = (char_index + 1) * LETTER_TRANSITION_TIME;

                            // Create new Image ui component
                            Image letterImage = GameObject.Instantiate(LetterSpritePrefab).GetComponent<Image>();
                            letterImage.sprite = UIController.Instance.GetSpriteForLetter(this_char);
                            letterImage.transform.SetParent(m_NPCSpeechBubble.transform, false);
                            SetImageAlpha(letterImage, 0);

                            // Create animator 
                            LetterAnimator newLetter = new LetterAnimator
                            {
                                m_image = letterImage,
                                m_elapsedTime = 0,
                                m_delay = targetTime - LETTER_TRANSITION_TIME,
                                m_done = false
                            };
                            m_currentSentence.Add(newLetter);

                            char_index++;
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

        SetImageAlpha(m_NPCSpeechBubbleImage, normalized);

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

                SetImageAlpha(m_currentSentence[i].m_image, normalized);
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

    }

    void Update_TransitioningSpeechBubbleOff()
    {

    }



    //------------------------------
    // Util

    void SetImageAlpha(Image image, float alpha)
    {
        Color col = image.color;
        col.a = alpha;
        image.color = col;
    }

    private void EnableSpeechBubble(bool enab)
    {
        m_NPCSpeechBubbleImage.enabled = enab;
    }

    private void ClearSentences()
    {
        if (m_currentSentence.Count == 0) { return; }

        Debug.Log("clear");

        int childCount = m_NPCSpeechBubble.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(m_NPCSpeechBubble.transform.GetChild(i).gameObject);
        }
        m_currentSentence.Clear();
    }
}