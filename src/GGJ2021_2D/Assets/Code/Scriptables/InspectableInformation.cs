using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InspectableInfo", menuName = "GGJ/InspectableInfo")]
public class InspectableInformation : ScriptableObject
{
    public Sprite InspectIcon;
    public LanguageWord InspectName;
    public List<LanguageWord> InspectText;
}
