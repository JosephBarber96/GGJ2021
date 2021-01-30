using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInspectPanel : MonoBehaviour
{
    public RectTransform m_InspectBubbleRect;
    public Text m_inspectText;
    public Image m_inspectImage;

    public void DisplayInfo(string str, Sprite spr)
    {
        m_InspectBubbleRect.gameObject.SetActive(true);
        m_inspectText.text = str;
        m_inspectImage.sprite = spr;
    }
}
