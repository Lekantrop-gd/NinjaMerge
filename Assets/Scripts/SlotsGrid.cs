using Merge;
using System.Collections.Generic;
using UnityEngine;

public class SlotsGrid : MonoBehaviour
{
    private List<Slot> _slots;

    public void AddSlot(Slot slot)
    {
        _slots.Add(slot);
    }

    public void RemoveSlot(Slot slot)
    {

    }
}
