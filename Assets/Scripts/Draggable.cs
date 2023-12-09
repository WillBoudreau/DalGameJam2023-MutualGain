using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform originalPosition;
    public Transform returnPatent;
    private GameObject card;
    public enum CardType { Value, Ability}
    public CardType cardType = CardType.Value;
    void Start()
    {
        card = this.gameObject;
        if(card.GetComponent<Card>().value == 0)
        {
            cardType = CardType.Ability;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log ("OnBeginDrag");
        if(card.transform.parent.name == "TradePanel")
        {
            card.transform.parent.GetComponent<OfferDropzone>().cardsForOffer -= 1;
            eventData.pointerDrag.GetComponent<Card>().upForTrade = false;
            if(card.transform.parent.GetComponent<OfferDropzone>().cardsForOffer <= 0)
            {
                card.transform.parent.GetComponent<OfferDropzone>().cardsForOffer = 0;
            }
        }
        if(card.transform.parent.name == "ActionPanel")
        {
            card.transform.parent.GetComponent<ActionDropzone>().cardSloted = false;
            eventData.pointerDrag.GetComponent<Card>().forAction = true;
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
