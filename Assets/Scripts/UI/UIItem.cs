using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class UIItem : MonoBehaviour
{
    //public Camera UICamera;
    public static GameObject SelectedItem { get; set; }
    public static IntVector2 SelectedItemSize { get; set; }
    public static bool IsDragging = false;

    public Image ItemImage;

    public RectTransform ItemRtf;

    public EItemType ItemType { get { return m_ItemData.itemType; } }

    [SerializeField]
    protected ItemData m_ItemData;

    public IntVector2 ItemSize { get { return m_ItemData.size; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void SetData(ItemData data)
    {
        
    }

    private void Update()
    {
        if(IsDragging)
        {
            Vector3 mousePosition = GameManager.UICamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = ItemRtf.position.z;
            SelectedItem.GetComponent<RectTransform>().position = mousePosition;
            Debug.Log(mousePosition);
        }
    }

    public static void SetSelectedItem(GameObject obj)
    {
        UIItem uiItem = obj.GetComponent<UIItem>();
        if(uiItem != null)
        {
            SelectedItem = obj;
            SelectedItemSize = uiItem.ItemSize;
            IsDragging = true;
            uiItem.ItemRtf.localScale = Vector3.one;
        }
    }

    public static void ResetSelectedItem()
    {
        SelectedItem = null;
        SelectedItemSize = IntVector2.zero;
        IsDragging = false;
    }

    //public void OnDrag(PointerEventData eventData)
    //{
    //    Vector3 mousePosition = GameManager.UICamera.ScreenToWorldPoint(eventData.position);
    //    mousePosition.z = ItemRtf.position.z;
    //    ItemRtf.position = mousePosition;
    //    IsDragging = true;
    //    Debug.Log(mousePosition);
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    Debug.Log("Down");
    //    //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    //mousePosition.z = ItemRtf.position.z;
    //    //ItemRtf.position = mousePosition;
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    Debug.Log("Up");
    //}

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    Debug.Log("Click");
    //}
}
