using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalPosition;
    private Transform returnPatent;
    private GameObject card;
    void Start()
    {
        card = this.gameObject;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log ("OnBeginDrag");
        returnPatent = this.transform.parent;
        card.transform.SetParent(this.transform.parent.parent);
        
        //originalPosition.position = card.transform.position; nullreferance, object is not an instance of an object?
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log ("OnDrag");
        card.gameObject.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log ("OnEndDrag");
        card.transform.SetParent(returnPatent);
        //card.transform.position = originalPosition.position; nullreferance, object is not an instance of an object? 
    }
}
