using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandDropzone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name +" was dropped onto " + gameObject.name);
        eventData.pointerDrag.GetComponent<Draggable>().returnPatent = this.transform;
    }
}
