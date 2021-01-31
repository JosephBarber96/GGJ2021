using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnglishInspectable : Inspectable
{
    [Header("English Information")]
    public EnglishInspectableInformation Information;

    protected override void OnInteract(Player player)
    {
        UIController.Instance.InspectEnglishItem(this);
    }

    protected override void OnPlayerExit()
    {
        if (UIController.Instance.CurrentInspectable == this)
        {
            UIController.Instance.HideInspect();
        }
    }
}
