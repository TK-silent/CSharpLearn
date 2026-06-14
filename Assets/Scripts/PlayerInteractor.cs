using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private List<IInteractable> interactablesInRange = new List<IInteractable>();
    private PlayerCarry playerCarry;

    void Awake()
    {
        playerCarry = GetComponent<PlayerCarry>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactablesInRange.Count > 0)
            {
                IInteractable interactable = interactablesInRange[interactablesInRange.Count - 1];
                
                CarryItem carryItem = interactable as CarryItem;

                if (carryItem != null)
                {
                    playerCarry.PickUp(carryItem);
                }
                else
                {
                    interactable.Interact(playerCarry);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerCarry.Drop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null && !interactablesInRange.Contains(interactable))
        {
            interactablesInRange.Add(interactable);
        }
    }

    void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactablesInRange.Remove(interactable);
        }
    }
}
