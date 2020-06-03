using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIEquipment : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public Color OriginColor;
    public Color EquippableColor;
    public Color UnequippableColor;

    public Image EdgeImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(EdgeImage == null)
        {
            Debug.LogError($"{this.name} is EdgeImage variable not setting");
            return;
        }
        UIItem selectedScript = UIItem.SelectedItem?.GetComponent<UIItem>();
        if (selectedScript == null) return;

        if(selectedScript.ItemType == EItemType.TecticalRig)
        {
            EdgeImage.color = EquippableColor;
        }
        else
        {
            EdgeImage.color = UnequippableColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (EdgeImage == null)
        {
            Debug.LogError($"{this.name} is EdgeImage variable not setting");
            return;
        }
        EdgeImage.color = OriginColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
