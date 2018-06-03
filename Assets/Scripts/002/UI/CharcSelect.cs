using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharcSelect : MonoBehaviour
{

    public Button _btnBuy;
    public Text _txtSkill;

    //开关转换时，更改当前选择的人物和技能描述
    public void OnCharcChange(bool isOn)
    {
        if (isOn)
        {
            int _currentCharc = int.Parse(transform.name);
            CharcManager.Instance.SetCurrentCharcSelect(_currentCharc);
            //如果已经购买，按钮失活
            if (_Data._unlockCharc[_currentCharc] == 1)
            {
                _btnBuy.interactable = false;
            }
            else
            {
                _btnBuy.interactable = true;
            }
            
            _txtSkill.text = _Data._charcSkill[_currentCharc];

        }
    }

    public void SetOffBtnBuy()
    {
        _btnBuy = GameObject.Find("Buy").GetComponent<Button>();
        _btnBuy.interactable = false;
    }
	
}
