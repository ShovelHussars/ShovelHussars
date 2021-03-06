using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public void PickUp()
    {
        if (Inventory.instance.Add(item))
        {
            Destroy(gameObject);
        }
    }
    public void Add()
    {
        Inventory.instance.Add(item);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
