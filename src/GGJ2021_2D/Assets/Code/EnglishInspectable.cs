using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnglishInspectable : Inspectable
{
    public EnglishInspectableInformation Information;

    public override void Interact(Player player)
    {
        UIController.Instance.InspectEnglishItem(this);
    }
}
