
using UnityEngine;
using UnityEngine.UI;

public class ThreeUIManager : MonoBehaviour
{
    public static ThreeUIManager Instance;
    private int _childsCount;

    private void Awake()
    {
        Instance = this;
    }

    public void ReflashUI(int count,Transform parent)
    {
        if (count < 0)
            return;
        _childsCount = parent.childCount;
        for (int i = 0; i < _childsCount; i++)
        {
            if (i < count)
            {
                parent.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                parent.GetChild(i).gameObject.SetActive(false);
            }
        }
        
    }

    
}
