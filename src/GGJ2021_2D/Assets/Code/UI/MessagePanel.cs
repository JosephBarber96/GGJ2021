using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessagePanel : MonoBehaviour
{
    public RectTransform m_rectTransform;
    public TextMeshProUGUI m_text;
    public Image m_panelImage;

    public void Setup(UIController.MessageBuffer msg)
    {
        if (msg.m_type == UIController.MessageBuffer.eType.NewWord)
        {
            m_text.text = "Word unlocked: \"" + msg.m_word.m_Word + "\"";
        }
        else if (msg.m_type == UIController.MessageBuffer.eType.ObjectiveUnlocked)
        {
            m_text.text = "New objective: \"" + msg.m_objective.m_ObjectiveTitle + "\"";
        }     
    }

    public void SetAlpha(float alpha)
    {
        Utils.SetAlpha(m_panelImage, alpha);
        Color c = m_text.color;
        c.a = alpha;
        m_text.color = c;
    }
}
