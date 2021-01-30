using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInspectPanel : MonoBehaviour
{
    public RectTransform m_InspectBubbleRect;

    public TextMeshProUGUI m_inspectTitle;
    public TextMeshProUGUI m_inspectDescription;
    public Image m_inspectImage;


    public void DisplayAlienInfo(AlienInspectableInformation info)
    {
        m_InspectBubbleRect.gameObject.SetActive(true);

        m_inspectTitle.text = info.InspectName.m_Word;
        //m_inspectDescription.text = info.InspectName.m_Word;

        m_inspectImage.sprite = info.InspectIcon;
    }

    public void DisplayEnglishInfo(EnglishInspectableInformation info)
    {
        m_InspectBubbleRect.gameObject.SetActive(true);

        m_inspectTitle.text = info.InspectName;
        m_inspectDescription.text = info.InspectDescription;

        m_inspectImage.sprite = info.InspectIcon;
    }

    public void HideInfo()
    {
        m_InspectBubbleRect.gameObject.SetActive(false);
    }
}
