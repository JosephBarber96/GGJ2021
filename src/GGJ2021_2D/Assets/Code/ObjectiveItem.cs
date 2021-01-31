using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveItem : MonoBehaviour
{
    public bool UnlockOnStart = false;

    public List<ObjectiveData> m_ObjectivesToComplete;
    public List<ObjectiveData> m_ObjectivesToUnlock;

    private void Update()
    {
        if (UnlockOnStart)
        {
            Unlock();
        }
    }

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
