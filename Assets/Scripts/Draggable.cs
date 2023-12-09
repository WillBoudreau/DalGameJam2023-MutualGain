using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalPosition;
    public Transform returnPatent;
    private GameObject card;
    void Start()
    {
        card = this.gameObject;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log ("OnBeginDrag");
        if(card.transform.parent.name == "TradePanel")
        {
            card.transform.parent.GetComponent<OfferDropzone>().cardsForOffer -= 1;
            if(card.transform.parent.GetComponent<OfferDropzone>().cardsForOffer <= 0)
            {
                card.transform.parent.GetComponent<OfferDropzone>().cardsForOffer = 0;
            }
        }
        returnPatent = this.transform.parent;
        card.transform.SetParent(this.transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
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
        GetComponent<CanvasGroup>().blocksRaycasts = true; 
    }
}
