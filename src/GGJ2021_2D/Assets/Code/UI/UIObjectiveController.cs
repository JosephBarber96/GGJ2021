using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIObjectiveController : MonoBehaviour
{
    public Transform m_ParentObj;
    public Transform m_VerticalLayoutGroupTransform;
    public GameObject ObjectivePanelPrefab;

    private bool m_isOpen = false;

    public void ToggleObjectives()
    {
        if (m_isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public void Open()
    {
        m_ParentObj.gameObject.SetActive(true);
        m_isOpen = true;

        for (int i = 0; i < ObjectiveController.Instance.CurrentObjectives.Count; i++)
        {
            UIObjectivePanel panel = GameObject.Instantiate(ObjectivePanelPrefab).GetComponent<UIObjectivePanel>();
            panel.SetInfo(ObjectiveController.Instance.CurrentObjectives[i].m_objectiveData);

            panel.transform.SetParent(m_VerticalLayoutGroupTransform.transform, false);
        }
    }

    public void Close()
    {
        int childCount = m_VerticalLayoutGroupTransform.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(m_VerticalLayoutGroupTransform.transform.GetChild(i).gameObject);
        }

        m_ParentObj.gameObject.SetActive(false);
        m_isOpen = false;
    }
}
