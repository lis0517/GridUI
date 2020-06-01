using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : TSingleton<GameManager>
{

    private InventoryManager m_InventoryManager;

    public static InventoryManager InventoryManager
    {
        get
        {
            if(Instance.m_InventoryManager == null)
            {
                Instance.m_InventoryManager = new InventoryManager();
                //Instance.m_InventoryManager.creat
            }
            return Instance.m_InventoryManager;
        }
    }
}
