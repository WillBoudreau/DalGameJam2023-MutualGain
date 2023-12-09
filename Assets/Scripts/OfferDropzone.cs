using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OfferDropzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private int offerCap = 3;
    public int cardsForOffer;
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.GetComponent<Card>().faceName + " of " + eventData.pointerDrag.GetComponent<Card>().suit  +" was dropped onto " + gameObject.name);
        if(cardsForOffer < offerCap)
        {
            eventData.pointerDrag.GetComponent<Draggable>().returnPatent = this.transform;
            cardsForOffer += 1;
        }
    }
}
