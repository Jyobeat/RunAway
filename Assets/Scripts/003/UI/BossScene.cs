
using UnityEngine;


public class BossScene : MonoBehaviour
{

    private Vector3 _startPos = new Vector3(1920, 0, 0);


    public void OnDotweenComplete()
    {
        Time.timeScale = 1f;
        transform.localPosition = _startPos;
        gameObject.SetActive(false);
    }
}
