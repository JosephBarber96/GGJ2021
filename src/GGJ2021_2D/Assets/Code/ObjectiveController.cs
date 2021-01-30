using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    public enum eObjectives
    {
        FIX_WING = 0,
        GET_FUEL = 1,
    }

    public class ObjectiveProgression
    {
        public ObjectiveData m_objectiveData;
        public bool m_isComplete;
    }

    public static ObjectiveController Instance { get; private set; }

    public List<ObjectiveProgression> CurrentObjectives { get; private set; }


    //---------------------------
    // Monobehaviour 

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        CurrentObjectives = new List<ObjectiveProgression>();
    }




    //---------------------------
    // Objective

    public void UnlockObjective(ObjectiveData objective)
    {   
        if (IsObjectiveComplete(objective.m_Objective)) { return; }

        ObjectiveProgression progression = new ObjectiveProgression
        {
            m_objectiveData = objective,
            m_isComplete = false
        };

        CurrentObjectives.Add(progression);

        UIController.Instance.DisplayMessageObjectiveUnlocked(objective);
    }
    
    public void CompleteObjective(eObjectives objective)
    {
        for (int i = 0; i < CurrentObjectives.Count; i++)
        {
            if (CurrentObjectives[i].m_objectiveData.m_Objective == objective)
            {
                CurrentObjectives[i].m_isComplete = true;
            }
        }
    }

    public bool IsObjectiveComplete(eObjectives objective)
    {
        for (int i = 0; i < CurrentObjectives.Count; i++)
        {
            if (CurrentObjectives[i].m_objectiveData.m_Objective == objective)
            {
                return CurrentObjectives[i].m_isComplete;
            }
        }
        return false;
    }

    public ObjectiveData GetObjectiveData(eObjectives objective)
    {
        for (int i = 0; i < CurrentObjectives.Count; i++)
        {
            if (CurrentObjectives[i].m_objectiveData.m_Objective == objective)
            {
                return CurrentObjectives[i].m_objectiveData;
            }
        }
        return null;
    }
}
