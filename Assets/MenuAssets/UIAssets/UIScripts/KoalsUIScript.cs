using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoalsUIScript : MonoBehaviour
{
    public float buttonDelay = 0.5f;
    public GameObject TurnUI;
    public GameObject TradeMenu;
    public GameObject ActionMenu;
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
    public void SetAction()
    {
        StartCoroutine(StartAction());
    }
    private IEnumerator StartAction()
    {
        yield return new WaitForSeconds(buttonDelay);
        TurnUI.SetActive(false);
        ActionMenu.SetActive(true);
    }
    public void ActionBack()
    {
        StartCoroutine(BackAction());
    }
    private IEnumerator BackAction()
    {
        yield return new WaitForSeconds(buttonDelay);
        TurnUI.SetActive(true);
        ActionMenu.SetActive(false);
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
