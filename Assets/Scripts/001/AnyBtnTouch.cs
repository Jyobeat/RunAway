using UnityEngine;

public class AnyBtnTouch : MonoBehaviour
{
    public GameObject _loadScene;

    public void OnClickAnyBtn()
    {
        _loadScene.GetComponent<LoadingScene>()._nextScene = LoadingScene.SceneNum.MenuScene;
        _loadScene.SetActive(true);
    }
}
