using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMoney : MonoBehaviour {


    public void OnDoTweenEnd()
    {
        gameObject.SetActive(false);
    }

}
