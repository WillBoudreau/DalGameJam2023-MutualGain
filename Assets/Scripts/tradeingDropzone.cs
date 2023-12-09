using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tradeingDropzone : MonoBehaviour, IDropHandler
{
public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.GetComponent<Card>().faceName + " of " + eventData.pointerDrag.GetComponent<Card>().suit  +" was dropped onto " + gameObject.name);
        eventData.pointerDrag.GetComponent<Draggable>().returnPatent = this.transform;
    }
}
