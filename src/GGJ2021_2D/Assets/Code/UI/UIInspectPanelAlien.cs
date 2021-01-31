using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInspectPanelAlien : UIInspectPanelBase
{
    public RectTransform m_InspectBubbleRect;
    public TextMeshProUGUI m_inspectTitle;
    public Transform m_descriptionParent;
    public Image m_inspectImage;
    public Image[] m_animatableImages;
    public GameObject UIWordPanelPrefab;

    private List<UIWordPanel> m_wordPanelList = new List<UIWordPanel>();

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
        m_inspectTitle.text = info.InspectName;

        // Description
        {
            string[] words = info.InspectText.Split(' ');
            foreach (string word in words)
            {
                UIWordPanel wordPanel = GameObject.Instantiate(UIWordPanelPrefab).GetComponent<UIWordPanel>();
                wordPanel.Setup(word);
                wordPanel.transform.SetParent(m_descriptionParent.transform, false);

                m_wordPanelList.Add(wordPanel);
            }
        }

        m_inspectImage.sprite = info.InspectIcon;

        SetState(eState.AnimatingOn);
    }




    // -----------------------
    // Private

    protected override void SetState(eState newState)
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
                {
                    int childCount = m_descriptionParent.transform.childCount;
                    for (int i = childCount - 1; i >= 0; i--)
                    {
                        GameObject.Destroy(m_descriptionParent.transform.GetChild(i).gameObject);
                    }
                    m_wordPanelList.Clear();
                }
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

        for (int i = 0; i < m_wordPanelList.Count; i++)
        {
            Utils.SetAlpha(m_wordPanelList[i].m_englishText, a);
            Utils.SetAlpha(m_wordPanelList[i].m_alienText, a);
        }

        for (int i = 0; i < m_animatableImages.Length; i++)
        {
            Utils.SetAlpha(m_animatableImages[i], a);
        }
    }
}
