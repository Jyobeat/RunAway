using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 圆心镂空遮罩引导
/// </summary>
public class CircleGuidanceController : MonoBehaviour
{
    public Text _TxttutorialTips;

    /// <summary>
    /// 最大半径
    /// </summary>
    private float _maxRadius=3000f;

    /// <summary>
    /// 要高亮显示的目标数组
    /// </summary>
    public Image[] Target;
    /// <summary>
    /// 要高亮显示的目标索引
    /// </summary>
    private int index = 0;

    /// <summary>
    /// 是否正在被指引
    /// </summary>
    private bool _isTutorialing;

    /// <summary>
    /// 所依附的画布
    /// </summary>
    private Canvas canvas;

    /// <summary>
    /// 高亮区域范围缓存（4个顶点）
    /// </summary>
    private Vector3[] _corners = new Vector3[4];

    /// <summary>
    /// 镂空区域圆心
    /// </summary>
    private Vector2 _center;

    /// <summary>
    /// 镂空区域半径
    /// </summary>
    private float _radius;

    /// <summary>
    /// 遮罩材质
    /// </summary>
    private Material _material;

    /// <summary>
    /// 当前高亮区域的半径
    /// </summary>
    private float _currentRadius;

    /// <summary>
    /// 高亮区域缩放的动画时间
    /// </summary>
    private float _shrinkTime = 0.5f;

    /// <summary>
    /// 事件渗透组件
    /// </summary>
    private GuidanceEventPeneTrate _eventPeneTrate;

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
        /// 世界坐标转换为画布坐标
        /// </summary>
        /// <param name="canvas">画布目标</param>
        /// <param name="world">屏幕坐标</param>
        /// <returns>对应画布目标的坐标</returns>
    private Vector2 WorldToCanvasPos(Canvas canvas, Vector3 world)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, world,
            canvas.GetComponent<Camera>(), out position);
        return position;
    }

    private void Awake()
    {
        //获取事件渗透组件
        _eventPeneTrate = GetComponent<GuidanceEventPeneTrate>();

        //获取画布
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        NewbeeTutorial(Target[index]);

    }


    public void NewbeeTutorial(Image target )
    {
        _isTutorialing = true;
        _TxttutorialTips.gameObject.SetActive(true);
        _TxttutorialTips.text = _Data._tutorialTips[index];
        if (_eventPeneTrate != null)
        {
            _eventPeneTrate.SetTargetImager(target);
        }
        //得到最终要高亮区域的4个顶点的世界坐标
        target.rectTransform.GetWorldCorners(_corners);

        //得到要高亮显示的半径
        _radius = Vector2.Distance(WorldToCanvasPos(canvas, _corners[0]), WorldToCanvasPos(canvas, _corners[2])) * 0.5f;
        //计算要高亮显示的圆心的世界坐标
        float x = (_corners[0].x + _corners[2].x) * 0.5f;
        float y = (_corners[1].y + _corners[3].y) * 0.5f;
        Vector3 centerWorld = new Vector3(x, y, 0);
        //圆心在画布上的二维坐标
        _center = WorldToCanvasPos(canvas, centerWorld);

        //设置遮罩材质中的圆心变量
        Vector4 centerMat = new Vector4(_center.x, _center.y, 0, 0);
        _material = GetComponent<Image>().material;
        _material.SetVector("_Center", centerMat);

        //计算当前高亮显示区域的半径
        RectTransform canRectTransform = canvas.transform as RectTransform;
        if (canRectTransform != null)
        {
            //获取画布的四个顶点
            canRectTransform.GetWorldCorners(_corners);
            //将画布的4个顶点中的距离圆心最远的距离作为初始半径。
            foreach (var corner in _corners)
            {
                _currentRadius = Mathf.Max(Vector3.Distance(WorldToCanvasPos(canvas, corner), _center), _currentRadius);
            }
        }

        //设置遮罩材质的当前半径值
        _material.SetFloat("_Slider", _currentRadius);
    }



    private float _shrinkVelocity = 0f;

    private void Update()
    {
        if (_isTutorialing)
        {
            CircleShrink();
            if (Input.touchCount != 0)
            {
                if (Vector2.Distance(WorldToCanvasPos(canvas, Input.touches[0].position), _center) < _radius)
                {
                    _TxttutorialTips.gameObject.SetActive(false);
                    if (index == 0 && PlayerCharacteristic.Instance._knifeCount == 0)
                        return;
                    _isTutorialing = false;
                    index++;
                    if (index < 3)
                    {
                        StartCoroutine(NextTutorial());
                    }
                    if (index == 3)
                    {
                        TerrainMoveManager.Instance._moveSpeed = 1f;
                        TutorialLogicManager.Instance.CreateAIForTutorial();
                    }
                }
            }          
        }
        else
        {
            CircleExpand();
        }
    }

    /// <summary>
    /// 圆圈收缩
    /// </summary>
    private void CircleShrink()
    {
        //从当前半径到目标半径差值显示收缩动画
        float value = Mathf.SmoothDamp(_currentRadius, _radius, ref _shrinkVelocity, _shrinkTime);
        if (!Mathf.Approximately(value, _currentRadius))
        {
            _currentRadius = value;
            _material.SetFloat("_Slider", _currentRadius);
        }
    }

    private void CircleExpand()
    {
        float value = Mathf.Lerp(_currentRadius, _maxRadius, _shrinkTime*0.5f);
        if (!Mathf.Approximately(value, _currentRadius))
        {
            _currentRadius = value;
            _material.SetFloat("_Slider", _currentRadius);
        }
        _eventPeneTrate.SetTargetImager(transform.GetComponent<Image>());
    }

    IEnumerator NextTutorial()
    {
        yield return new WaitForSeconds(1f);
        NewbeeTutorial(Target[index]);
        
    }

}
