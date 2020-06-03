using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIListItem : MonoBehaviour
{
    public ItemData data;

    public Image ItemImage;

    public TextMeshProUGUI ItemNameLabel;

    public Image TypeImage;

    public TextMeshProUGUI ItemTypeLabel;

    public UIItem ItemPrefab;

    private void Start()
    {
        ItemImage.sprite = data.itemImage;
        ItemNameLabel.text = data.itemName;

        TypeImage.color = GameManager.GlobalData.GetTypeColor(data.itemType);
        ItemTypeLabel.text = GameManager.GlobalData.GetTypeText(data.itemType);
    }

    public void GetItem()
    {
        UIItem newItem = ObjectPool.Spawn(ItemPrefab);
        newItem.SetData(data);
    }


}
