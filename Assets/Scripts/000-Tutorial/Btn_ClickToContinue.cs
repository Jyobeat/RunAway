using UnityEngine;

public class Btn_ClickToContinue : MonoBehaviour {

    public void OnClickContinue()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        if (TutorialLogicManager.Instance._teachingIndex == 9)
        {
            TutorialLogicManager.Instance.LoadGamePlayingScene();
        }
    }
}
