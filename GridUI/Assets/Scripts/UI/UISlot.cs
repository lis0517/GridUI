using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    public IntVector2 GridPosition;

    public UIInventoryGrid ParentObject;

    public GameObject StoredItemObject;

    public ItemData StoredItemData;

    public Image BackgroundImage;

    public IntVector2 StoredItemStartPos;
    public IntVector2 StoredItemSize;

    public bool IsOccupied;

    public void Release()
    {
        GridPosition = IntVector2.zero;
        StoredItemObject = null;
        StoredItemData = null;
        StoredItemStartPos = IntVector2.zero;
        IsOccupied = false;
        gameObject.Recycle();
    }
}
