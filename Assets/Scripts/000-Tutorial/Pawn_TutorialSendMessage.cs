
using BehaviorDesigner.Runtime.Tasks;


public class Pawn_TutorialSendMessage : Action {

    public override void OnStart()
    {
        TutorialLogicManager.Instance.ShowTutorialTips();
    }

    public override TaskStatus OnUpdate()
    {
        return TaskStatus.Success;
    }

}
