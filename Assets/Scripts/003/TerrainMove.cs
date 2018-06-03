using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMove : MonoBehaviour
{

    private float _speed;
    private TerrainMoveManager _terrainMoveManager;

	void Start ()
	{
	    _terrainMoveManager = TerrainMoveManager.Instance;
	    _speed = _terrainMoveManager._moveSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
        _speed = _terrainMoveManager._moveSpeed;
        if (transform.position.z < 10)
	        transform.position += Vector3.forward*_speed*Time.deltaTime;
	    else
	        _terrainMoveManager.ReflashTerrain();
	}

}
