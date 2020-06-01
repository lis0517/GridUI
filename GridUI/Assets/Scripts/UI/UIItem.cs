using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UIItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Camera UICamera;
    public static GameObject SelectedItem { get; set; }
    public static IntVector2 SelectedItemSize { get; set; }
    public static bool IsDragging = false;

    public RectTransform ItemRtf;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = UICamera.ScreenToWorldPoint(eventData.position);
        mousePosition.z = ItemRtf.position.z;
        ItemRtf.position = mousePosition;
        IsDragging = true;
        Debug.Log(mousePosition);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition.z = ItemRtf.position.z;
        //ItemRtf.position = mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
