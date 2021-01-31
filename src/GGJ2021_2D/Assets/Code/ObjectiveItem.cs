using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveItem : MonoBehaviour
{
    public List<ObjectiveData> m_ObjectivesToComplete;

    public List<ObjectiveData> m_ObjectivesToUnlock;

    public void Unlock()
    {
        for (int i = 0; i < m_ObjectivesToUnlock.Count; i++)
        {
            ObjectiveController.Instance.UnlockObjective(m_ObjectivesToUnlock[i]);
        }
    }

    public void Complete()
    {
        foreach (var objectiveData in m_ObjectivesToComplete)
        {
            ObjectiveController.Instance.CompleteObjective(objectiveData);
        }
    }
}
