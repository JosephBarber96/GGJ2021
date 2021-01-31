using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInspectPanel : MonoBehaviour
{
    public enum eState
    {
        OFF,
        AnimatingOn,
        Idle,
        AnimatingOff,
    }

    public const float TRANSITION_TIME = 0.25f;

    public RectTransform m_InspectBubbleRect;
    public TextMeshProUGUI m_inspectTitle;
    public TextMeshProUGUI m_inspectDescription;
    public Image m_inspectImage;
    public Image[] m_animatableImages;

    private eState m_state;
    private float m_stateTime;


    private void Update()
    {
        m_stateTime += Time.deltaTime;

        switch (m_state)
        {
            case eState.AnimatingOn:
                Update_AnimatingOn();
                break;

            case eState.AnimatingOff:
                Update_AnimatingOff();
                break;
        }
    }




    // -----------------------
    // Public calls 

    public void DisplayAlienInfo(AlienInspectableInformation info)
    {
        m_InspectBubbleRect.gameObject.SetActive(true);
        m_inspectTitle.text = info.InspectName.m_Word;
        m_inspectImage.sprite = info.InspectIcon;

        SetState(eState.AnimatingOn);
    }

    public void DisplayEnglishInfo(EnglishInspectableInformation info)
    {
        m_InspectBubbleRect.gameObject.SetActive(true);
        m_inspectTitle.text = info.InspectName;
        m_inspectDescription.text = info.InspectDescription;
        m_inspectImage.sprite = info.InspectIcon;

        SetState(eState.AnimatingOn);
    }

    public void AnimateOff()
    {
        SetState(eState.AnimatingOff);
    }



    // -----------------------
    // Private

    private void SetState(eState newState)
    {
        m_stateTime = 0f;

        // Enter
        m_state = newState;
        switch (m_state)
        {

            case eState.AnimatingOn:
                SetThisAlpha(0);
                break;
            case eState.Idle:
                break;
            case eState.AnimatingOff:
                break;
            case eState.OFF:
                m_InspectBubbleRect.gameObject.SetActive(false);
                break;

        }
    }

    private void Update_AnimatingOn()
    {
        float normalized = m_stateTime / TRANSITION_TIME;
        bool done = false;
        if (normalized > 1)
        {
            done = true;
            normalized = 1;
        }

        SetThisAlpha(normalized);
       

        if (done)
        {
            SetState(eState.Idle);
        }
    }

    private void Update_AnimatingOff()
    {
        float normalized = m_stateTime / TRANSITION_TIME;
        bool done = false;
        if (normalized > 1)
        {
            done = true;
            normalized = 1;
        }

        float a = 1 - normalized;
        SetThisAlpha(a);

        if (done)
        {
            SetState(eState.OFF);
        }
    }

    private void SetThisAlpha(float a)
    {
        Utils.SetAlpha(m_inspectTitle, a);
        Utils.SetAlpha(m_inspectDescription, a);
        for (int i = 0; i < m_animatableImages.Length; i++)
        {
            Utils.SetAlpha(m_animatableImages[i], a);
        }
    }
}
