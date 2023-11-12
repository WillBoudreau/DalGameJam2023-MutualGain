using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnUIController : MonoBehaviour
{
    public float buttonDelay = 0.5f;
    public GameObject TurnUI;
    public GameObject TradeMenu;
    public void SetTrade()
    {
        StartCoroutine(StartTrade());
    }
    private IEnumerator StartTrade()
    {
        yield return new WaitForSeconds(buttonDelay);
        TurnUI.SetActive(false);
        TradeMenu.SetActive(true);
    }
    public void TradeBack()
    {
        StartCoroutine(BackTrade());
    }
    private IEnumerator BackTrade()
    {
        yield return new WaitForSeconds(buttonDelay);
        TurnUI.SetActive(true);
        TradeMenu.SetActive(false);
    }
}
