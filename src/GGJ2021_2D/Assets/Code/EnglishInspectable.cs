using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnglishInspectable : Inspectable
{
    [Header("English Information")]
    public EnglishInspectableInformation Information;

    public override void Interact(Player player)
    {
        base.Interact(player);
        UIController.Instance.InspectEnglishItem(this);
    }
}
