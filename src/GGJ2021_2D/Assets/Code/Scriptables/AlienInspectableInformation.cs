using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlienInspectableInfo", menuName = "GGJ/AlienInspectableInfo")]
public class AlienInspectableInformation : ScriptableObject
{
    public Sprite InspectIcon;
    public LanguageWord InspectName;
    public List<LanguageWord> InspectText;
}
