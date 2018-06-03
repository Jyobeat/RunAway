using UnityEngine;

/// <summary>
/// 这个是Boss特有的攻击方式：武器会跟踪玩家
/// </summary>
public class FollowWeapon : MonoBehaviour
{

    private Transform _player;
    public float _flySpeed = 2f;
    public float _flyRotateSpeed = 2f;
    public float _flyTime = 5f;
    public GameObject _blood;

    private Vector3 _targetDir;
    private bool _isDrop;
    private CapsuleCollider _collider;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag(_Data._tagPlayer).transform;
        _collider = GetComponent<CapsuleCollider>();
    }

    void Start ()
    {
        _isDrop = false;
        Invoke("DropDown", _flyTime);
    }
	
	
	void Update ()
	{
	    if (!_isDrop)
	    {
	        _targetDir = _player.position + Vector3.up;
	        transform.rotation = Quaternion.Slerp(transform.rotation,
	            Quaternion.LookRotation(_targetDir - transform.position),
	            _flyRotateSpeed*Time.deltaTime);
	        transform.position += transform.forward*_flySpeed*Time.deltaTime;
	    }
	    else
	    {
	        if (transform.position.y>0.1f)
	        {
	            transform.position += -Vector3.up*4f*Time.deltaTime;
	        }
	        else
	        {
	            transform.position += Vector3.forward*TerrainMoveManager.Instance._moveSpeed*Time.deltaTime;
	        }
	    }
        
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == _Data._tagPlayer)
        {
            collision.transform.SendMessage("TakeDamage");
            Instantiate(_blood, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)));
            Destroy(gameObject);
        }
    }

    private void DropDown()
    {
        _isDrop = true;
        _collider.enabled = false;
        Destroy(gameObject, 3f);
    }

}
