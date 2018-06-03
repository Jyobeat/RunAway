
using UnityEngine;
using UnityEngine.UI;

public class Btn_Attack : MonoBehaviour
{

    private Transform _player;
    private Button _btnItself;
    private int count;

    private Transform _knifeParent;//UI中刀的父物体，这里由playercharacteristic中获取其引用

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _btnItself = GetComponent<Button>();
        _knifeParent = PlayerCharacteristic.Instance._knifeParent;
    }

    public void OnClickAttack()
    {
        _player.SendMessage("PlayerAttack");
        
        if ((count=--PlayerCharacteristic.Instance._knifeCount)== 0)
        {
            _btnItself.interactable = false;
        }

        ThreeUIManager.Instance.ReflashUI(count, _knifeParent);
    }

}
