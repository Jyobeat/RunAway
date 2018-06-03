
using   BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Boss_Attack : Action {

    private Animator _ani;

    public float _waitTime = 1f;
    private float _mytime;

    public override void OnStart()
    {
        _ani = GetComponent<Animator>();
        _ani.SetTrigger("Attack");
        _mytime = 0f;
    }

    public override TaskStatus OnUpdate()
    {
        if (_mytime < _waitTime)
        {
            _mytime += Time.deltaTime;
            return TaskStatus.Running;
        }
        else
        {
            return TaskStatus.Success;
        }
    }


}
