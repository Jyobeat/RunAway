using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialKnife : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.SendMessage("PickUp", SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}
