using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuidanceEventPeneTrate : MonoBehaviour,ICanvasRaycastFilter
{
    private Image _targetImage;

    public void SetTargetImager(Image target)
    {
        _targetImage = target;
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (_targetImage == null)
        {
            return true;
        }

        return !RectTransformUtility.RectangleContainsScreenPoint(_targetImage.rectTransform, sp, eventCamera);
    }

    
}
