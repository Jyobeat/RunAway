using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Pawn_ChangeBehaviorTree : Action
{

    private BehaviorTree[] _allTrees;

    public override void OnAwake()
    {
        _allTrees = transform.GetComponents<BehaviorTree>();        
    }

    public override void OnStart()
    {
        //玩家死了后
        if (PlayerCharacteristic.Instance._isDead)
            ChangePlayerDeadBehavior();
        //玩家没死
        else
            ChangePlayerAliveBehavior();
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }


    private void ChangePlayerDeadBehavior()
    {
        for (int i = 0; i < _allTrees.Length; i++)
        {
            if (_allTrees[i] == null)
                break;
            if (_allTrees[i].Group.Equals(0))
            {
                _allTrees[i].enabled = false;
            }
            else if (_allTrees[i].Group.Equals(1))
            {
                _allTrees[i].enabled = true;
            }
        }
    }

    private void ChangePlayerAliveBehavior()
    {
        
        for (int i = 0; i < _allTrees.Length; i++)
        {
            if (_allTrees == null)
            {
                break;
            }
            if (_allTrees[i].Group.Equals(0))
            {
                _allTrees[i].enabled = true;
            }
            else if (_allTrees[i].Group.Equals(1))
            {
                _allTrees[i].enabled = false;
            }
        }
    }

}
