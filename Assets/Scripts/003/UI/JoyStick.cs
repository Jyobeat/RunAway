
using UnityEngine;

public class JoyStick : MonoBehaviour
{

    private bool _canMove;

    private Vector3 _startPoint;

    private float _dis;

    private int i;
    private Vector2 _touchPos;
    
	void Start ()
	{
	    _startPoint = transform.parent.transform.position;
	}
	
	void Update ()
	{

	    if (_canMove)
	    {
	        //transform.position = JyoStickTouch();//手机平台由于有多点触摸，所以不能用mousePosition

	        transform.position = Input.mousePosition;//电脑平台可以用Input.mousePosition来模拟
	    }
	    else
	    {
	        transform.position = _startPoint;
	    }

	    _dis = Vector3.Magnitude(transform.position - _startPoint);
        if (_dis >= 200)
        {
            transform.position = Vector3.Lerp(_startPoint, transform.position, 200/_dis);
        }
	}

    private Vector2 JyoStickTouch()
    {
        for (i = 0; i < Input.touchCount; i++)
        {
            _touchPos = Input.touches[i].position;
            Debug.Log(_touchPos);
            if (_touchPos.x < 700)
            {
                
                return _touchPos;
            }
        }
        return _startPoint;
    }

    public void OnFingerDown()
    {
        _canMove = true;
    }

    public void OnFingerUp()
    {
        _canMove = false;
    }

}
