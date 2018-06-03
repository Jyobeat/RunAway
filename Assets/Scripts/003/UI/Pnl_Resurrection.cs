
using DG.Tweening;
using UnityEngine;

public class Pnl_Resurrection : MonoBehaviour
{

    public GameObject _PnlResurrectionWindow;

    public void OnClickClose()
    {
        _PnlResurrectionWindow.SetActive(false);
        GameLogicManager.Instance.ShowGameOverWindows();
    }

    public void OnClickAds()
    {
        //广告复活
        GameLogicManager.Instance.ShowAds();
    }

    public void OnClick5Gem()
    {
        //宝石复活
        GameLogicManager.Instance.UseGemToResurrect();
    }

    public void OnTxtDTComplete()
    {
        Camera.main.transform.DOShakePosition(0.5f);
    }
}
