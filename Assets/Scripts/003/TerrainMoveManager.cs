using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using Lean;

public class TerrainMoveManager : MonoBehaviour
{
    public static TerrainMoveManager Instance;

    public float _moveSpeed;
    public LeanGameObjectPool[] _leanPools;

    private GameObject _firstTerrain, _lastTerrain, _tempTerrain;

    private void Awake()
    {
        Instance = this;
    }

    void Start ()
	{
	    _firstTerrain = _leanPools[Random.Range(0, _leanPools.Length)].Spawn(Vector3.zero, Quaternion.identity);
	    _lastTerrain = _leanPools[Random.Range(0, _leanPools.Length)].Spawn(Vector3.zero - new Vector3(0, 0, 10), Quaternion.identity);
	}

    public void ReflashTerrain()
    {
        LeanPool.Despawn(_firstTerrain);
        _tempTerrain = _leanPools[Random.Range(0, _leanPools.Length)].Spawn(Vector3.zero - new Vector3(0, 0, 10), Quaternion.identity);
        _firstTerrain = _lastTerrain;
        _lastTerrain = _tempTerrain;
    }
}
