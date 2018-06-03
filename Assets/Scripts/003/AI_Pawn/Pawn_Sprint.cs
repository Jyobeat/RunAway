
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Pawn_Sprint : Action
{
    private Animator _ani;
    private CapsuleCollider _selfCollider;

    public Pawn_Throw _throw;

    public float _waitTime=2f;
    public SharedGameObject _trail;
    private GameObject _trailTemp;

    private float _myTime;

    public override void OnAwake()
    {
        _trailTemp= GameObject.Instantiate(_trail.Value, transform);
        _trailTemp.SetActive(false);
    }

    public override void OnStart()
    {
        _trailTemp.SetActive(true);
        _ani = _throw._ani;
        _ani.speed = 2f;
        _myTime = 0f;
        _selfCollider = transform.GetComponent<CapsuleCollider>();
        _selfCollider.isTrigger = true;
    }

    public override TaskStatus OnUpdate()
    {
        if (_myTime < _waitTime)
        {
            _myTime += Time.deltaTime;
            return TaskStatus.Running;
        }
        else
        {
            if (transform.position.z > -0.5f)
            {
                transform.position += Vector3.back*6f*Time.deltaTime;
                return TaskStatus.Running;
            }
            else
            {
                _ani.speed = 1f;
                return TaskStatus.Success;
            }
        }
    }

    public override void OnEnd()
    {
        _trailTemp.SetActive(false);
        _ani.speed = 1f;
        _selfCollider.isTrigger = false;
    }
}
