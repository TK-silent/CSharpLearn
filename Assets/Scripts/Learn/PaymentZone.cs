using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaymentZone : MonoBehaviour
{
    private List<CarryItem> coinInPaymentZone = new List<CarryItem>();
    private Collider zoneCollider;

    void Awake()
    {
        zoneCollider = GetComponent<Collider>();
    }
    
    public int GetCoinCount()
    {
        CleanUpCoins();
        return coinInPaymentZone.Count;
    }

    public void CleanUpCoins()
    {
        for (int i = coinInPaymentZone.Count - 1; i >= 0; i--)
        {
            CarryItem coin = coinInPaymentZone[i];

            if (coin == null)
            {
                coinInPaymentZone.RemoveAt(i);
                continue;
            }

            Collider coinCollider = coin.GetComponent<Collider>();

            if (coinCollider == null || !coinCollider.enabled)
            {
                coinInPaymentZone.RemoveAt(i);
                continue;
            }

            if (zoneCollider != null && !zoneCollider.bounds.Intersects(coinCollider.bounds))
            {
                coinInPaymentZone.RemoveAt(i);
            }
        }
    }

    public bool TryUseCoin(int coinNeedAmount)
    {
        CleanUpCoins();

        if (coinNeedAmount > coinInPaymentZone.Count || coinNeedAmount < 0)
        {
            return false;
        }

        for (int i = 0; i < coinNeedAmount; i++)
        {
            int lastIndex = coinInPaymentZone.Count - 1;

            CarryItem carryItem = coinInPaymentZone[lastIndex];
            coinInPaymentZone.RemoveAt(lastIndex);

            Destroy(carryItem.gameObject);
        }

        return true;
    }

    void OnTriggerEnter(Collider other)
    {
        CarryItem carryItem = other.GetComponent<CarryItem>();
        if (carryItem == null || carryItem.itemType != ItemType.Coin)
        {
            return;
        }

        if (!coinInPaymentZone.Contains(carryItem))
        {
            coinInPaymentZone.Add(carryItem);
        }
    }

    void OnTriggerExit(Collider other)
    {
        CarryItem carryItem = other.GetComponent<CarryItem>();
        if (carryItem != null)
        {
            coinInPaymentZone.Remove(carryItem);
        }
    }
}
