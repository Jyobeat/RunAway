using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostagesAI : MonoBehaviour
{
    private bool _isGotHelp;
    private Transform _player;
    private Animator _anim;
    private Vector3 _faceTo;
    private Vector3 _leftFaceto;
    private CapsuleCollider _selfCollider;

    private bool _flag;//在update的标识位，笨方法    

    public float _runFollowTime = 6f;//人质被救起后跟着玩家跑的时间
    private float _time;//计时器

    private GameObject _worldCanvasParent;//世界画布，父物体
    private GameObject _imgHelg;
    private GameObject _imgThx;

	void Start ()
	{
	    _isGotHelp = false;
	    _player = GameObject.FindGameObjectWithTag(_Data._tagPlayer).transform;
	    _anim = GetComponent<Animator>();
	    _selfCollider = GetComponent<CapsuleCollider>();
	    _faceTo = new Vector3(0, 180, 0);
	    _leftFaceto = new Vector3(0, -90, 0);
	    _time = 0f;
	    _flag = false;

	    _worldCanvasParent = GameObject.FindGameObjectWithTag(_Data._tagWorldCanvas);
	    _imgHelg = _worldCanvasParent.transform.GetChild(0).gameObject;
	    _imgThx = _worldCanvasParent.transform.GetChild(1).gameObject;

	    _worldCanvasParent.transform.position = new Vector3(transform.position.x, 1.2f, 0);
	    _worldCanvasParent.transform.rotation = Quaternion.Euler(90, 0, 0);
	    _imgHelg.SetActive(true);
	    Invoke("ImgSetParent", 1.35f);
	}
	
	
	void Update () {
	    if (!_isGotHelp)
	    {

	        transform.position += Vector3.forward*TerrainMoveManager.Instance._moveSpeed*Time.deltaTime;
	        if (transform.position.z > 4.5f)
	            DestroyItself();
	    }
	    else
	    {
	        if (_time < _runFollowTime)
	        {
	            transform.position = _player.position + 0.6f*Vector3.forward;
	            transform.rotation = Quaternion.Euler(_faceTo);
	            _time += Time.deltaTime;
	        }
	        else
	        {
	            transform.rotation = Quaternion.Euler(_leftFaceto);
	            transform.position += Vector3.left*3f*Time.deltaTime;
	            _worldCanvasParent.transform.rotation = Quaternion.Euler(90, 0, 0);
	            if (!_flag)
	            {
	                _imgThx.SetActive(true);
	                _flag = true;
	                CancelContact();
                    //todo：生成奖励物品
	                PlayerCharacteristic.Instance.IncreaseHealth();
	            }
	        }
	    }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag==_Data._tagPlayer)
        {
            _imgHelg.SetActive(false);
            _isGotHelp = true;
            transform.parent = collision.transform;
            _anim.SetBool("Run", true);
            PlayerCharacteristic.Instance._hasHostage = true;
        }
    }

    private void HostageDead()
    {
        _anim.SetTrigger("Dead");
        _isGotHelp = false;
        CancelContact();
    }

    private void CancelContact()
    {
        _selfCollider.enabled = false;
        transform.parent = null;
        PlayerCharacteristic.Instance._hasHostage = false;
        Invoke("DestroyItself", 2f);
    }

    private void DestroyItself()
    {
        _imgThx.SetActive(false);
        _worldCanvasParent.transform.SetParent(null);
        Destroy(gameObject);
    }

    private void ImgSetParent()
    {
        _worldCanvasParent.transform.SetParent(transform);
    }



}
