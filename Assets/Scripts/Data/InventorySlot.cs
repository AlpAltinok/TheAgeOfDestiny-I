using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public InventoryHandler.SlotType SlotType;
    public int slotIndex;

    public void SetSlot(int _index, InventoryHandler.SlotType _type)
    {
        slotIndex = _index;
        SlotType = _type;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        InventoryHandler.instance.OnSlotSelected(slotIndex, SlotType);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        
    }
}
