using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class UISlotSector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject ParentObject;

    public static IntVector2 PositionOffset;

    public static UISlotSector SectorScript;

    public int QuadNumber;

    private UISlot m_ParentSlotScript;


    // Start is called before the first frame update
    void Start()
    {
        m_ParentSlotScript = ParentObject?.GetComponent<UISlot>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SectorScript = this;
        

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void CalculatePositionOffset()
    {
        if (UIItem.SelectedItemSize.x != 0 && UIItem.SelectedItemSize.x % 2 == 0)
        {
            switch (QuadNumber)
            {
                case 1:
                    PositionOffset.x = 0; break;
                case 2:
                    PositionOffset.x = -1; break;
                case 3:
                    PositionOffset.x = 0; break;
                case 4:
                    PositionOffset.x = -1; break;
                default: break;
            }
        }
        if (UIItem.SelectedItemSize.y != 0 && UIItem.SelectedItemSize.y % 2 == 0)
        {
            switch (QuadNumber)
            {
                case 1:
                    PositionOffset.y = 0; break;
                case 2:
                    PositionOffset.y = 0; break;
                case 3:
                    PositionOffset.y = 0; break;
                case 4:
                    PositionOffset.y = 0; break;
                default: break;
            }
        }
    }
}
