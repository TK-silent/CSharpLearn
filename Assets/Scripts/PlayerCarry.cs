using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    public Transform holdPoint;
    public float dropDistant = 1.2f;

    private CarryItem currentItem;
    
    public bool HasItem()
    {
        return currentItem != null;
    }

    public void PickUp(CarryItem item)
    {
        if (currentItem != null)
        {
            return;
        }

        currentItem = item;
        currentItem.OnPickedUp(holdPoint);
    }

    public void Drop()
    {
        if (currentItem == null)
        {
            return;
        }

        Vector3 dropPosition = transform.position + transform.forward * dropDistant;

        currentItem.OnDroped(dropPosition);
        currentItem = null;
    }

    public CarryItem GetCurrentItem()
    {
        return currentItem;
    }
}
