using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class PlayerAtkDmg : MonoBehaviour
{
    public float _AllowableError = 0.5f;
    public GameObject _knife;
    private Animator _anim;
    private LeanGameObjectPool _knifePool;
    private GameObject _knifeTemp;

    //无敌时间
    public float _invincibleTime = 1.5f;
    public float _flashingRate = 0.5f;  //闪烁频率

    private bool _isFlashing;
    private float _myTimer;
    [HideInInspector]
    public bool _isInvincible;//是否处于无敌

    private SkinnedMeshRenderer _flashingBody;//闪烁的身体

    private Transform _hostage;//人质的引用
    private int _childCount;

    private Vector3[] _throwPos = new Vector3[]
    {
        new Vector3(-1.3f,0,0),
        new Vector3(0,0,0),
        new  Vector3(1.3f,0,0),
    };
 
    private void Start()
    {
        _anim = GetComponent<Animator>();
        _knifePool = GameObject.FindGameObjectWithTag(_Data._tagKnifePool).GetComponent<LeanGameObjectPool>();
        _myTimer = 0f;
        _isFlashing = _isInvincible = false;
        _flashingBody = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        _childCount = transform.GetChildCount();
    }

    private void Update()
    {
        if (_isInvincible)
        {
            //处于无敌状态
            _myTimer += Time.deltaTime;
            Invincible();

            if (_myTimer > _invincibleTime)
            {
                //无敌时间结束
                _myTimer = 0;
                _isInvincible = false;
                gameObject.layer = 0;
            }
        }
    }

    //响应动画事件
    public void ThrowKnife()
    {
        if (Mathf.Abs(transform.position.x + 1.3f) <= _AllowableError)
        {
            _throwPos[0].z = transform.position.z;
            _knifeTemp = _knifePool.Spawn(_throwPos[0] + Vector3.forward * 0.5f + Vector3.up, Quaternion.Euler(90, 0, 0));
        }
        else if (Mathf.Abs(transform.position.x) <= _AllowableError)
        {
            _throwPos[1].z = transform.position.z;
            _knifeTemp = _knifePool.Spawn(_throwPos[1] + Vector3.forward * 0.5f + Vector3.up, Quaternion.Euler(90, 0, 0));
        }
        else if (Mathf.Abs(transform.position.x - 1.3f) <= _AllowableError)
        {
            _throwPos[2].z = transform.position.z;
            _knifeTemp = _knifePool.Spawn(_throwPos[2] + Vector3.forward*0.5f + Vector3.up, Quaternion.Euler(90, 0, 0));
        }
        //更改刀的层
        else
        {
            _knifeTemp = _knifePool.Spawn(transform.position+ Vector3.forward * 0.5f + Vector3.up, Quaternion.Euler(90, 0, 0));
        }
        _knifeTemp.layer = LayerMask.NameToLayer("Weapon");
    }

    public void TakeDamage()
    {
        if (PlayerCharacteristic.Instance._hasHostage)
        {

            _hostage = transform.GetChild(_childCount);
            if (_hostage != null)
            {

                _hostage.SendMessage("HostageDead", SendMessageOptions.DontRequireReceiver);
            }
            return;
        }

        //UI更新，和扣血
        PlayerCharacteristic.Instance.DecreaseHealth();
        _anim.SetTrigger("GetHit");
        if (!_isFlashing && PlayerCharacteristic.Instance._health > 0)
        {
            //0.1秒后闪烁
            Invoke("FlashBody", 0.1f);
            //播放动画          
        }
        _isInvincible = true;
    }

    private void PlayerAttack()
    {
        _anim.SetTrigger("Attack");
    }

    /// <summary>
    /// 身体闪烁
    /// </summary>
    private void FlashBody()
    {
        _isFlashing = true;              
        StartCoroutine("DisableBody", _flashingRate);        
    }

    IEnumerator DisableBody(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        _flashingBody.enabled = false;
        yield return new WaitForSecondsRealtime(time);
        _flashingBody.enabled = true;
        yield return new WaitForSecondsRealtime(time);
        _flashingBody.enabled = false;
        yield return new WaitForSecondsRealtime(time);
        _flashingBody.enabled = true;
        _isFlashing = false;
    }

    /// <summary>
    /// 无敌
    /// </summary>
    private void Invincible()
    {
        gameObject.layer = LayerMask.NameToLayer(_Data._layerInvincible);
    }

}
