using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienInspectable : Inspectable
{
    [Header("Alien Information")]
    public AlienInspectableInformation Information;

    public override void Interact(Player player)
    {
        base.Interact(player);
        UIController.Instance.InspectAlienItem(this);
    }
}
