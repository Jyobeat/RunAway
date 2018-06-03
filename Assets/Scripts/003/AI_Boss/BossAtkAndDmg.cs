using DG.Tweening;
using Lean.Pool;
using UnityEngine;
using BehaviorDesigner.Runtime;

public class BossAtkAndDmg : MonoBehaviour
{
    private int _health;

    private Vector3 _startPoint;

    public Vector3 StartPoint
    {
        get { return _startPoint; }
        set { _startPoint = value; }
    }

    private Vector3 _initialPos;//生成boss的初始位置

    private Transform _target;
    private PlayerAtkDmg _playerAtkDmg;
    private Animator _anim;
    private BehaviorTree _bossTree;//行为树

    public GameObject _followWeapon;//跟踪武器
    private Vector3[] _FWPoints;//follow weapon 的生成点
    private Vector3[] _FWRotation;//follow weapon 的初始rotation

    //public GameObject _knife;//飞刀
    private LeanGameObjectPool _knifePool;
    private Vector3[] _knifePoints;

    //召唤pawn
    private GameObject _pawn;

    //过场动画
    public GameObject _scene;


    void Awake ()
    {
        _initialPos = new Vector3(0, 0, 5f);
	    _startPoint = new Vector3(0, 0, 2.5f);
        _target = GameObject.FindGameObjectWithTag(_Data._tagPlayer).transform;
        _playerAtkDmg = _target.GetComponent<PlayerAtkDmg>();
	    _knifePool = GameObject.FindGameObjectWithTag(_Data._tagKnifePool).GetComponent<LeanGameObjectPool>();
	    _anim = GetComponent<Animator>();
	    _bossTree = GetComponent<BehaviorTree>();
	    _FWPoints = new Vector3[] {new Vector3(-0.7f, 1, 1.6f), new Vector3(0.5f, 1, 1.6f)};
	    _FWRotation = new Vector3[] {new Vector3(0, -120, 0), new Vector3(0, 120, 0)};
	    _knifePoints = new Vector3[] {new Vector3(1.3f, 1, 4), new Vector3(0, 1, 4), new Vector3(-1.3f, 1, 4)};
	}

    private void OnEnable()
    {
        _health = 3;
        _anim.SetTrigger("Reborn");
        transform.position = _initialPos;
        _bossTree.enabled = true;
        GameLogicManager.Instance.KillAllPawn();
        Camera.main.transform.DOShakePosition(3.5f, new Vector3(0.5f, 0.5f, 0.5f)).OnComplete(ShowCutscenes);
    }

    void Attack()
    {
        if (_playerAtkDmg._isInvincible)
            return;

        if (Vector3.SqrMagnitude(transform.position - _target.position) < 4f)
        {
            _target.SendMessage("TakeDamage");
        }
    }

    private void FollowWeaponAttack()
    {
        Instantiate(_followWeapon, _FWPoints[0], Quaternion.Euler(_FWRotation[0]));
        Instantiate(_followWeapon, _FWPoints[1], Quaternion.Euler(_FWRotation[1]));
    }

    private void KnifeAttack()
    {
        for (int i = 0; i < _knifePoints.Length; i++)
        {
            _knifePool.Spawn(_knifePoints[i], Quaternion.Euler(-90, 0, 0));
        }
    }

    private void CallPawn()
    {
        _pawn = GameLogicManager.Instance.AddAI();
        if (_pawn.GetComponent<AIAtkDmg>().StartPoint.x == 0)
        {
            Destroy(_pawn);
            CallPawn();
        }
    }

    private void TakeDamage()
    {
        if (--_health > 0)
        {
            _anim.SetTrigger("GotHit");
        }
        else
        {
            //死亡
            ScoreManager.Instance.AddScoreByKillBoss();
            _anim.SetTrigger("Dead");
            _bossTree.enabled = false;
            Invoke("DisableMyself", 1.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage("TakeDamage");
        }
    }

    private void DisableMyself()
    {
        GameLogicManager.Instance.IsBossTime = false;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 过场动画
    /// </summary>
    private void ShowCutscenes()
    {

        _scene.SetActive(true);
        Time.timeScale = 0f;

    }


}
