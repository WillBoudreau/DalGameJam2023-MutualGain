using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OfferDropzone : MonoBehaviour, IDropHandler
{
    private int offerCap = 3;
    public int cardsForOffer;
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log(eventData.pointerDrag.GetComponent<Card>().faceName + " of " + eventData.pointerDrag.GetComponent<Card>().suit  +" was dropped onto " + gameObject.name);
        if(cardsForOffer < offerCap)
        {
            eventData.pointerDrag.GetComponent<Draggable>().returnPatent = this.transform;
            eventData.pointerDrag.GetComponent<Card>().upForTrade = true;
            cardsForOffer += 1;
        }
    }
}
