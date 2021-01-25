using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using UnityEngine.UI;

//给空间添加监听事件要实现的一些接口
public class FragmentsControl : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler,
    IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public BaseFragment fragmentInformation;
    /// <summary>
    /// 鼠标拖动模块变量
    /// </summary>
    public RectTransform canvas;          //得到canvas的ugui坐标
    private RectTransform imgRect;        //得到图片的ugui坐标
    /// <summary>
    /// 游戏物体等变量
    /// </summary>
    private Transform parent;               //获得父物体
    private RectTransform parentRect;       //父物体的rect
    private Vector3 prePosition;            //之前位置
    public GameObject round;                //圆盘
    public GameObject information;          //信息牌
    private RectTransform roundRect;        //圆盘的rect
    private GameObject line;                //提示线
    private RectTransform lineRect;         //线的rect
    private GameObject angleTip;            //提示模板
    private RectTransform angleTipRect;

    /// <summary>
    /// 两个bool值用来调整拖动逻辑的
    /// </summary>
    private bool [] firstTime;         
    private bool endDrag;
    /// <summary>
    /// 判断信息版状态
    /// </summary>
    private bool ifClose;
    /// <summary>
    /// 判断是否在圆盘里面
    /// </summary>
    private bool inRound;
    private const float exp = (float)Math.PI * 2 / 360;
    private bool lineActive;
    /// <summary>
    /// 偏移量与缩放
    /// </summary>
    Vector2 offset = new Vector3();    //用来得到鼠标和图片的差值
    Vector3 imgReduceScale = new Vector3(1f, 1f, 1);   //设置图片缩放
    Vector3 imgMovingScale = new Vector3(0.6f, 0.6f, 1);
    Vector3 imgNormalScale = new Vector3(0.4f, 0.4f, 1);   //正常大小

    // Use this for initialization
    void Start()
    {
        firstTime = new bool[2];
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
        imgRect = GetComponent<RectTransform>();
        parent = transform.parent;
        parentRect = parent.GetComponent<RectTransform>();
        roundRect = round.GetComponent<RectTransform>();
        line = round.transform.GetChild(0).gameObject;
        lineRect = line.GetComponent<RectTransform>();
        firstTime[0] = true;
        firstTime[1] = true;
        endDrag = false;
        inRound = false;
        ifClose = true;
        lineActive = false;
        

        switch(/*fragmentInformation.model*/FragmentModel.thirty)
        {
            case FragmentModel.thirty:
                angleTip = round.transform.GetChild(1).gameObject;
                angleTipRect = angleTip.GetComponent<RectTransform>();
                break;
            case FragmentModel.sixty:
                angleTip = round.transform.GetChild(2).gameObject;
                angleTipRect = angleTip.GetComponent<RectTransform>();
                break;
            case FragmentModel.ninety:
                angleTip = round.transform.GetChild(3).gameObject;
                angleTipRect = angleTip.GetComponent<RectTransform>();
                break;
            case FragmentModel.oneHundredAndTwenty:
                angleTip = round.transform.GetChild(4).gameObject;
                angleTipRect = angleTip.GetComponent<RectTransform>();
                break;
        }
    }

    /// <summary>
    /// 当鼠标按下时调用 接口对应  IPointerDownHandler
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 mouseDown = eventData.position;    //记录鼠标按下时的屏幕坐标
        Vector2 mouseUguiPos = new Vector2();   //定义一个接收返回的ugui坐标
        if(firstTime[0])
        {
            prePosition = imgRect.anchoredPosition;
            firstTime[0] = false;
        }
            
        //RectTransformUtility.ScreenPointToLocalPointInRectangle()：把屏幕坐标转化成ugui坐标
        //canvas：坐标要转换到哪一个物体上，这里img父类是Canvas，我们就用Canvas
        //eventData.enterEventCamera：这个事件是由哪个摄像机执行的
        //out mouseUguiPos：返回转换后的ugui坐标
        //isRect：方法返回一个bool值，判断鼠标按下的点是否在要转换的物体上
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDown, eventData.enterEventCamera, out mouseUguiPos);
        if (isRect)   //如果在
        {
            //计算图片中心和鼠标点的差值
            offset = imgRect.anchoredPosition - mouseUguiPos;
        }
        Debug.Log(offset);
    }

    /// <summary>
    /// 当鼠标拖动时调用   对应接口 IDragHandler
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        //if (offset.x < -618.0f && offset.x > -775f && offset.y < -474.0f && offset.y > -622.1f) 
        {
            Vector2 mouseDrag = eventData.position;   //当鼠标拖动时的屏幕坐标
            Vector2 uguiPos = new Vector2();   //用来接收转换后的拖动坐标
                                               //和上面类似
            bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDrag, eventData.enterEventCamera, out uguiPos);
            //与scroll的交互
            if (firstTime[1] && Math.Abs(prePosition.x - imgRect.anchoredPosition.x) > 150f)
            {
                RectTransform rectTransform = imgRect;
                transform.SetParent(transform.parent.parent);
                imgRect = rectTransform;
                firstTime[1] = false;
                imgRect.localScale = imgMovingScale;
            }
            if (!firstTime[1] && Math.Abs(prePosition.x - imgRect.anchoredPosition.x) < 100f)
            {
                RectTransform rectTransform = imgRect;
                transform.SetParent(parent);
                ChangeChildOrder();
                imgRect = rectTransform;
                firstTime[1] = true;
                endDrag = true;
                imgRect.localScale = imgReduceScale;
            }

            //与round的交互
            if (Vector2.Distance(imgRect.anchoredPosition, roundRect.anchoredPosition) < 500f)
            {
                ShowLine();
                Vector2 vector2 = imgRect.anchoredPosition - roundRect.anchoredPosition;
                float angle = Vector2.Angle(new Vector2(0, 1), vector2);
                if (imgRect.anchoredPosition.x > roundRect.anchoredPosition.x)
                    imgRect.localEulerAngles = new Vector3(0, 0, 360 - angle);
                else
                    imgRect.localEulerAngles = new Vector3(0, 0, angle);
                inRound = true;
                int index = (int)(imgRect.localEulerAngles.z + 3) / 6;
                lineRect.localEulerAngles = new Vector3(0, 0, index * 6);
                angleTipRect.localEulerAngles = new Vector3(0, 0, index * 6);
                if(endDrag)
                    imgRect.localScale = imgMovingScale;
            }
            else
            {
                HideLine();
                imgRect.localEulerAngles = new Vector3(0, 0, 0);
                inRound = false;
            }

            if (isRect && !endDrag)
            {
                //设置图片的ugui坐标与鼠标的ugui坐标保持不变
                imgRect.anchoredPosition = offset + uguiPos;
                //Debug.Log(offset + "+" + uguiPos);
            }
            if (!ifClose)
                HideInformation();
            if(endDrag)
                endDrag = false;
        }
    }

    /// <summary>
    /// 当鼠标抬起时调用  对应接口  IPointerUpHandler
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        offset = Vector2.zero;
        HideLine();
    }

    /// <summary>
    /// 当鼠标结束拖动时调用   对应接口  IEndDragHandler
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        offset = Vector2.zero;
        if (firstTime[1])
        {
            imgRect.anchoredPosition = prePosition;
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentRect);
        }
        endDrag = true;
        if(inRound)
        {
            int index = (int)imgRect.localEulerAngles.z / 6;
            imgRect.anchoredPosition = roundRect.anchoredPosition + new Vector2(-(float)Math.Sin(index * 6 * exp), (float)Math.Cos(index * 6 * exp)) * 200f;
            imgRect.localEulerAngles = new Vector3(0, 0, index * 6);
            imgRect.localScale = imgReduceScale;
        }
        else
        {
            RectTransform rectTransform = imgRect;
            transform.SetParent(parent);
            ChangeChildOrder();
            imgRect = rectTransform;
            firstTime[1] = true;
            endDrag = true;
            imgRect.localScale = imgReduceScale;
        }
    }

    /// <summary>
    /// 当鼠标进入图片时调用   对应接口   IPointerEnterHandler
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(firstTime[1])
        {
            imgRect.localScale = imgReduceScale;   //变化图片
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentRect);
            ShowInformation();
        }
    }

    /// <summary>
    /// 当鼠标退出图片时调用   对应接口   IPointerExitHandler
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (firstTime[1])
        {
            imgRect.localScale = imgNormalScale;   //恢复图片
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentRect);
        }   
        HideInformation();
    }
    /// <summary>
    /// 用于重新归队时根据位置调整次序
    /// </summary>
    private void ChangeChildOrder()
    {
        for(int i=0; i<parent.childCount; i++)
        {
            if(imgRect.anchoredPosition.y>parent.GetChild(i).GetComponent<RectTransform>().anchoredPosition.y)
            {
                transform.SetSiblingIndex(i);
                return;
            }
        }
    }
    /// <summary>
    /// 改变信息版内容
    /// </summary>
    private void ShowInformation()
    {
        information.SetActive(true);
        //根据name改变信息版内容
        ifClose = false;
    }
    private void HideInformation()
    {
        information.SetActive(false);
        ifClose = true;
    }
    private void ShowLine()
    {
        if(!lineActive)
        {
            line.SetActive(true);
            angleTip.SetActive(true);
            lineActive = true;

        }
    }
    private void HideLine()
    {
        if(lineActive)
        {
            line.SetActive(false);
            angleTip.SetActive(false);
            lineActive = false;
        }
    }
}
