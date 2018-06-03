using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class LoadingScene : MonoBehaviour
{
    public  enum SceneNum
    {        
        StartScene,
        MenuScene,
        Tutorial,
        PlayScene
    }


    //进度条
    public Slider _loadSlider;
    //加载速度
    public float _loadingSpeed;

    private float _tempFloat;
    //下一个要加载的场景
    public SceneNum _nextScene;

    //异步对象
    private AsyncOperation _Async;

	void Start ()
	{
	    _loadSlider.value = 0f;
	    StartCoroutine(AsyncLoading(_nextScene));
	}

    IEnumerator AsyncLoading(SceneNum num)
    {
        _Async = SceneManager.LoadSceneAsync((int)num);
        _Async.allowSceneActivation = false;
        yield return _Async;
    }


    void Update ()
    {
        _tempFloat = _Async.progress;
        //_Async.progress的最大值为0.9，因此当他等于0.9时，滑动条要等于1
        if (_Async.progress >= 0.9f)
        {
            _tempFloat = 1f;
        }

        if (_tempFloat != _loadSlider.value)
        {
            _loadSlider.value = Mathf.Lerp(_loadSlider.value, _tempFloat, Time.deltaTime*_loadingSpeed);

            if (Mathf.Abs(_loadSlider.value - _tempFloat) < 0.01f)
            {
                _loadSlider.value = _tempFloat;
            }
        }

        if (_loadSlider.value == 1)
        {
            //允许异步加载完毕后自动进入场景.
            _Async.allowSceneActivation = true;
        }
    }
}
