using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager
{

    #region < Public member >

    public UISlot HighlightedSlot;
    public UISlot PrevHighlightSlot;

    #endregion

    #region < Private member >

    private Dictionary<UIInventoryGrid, GameObject[,]> m_InventoryGridUIDic = new Dictionary<UIInventoryGrid, GameObject[,]>();
    private int m_CheckState;
    private bool m_IsOverEdge = false;
    private IntVector2 m_TotalOffset, m_CheckSize, m_CheckStartPosition;
    private IntVector2 m_OtherItemPosition, m_OtherItemSize;

    #endregion

    public void Initialized()
    {
        if(m_InventoryGridUIDic == null)
        {
            m_InventoryGridUIDic = new Dictionary<UIInventoryGrid, GameObject[,]>();
        }
        m_InventoryGridUIDic.Clear();
    }


    public void AddGridUI(UIInventoryGrid grid, GameObject[,] objs)
    {
        if(!m_InventoryGridUIDic.ContainsKey(grid))
        {
            m_InventoryGridUIDic.Add(grid, objs);
        }
        else
        {
            m_InventoryGridUIDic[grid] = objs;
        }
    }
}
