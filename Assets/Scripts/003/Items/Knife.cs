using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class Knife : MonoBehaviour
{
    public float _flyingSpeed = 6f;
    public float _dropRate = 0.33f;
    public GameObject _blood;
    private float _randomRate;

    private CapsuleCollider _selfCollider;
    private Knife _knife;
    private ItemKnife _itemKnife;

    private void Awake()
    {
        _selfCollider = GetComponent<CapsuleCollider>();
        _knife = this;
        _itemKnife = GetComponent<ItemKnife>();
        transform.localScale = Vector3.one;
    }

    //小刀从对象池取出时会调用此函数，初始化小刀掉落的概率
    void OnSpawn()
	{
        
        _randomRate = Random.Range(0f, 1f);
	    if (_randomRate <= _dropRate)
	    {
	        Invoke("KnifeDrop", 0.7f);            
	    }
	    else
	    {
	        LeanPool.Despawn(gameObject, 2f);
	    }

	}
	
	
	void FixedUpdate ()
	{
	    transform.localPosition += transform.up*_flyingSpeed*Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "AI")
        {
            other.SendMessage("TakeDamage");
            Instantiate(_blood, transform.position, Quaternion.Euler(new Vector3(0,0,90)));
            LeanPool.Despawn(gameObject);
        }
    }

    //刀掉落地上
    private void KnifeDrop()
    {
        
        _selfCollider.isTrigger = false;
        _knife.enabled = false;
        _itemKnife.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Item");
    }
    //小刀被回收到对象池时调用
    void OnDespawn()
    {

        gameObject.layer = LayerMask.NameToLayer("Enemy");
        CancelInvoke("KnifeDrop");
        _selfCollider.isTrigger = true;
        _knife.enabled = true;
        _itemKnife.enabled = false;
    }

}
