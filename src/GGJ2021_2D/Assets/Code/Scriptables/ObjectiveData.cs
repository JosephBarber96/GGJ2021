using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectiveData", menuName = "GGJ/Objective")]
public class ObjectiveData : ScriptableObject
{
    public ObjectiveController.eObjectives m_Objective;
    public string m_ObjectiveTitle;
    public string m_ObjectiveDescription;
    public Sprite m_ObjectiveIcon;
}
