using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostage_AI : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == _Data._tagPlayer)
        {
            TerrainMoveManager.Instance._moveSpeed = 1f;
            TutorialLogicManager.Instance.WaitToShow();
        }
    }
}
