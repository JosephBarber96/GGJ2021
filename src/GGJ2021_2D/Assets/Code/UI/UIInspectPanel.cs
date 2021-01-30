using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInspectPanel : MonoBehaviour
{
    public RectTransform m_InspectBubbleRect;
    public Text m_inspectText;
    public Image m_inspectImage;

    public void DisplayAlienInfo(AlienInspectableInformation info)
    {
        m_InspectBubbleRect.gameObject.SetActive(true);

        m_inspectText.text = info.InspectName.m_Word;
        m_inspectImage.sprite = info.InspectIcon;
    }

    public void DisplayEnglishInfo(EnglishInspectableInformation info)
    {
        m_InspectBubbleRect.gameObject.SetActive(true);

        m_inspectText.text = info.InspectName;
        m_inspectImage.sprite = info.InspectIcon;

        //info.Description
    }

    public void HideInfo()
    {
        m_InspectBubbleRect.gameObject.SetActive(false);
    }
}
