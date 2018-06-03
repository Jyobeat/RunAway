using   BehaviorDesigner .Runtime.Tasks;
using UnityEngine;

public class Boss_WeaponAttack :Action
{
    private Animator _anim;
    public float _waitTime = 1f;
    private float _time;

    public override void OnAwake()
    {
        _anim = GetComponent<Animator>();
    }


    public override void OnStart()
    {
        _anim.SetTrigger("WeaponFollow");
        _time = 0;
    }

    public override TaskStatus OnUpdate()
    {
        _time += Time.deltaTime;
        if (_time < _waitTime&&_anim.GetCurrentAnimatorStateInfo(1).IsName("WeaponFollowAttack"))
            return TaskStatus.Running;
        else if(_time>=_waitTime&&_anim.GetCurrentAnimatorStateInfo(1).IsName("Idle"))
            return TaskStatus.Success;
        return TaskStatus.Running;
    }


}
