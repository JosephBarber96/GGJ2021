using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienInspectable : Inspectable
{
    public AlienInspectableInformation Information;

    public override void Interact(Player player)
    {
        UIController.Instance.InspectAlienItem(this);
    }
}
