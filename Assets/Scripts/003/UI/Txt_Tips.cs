using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Txt_Tips : MonoBehaviour
{
    private Text _tips;

	void Start ()
	{
	    _tips = GetComponent<Text>();
	    _tips.text = _Data._tips[Random.Range(0, _Data._tips.Count)];
	}
	

}
