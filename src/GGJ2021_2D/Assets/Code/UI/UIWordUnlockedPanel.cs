using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIWordUnlockedPanel : MonoBehaviour
{
    public RectTransform m_rectTransform;
    public TextMeshProUGUI m_text;
    public Image m_panelImage;

    public void Setup(LanguageWord word)
    {
        m_text.text = "Word unlocked: \"" + word.m_Word + "\"";
    }

    public void SetAlpha(float alpha)
    {
        Utils.SetImageAlpha(m_panelImage, alpha);
        Color c = m_text.color;
        c.a = alpha;
        m_text.color = c;
    }
}
