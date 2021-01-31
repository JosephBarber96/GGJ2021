using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienInspectable : Inspectable
{
    [Header("Alien Information")]
    public AlienInspectableInformation Information;

    protected override void OnInteract(Player player)
    {
        UIController.Instance.InspectAlienItem(this);
    }

    protected override void OnPlayerExit()
    {
        if (UIController.Instance.CurrentInspectable == this)
        {
            UIController.Instance.HideInspect();
        }
    }
}
