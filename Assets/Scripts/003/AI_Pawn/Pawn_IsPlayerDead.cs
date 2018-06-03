
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Pawn_IsPlayerDead : Conditional
{
    private bool _playerDead;

    public override void OnStart()
    {
        _playerDead = PlayerCharacteristic.Instance._isDead;
    }

    public override TaskStatus OnUpdate()
    {
        if (_playerDead)
        {
            return TaskStatus.Success;
        }
        else
            return TaskStatus.Failure;
    }

}
