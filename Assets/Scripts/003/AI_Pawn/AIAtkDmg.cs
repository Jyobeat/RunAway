using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using BehaviorDesigner.Runtime;
using UnityEngine.SceneManagement;

public class AIAtkDmg : MonoBehaviour
{
    private Transform _target;
    public GameObject _knife;
    private LeanGameObjectPool _knifePool;
    private BehaviorTree[] _AIPawn;
    private Animator _anim;
    private bool _isDead = false;
    private PlayerAtkDmg _playerAtkDmg;

    //private Vector3 _startPoint;
    public Vector3 StartPoint { get; set; }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _knifePool = GameObject.FindGameObjectWithTag(_Data._tagKnifePool).GetComponent<LeanGameObjectPool>();
        _AIPawn = GetComponents<BehaviorTree>();
        _target = GameObject.FindGameObjectWithTag(_Data._tagPlayer).transform;
        _playerAtkDmg = _target.GetComponent<PlayerAtkDmg>();
    }

    private void FixedUpdate()
    {
        if (_isDead)
        {
            transform.position += Vector3.forward*TerrainMoveManager.Instance._moveSpeed*Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage("TakeDamage");
        }
    }

    public void Attack()
    {
        if (_playerAtkDmg._isInvincible)
            return;

        if (Vector3.SqrMagnitude(transform.position - _target.position) < 1.3f)
        {
            _target.SendMessage("TakeDamage");
        }
    }

    public void ThrowKnife()
    {
        _knifePool.Spawn(transform.position + transform.forward + Vector3.up, Quaternion.Euler(-90, 0, 0));
    }

    //AI受伤后
    public void TakeDamage()
    {
        _anim.SetTrigger("Dead");
        _isDead = true;
        for (int i = 0; i < _AIPawn.Length; i++)
        {
            _AIPawn[i].enabled = false;
        }
        ScoreManager.Instance.AddScoreByKillPawn();
        Destroy(gameObject, 1.5f);
    }

    private void OnDestroy()
    {
        if (SceneManager.GetActiveScene().name == "002-Tutorial")
        {
            TutorialLogicManager.Instance.ShowTutorialTips();
            return;
        }
        GameLogicManager.Instance.RemoveAIInList(gameObject);
        GameLogicManager.Instance.AddStartPointInList(StartPoint);
    }

}
