using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using Lean;

public class ItemKnife : MonoBehaviour {

    private Vector3 _vecOnFloor;

    void OnEnable()
    {
        _vecOnFloor.Set(transform.position.x, 0, transform.position.z);
        transform.position = _vecOnFloor;
        transform.rotation = Quaternion.Euler(new Vector3(90, 0, Random.Range(0, 360)));
	    LeanPool.Despawn(gameObject, 5f);
	}
	

	void FixedUpdate ()
	{
	    transform.position += Vector3.forward*TerrainMoveManager.Instance._moveSpeed*Time.deltaTime;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.SendMessage("PickUp", SendMessageOptions.DontRequireReceiver);
            LeanPool.Despawn(gameObject);
        }
    }

    


}
