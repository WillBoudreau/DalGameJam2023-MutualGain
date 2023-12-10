using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tradeingDropzone : MonoBehaviour, IDropHandler
{
    public GameObject OfferCard;
    public GameObject DesiredCard;
    public GameObject TradeScreen;
    public GameObject gameManager;
    public void OnDrop(PointerEventData eventData)
    {
        if(gameManager.GetComponent<GameManager>().canTrade == true)
        {
            //Debug.Log(eventData.pointerDrag.GetComponent<Card>().faceName + " of " + eventData.pointerDrag.GetComponent<Card>().suit  +" was dropped onto " + gameObject.name);
            TradeScreen.SetActive(true);
            this.gameObject.transform.GetChild(0).parent = DesiredCard.transform;
            eventData.pointerDrag.GetComponent<Draggable>().returnPatent = OfferCard.transform;
        }

    }
}
