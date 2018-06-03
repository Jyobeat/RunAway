using BehaviorDesigner .Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class Pawn_WithinSight : Conditional
{

    public SharedTransform _player;
    

    public override void OnAwake()
    {
        _player =GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override TaskStatus OnUpdate()
    {
        if (_player.Value.position.z >= 1.2)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
