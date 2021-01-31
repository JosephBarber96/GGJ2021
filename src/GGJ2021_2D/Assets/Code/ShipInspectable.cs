using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInspectable : EnglishInspectable
{
    protected override void OnInteract(Player player)
    {
        if (ObjectiveController.Instance.AreAllObjectivesComplete())
        {
            GameController.Instance.LoadScene(GameController.eScenes.EndCredits);
        }
        else
        {
            base.OnInteract(player);
        }
    }
}
