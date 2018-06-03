using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputJoyStick : MonoBehaviour {

    private Vector3 _startPoint;
    public static Vector3 InputDir;
	
	void Start ()
	{
	    _startPoint = transform.parent.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        InputDir = Vector3.Normalize(transform.position - _startPoint);
	}
}
