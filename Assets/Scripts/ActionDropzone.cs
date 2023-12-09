using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionDropzone : MonoBehaviour, IDropHandler
{
    public Draggable.CardType cardType = Draggable.CardType.Ability;
    public bool cardSloted;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.GetComponent<Card>().faceName + " of " + eventData.pointerDrag.GetComponent<Card>().suit  +" was dropped onto " + gameObject.name);
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        if(d != null )
        {
            if(cardType == d.cardType && cardSloted != true)
            {
                eventData.pointerDrag.GetComponent<Draggable>().returnPatent = this.transform;
                cardSloted = true;
            }
        }
    }
}
