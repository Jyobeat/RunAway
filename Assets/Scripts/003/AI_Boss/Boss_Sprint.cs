using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine;

public class Boss_Sprint : Action{

    private Animator _ani;
    private CapsuleCollider _selfCollider;


    public SharedGameObject _trail;
    private GameObject _trailTemp;



    public override void OnAwake()
    {
        _trailTemp = GameObject.Instantiate(_trail.Value, transform);
        _trailTemp.SetActive(false);
        _ani = GetComponent<Animator>();
        _selfCollider = GetComponent<CapsuleCollider>();
    }

    public override void OnStart()
    {
        _trailTemp.SetActive(true);

        _ani.speed = 2f;

        
        _selfCollider.isTrigger = true;
    }

    public override TaskStatus OnUpdate()
    {

            if (transform.position.z > -0.5f)
            {
                transform.position += Vector3.back * 3f * Time.deltaTime;
                return TaskStatus.Running;
            }
            else
            {
                _ani.speed = 1f;
                return TaskStatus.Success;
            }
        
    }

    public override void OnEnd()
    {
        _trailTemp.SetActive(false);
        _ani.speed = 1f;
        _selfCollider.isTrigger = false;
    }
}
