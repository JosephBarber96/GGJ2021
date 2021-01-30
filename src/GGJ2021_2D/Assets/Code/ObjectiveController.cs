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
        public eObjectives m_objective;
        public bool m_isComplete;
    }

    public static ObjectiveController Instance { get; private set; }

    [Header("Objectives")]
    public List<ObjectiveData> m_AllObjectivesList = new List<ObjectiveData>();

    private List<ObjectiveProgression> m_objectiveProgressionList = new List<ObjectiveProgression>();


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
        Init();
    }




    //---------------------------
    // Objective
    
    public void CompleteObjective(eObjectives objective)
    {
        for (int i = 0; i < m_objectiveProgressionList.Count; i++)
        {
            if (m_objectiveProgressionList[i].m_objective == objective)
            {
                m_objectiveProgressionList[i].m_isComplete = true;
            }
        }
    }

    public bool IsObjectiveComplete(eObjectives objective)
    {
        for (int i = 0; i < m_objectiveProgressionList.Count; i++)
        {
            if (m_objectiveProgressionList[i].m_objective == objective)
            {
                return m_objectiveProgressionList[i].m_isComplete;
            }
        }

        Debug.LogError("[ERROR]: Objective " + objective + " not registered in DB");
        return false;
    }

    public ObjectiveData GetObjectiveData(eObjectives objective)
    {
        for (int i = 0; i < m_AllObjectivesList.Count; i++)
        {
            if (m_AllObjectivesList[i].m_Objective == objective)
            {
                return m_AllObjectivesList[i];
            }
        }

        Debug.LogError("[ERROR]: Objective " + objective + " not registered in DB");
        return null;
    }

    void Init()
    {
        for (int i = 0; i < m_AllObjectivesList.Count; i++)
        {
            ObjectiveProgression progression = new ObjectiveProgression
            {
                m_objective = m_AllObjectivesList[i].m_Objective,
                m_isComplete = false
            };

            m_objectiveProgressionList.Add(progression);
        }
    }
}
