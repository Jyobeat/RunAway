using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Pawn_Eat : Action
{
    private Animator _anim;

    public override void OnAwake()
    {
        _anim = GetComponent<Animator>();
    }

    public override void OnStart()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Eating"))
            return;
        _anim.SetTrigger("Eat");
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

}
