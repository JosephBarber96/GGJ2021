using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspectable : MonoBehaviour, IInteractable
{
    public InspectableInformation Information;

    public void Interact(Player player)
    {
        UIController.Instance.InspectItem(Information);
    }
}
