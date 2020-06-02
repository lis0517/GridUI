using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryGrid : MonoBehaviour
{
    public RectTransform MyRtf;
    public GameObject[,] SlotGrid;

    public GameObject SlotPrefab;
    public IntVector2 GridSize;

    public float SlotSize;
    public float EdgePadding;

    public void SetData(IntVector2 size)
    {
        GridSize = size;
        SlotGrid = new GameObject[GridSize.x, GridSize.y];
        ResizePanel();
        CreateSlots();
    }

    private void CreateSlots()
    {
        for(int y =0; y < GridSize.y; ++y)
        {
            for(int x=0; x < GridSize.x; ++x)
            {
                GameObject obj = (GameObject)Instantiate(SlotPrefab);

                obj.transform.name = $"slot[{x}, {y}]";
                obj.transform.SetParent(this.transform);
                RectTransform rect = obj.transform.GetComponent<RectTransform>();

                rect.localPosition = new Vector3((x * SlotSize) + EdgePadding, (y * SlotSize) + EdgePadding, 0);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SlotSize);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,SlotSize);
                obj.GetComponent<RectTransform>().localScale = Vector3.one;

                var script = obj.GetComponent<UISlot>();
                script.ParentObject = this;
                script.GridPosition = new IntVector2(x, y);
                SlotGrid[x, y] = obj;
            }
        }
        GameManager.InventoryManager.AddGridUI(this, SlotGrid);
    }

    private void ResizePanel()
    {
        float width = (GridSize.x * SlotSize) + (EdgePadding * 2);
        float height = (GridSize.y * SlotSize) + (EdgePadding * 2);

        MyRtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        MyRtf.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        MyRtf.localScale = Vector3.one;
    }
}
