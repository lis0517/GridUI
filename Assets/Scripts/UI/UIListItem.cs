using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIListItem : MonoBehaviour, IPointerDownHandler
{
    public ItemData data;

    public Image ItemImage;

    public TextMeshProUGUI ItemNameLabel;

    public Image TypeImage;

    public TextMeshProUGUI ItemTypeLabel;

    public UIItem ItemPrefab;

    public Canvas ParentCavas;

    private void Start()
    {
        ItemImage.sprite = data.itemImage;
        ItemNameLabel.text = data.itemName;

        TypeImage.color = GameManager.GlobalData.GetTypeColor(data.itemType);
        ItemTypeLabel.text = GameManager.GlobalData.GetTypeText(data.itemType);
    }

    public void GetItem(Vector3 pointDown)
    {
        UIItem newItem = ObjectPool.SpawnUI(ItemPrefab);
        newItem.ItemRtf.SetParent(ParentCavas.transform);
        Vector3 pressPosition = GameManager.UICamera.ScreenToWorldPoint(pointDown);
        pressPosition.z = ParentCavas.GetComponent<RectTransform>().position.z;
        newItem.ItemRtf.position = pressPosition;
        newItem.ItemRtf.localScale = Vector3.one;
        newItem.ItemRtf.sizeDelta = data.rtfSize;
        newItem.SetData(data);
        
        UIItem.SelectedItem = newItem.gameObject;
        UIItem.SelectedItemSize = data.size;
        UIItem.IsDragging = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetItem(eventData.position);
    }

    private void Update()
    {
        UIItem item = UIItem.SelectedItem?.GetComponent<UIItem>();
        if(item != null)
        {
            Debug.Log(item.ItemRtf.anchoredPosition);

        }
    }
}
