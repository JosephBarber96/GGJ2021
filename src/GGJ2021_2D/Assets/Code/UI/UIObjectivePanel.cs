using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIObjectivePanel : MonoBehaviour
{
    public Image m_objectiveIcon;
    public TextMeshProUGUI m_titleText;
    public TextMeshProUGUI m_DescText;
    public Image m_checkBox;

    public void SetInfo(ObjectiveData objective)
    {
        m_objectiveIcon.sprite = objective.m_ObjectiveIcon;
        m_titleText.text = objective.m_ObjectiveTitle;
        m_DescText.text = objective.m_ObjectiveDescription;
        if (ObjectiveController.Instance.IsObjectiveComplete(objective.m_Objective))
        {
            m_checkBox.sprite = UIController.Instance.m_greenTick;
        }
        else
        {
            m_checkBox.sprite = UIController.Instance.m_RedCross;
        }
    }
}
