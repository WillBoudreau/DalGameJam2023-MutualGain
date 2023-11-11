using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMenu : MonoBehaviour
{
    public GameObject TradeMenu;
    public GameObject ActionMenu;
    // Start is called before the first frame update
    void Start()
    {
        TradeMenu.SetActive(false);
        ActionMenu.SetActive(false);
    }

    public void ActivateTrade()
    {
        TradeMenu.SetActive(true);
    }
    public void TradeBack()
    {
        TradeMenu.SetActive(false);
    }
    public void ActivateAction()
    {
        ActionMenu.SetActive(true);
    }
    public void ActionBack()
    {
        ActionMenu.SetActive(false);
    }
}
