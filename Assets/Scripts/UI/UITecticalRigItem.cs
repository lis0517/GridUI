using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITecticalRigItem : UIItem
{
    private TecticalRigData m_TecticalRigData;

    public override void SetData(ItemData data)
    {
        if(data is TecticalRigData)
        {
            var convertData = data as TecticalRigData;
            if (m_TecticalRigData == null)
                m_TecticalRigData = ScriptableObject.CreateInstance<TecticalRigData>();
            m_TecticalRigData.guid = Guid.NewGuid().ToString();
            m_TecticalRigData.size = convertData.size;
            m_TecticalRigData.itemName = convertData.itemName;
            m_TecticalRigData.itemType = convertData.itemType;
            m_TecticalRigData.maxUseCount = convertData.maxUseCount;
            m_TecticalRigData.isStackable = convertData.isStackable;
            m_TecticalRigData.rtfSize = convertData.rtfSize;
            m_TecticalRigData.itemImage = convertData.itemImage;
            m_TecticalRigData.spaces = convertData.spaces;
        }

    }

}
