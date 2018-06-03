
using System;
using Assets.Scripts._003;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour , IPlayerStatus
{
    private float _h, _v;
    private Vector3 _dir;
 

    //自身速度
    public float SelfSpeed
    {
        get;
        set;
    }

    

    private Animator _animator;


    private float _timer;
    public float _speedUpTime = 3f;
    private Button _btnSpeedUp;
    private bool _btnBoolSpeedup;

    void Start ()
    {

        SelfSpeed = 2.5f;
        _timer = _speedUpTime;
        _animator = GetComponent<Animator>();
        _btnSpeedUp = GameObject.Find("Btn_SpeedUp").GetComponent<Button>();
    }

    private void Update()
    {
        if (_btnBoolSpeedup && _timer > 0)
        {
            _timer -= Time.deltaTime;
            ChangeSpeed();
        }
        if (_timer < 0||!_btnBoolSpeedup)
        {
            SlowDown();
            if (_timer<_speedUpTime)
            {
                _timer += Time.deltaTime*1.5f;     
            }
        }
        _btnSpeedUp.image.fillAmount = _timer / _speedUpTime;
    }


    void FixedUpdate ()
	{
	    MoveFunc();
	}

    private void MoveFunc()
    {
        //键盘移动
        //_h = Input.GetAxisRaw("Horizontal");
        //_v = Input.GetAxisRaw("Vertical");
        //_dir.x = _h; _dir.z = _v;

        //虚拟摇杆移动
        _dir.x = InputJoyStick.InputDir.x;
        _dir.z = InputJoyStick.InputDir.y;

        if (_dir != Vector3.zero)
        {
            transform.position += _dir * SelfSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir), 10f * Time.deltaTime);
            //TODO: 声音
            _animator.SetBool("Run", true);
        }
        else
        {
            transform.position += Vector3.forward *TerrainMoveManager.Instance._moveSpeed * Time.deltaTime;
            //TODO：声音
            _animator.SetBool("Run", false);
        }
    }

    private void ChangeSpeed()
    {
        SelfSpeed = 5f;
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Running"))
        {
            _animator.speed = 2;
        }
    }



    private void SlowDown()
    {
        SelfSpeed = 2.5f;
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Running"))
        {
            _animator.speed = 1;
        }
    }


    public void OnPress(BaseEventData eventData)
    {
        _btnBoolSpeedup = true;
    }

    public void OnLoose(BaseEventData eventData)
    {
        _btnBoolSpeedup = false;
    }

}
