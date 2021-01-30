using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveUnlocker : MonoBehaviour
{
    public List<ObjectiveData> m_ObjectivesToUnlock;

    public void Unlock()
    {
        for (int i = 0; i < m_ObjectivesToUnlock.Count; i++)
        {
            ObjectiveController.Instance.UnlockObjective(m_ObjectivesToUnlock[i]);
        }
    }
}
