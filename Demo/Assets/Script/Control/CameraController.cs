using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    private float normalSize;
    public float targetSize;

    public float sizeSpeed;

    private bool enlarge = false;
    private bool ensmall = false;

    public bool large = false;

    private Vector3 normalPos;
    private Vector3 targetPos;

    private Vector2 oldPos1 = Vector2.zero;
    private Vector2 oldPos2 = Vector2.zero;

    Vector3 currentVelocity = new Vector3(0, 0, 0);     // 当前速度，这个值由你每次调用这个函数时被修改
    float maxSpeed = 10f;    // 选择允许你限制的最大速度
    public float smoothTime = 0.4f;      // 达到目标大约花费的时间。 一个较小的值将更快达到目标。

    public bool locked = false;

    private void Start()
    {
        normalPos = gameObject.transform.position;
        normalSize = gameObject.GetComponent<Camera>().orthographicSize;
        Input.multiTouchEnabled = true;
    }
    private Vector3 GetCenter(Image img)
    {
        Vector3[] corners = new Vector3[4];
        img.rectTransform.GetWorldCorners(corners);
        Vector3 center = new Vector3((corners[0].x + corners[2].x) / 2, (corners[0].y + corners[2].y) / 2, -10);
        return center;
    }
    public void SetCamera(Image img)
    {
        targetPos = GetCenter(img);
        enlarge = true;
        if(img.name== "蒸煮室" && GuideControl.id==3)
        {
            GuideControl.id = 203;
            gameObject.GetComponent<GuideControl>().Run();
        }
    }
    private void Update()
    {
        if (Input.touchCount > 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                Vector2 tempPos1 = Input.GetTouch(0).position;
                Vector2 tempPos2 = Input.GetTouch(1).position;
                if (IsEnsmall(oldPos1, oldPos2, tempPos1, tempPos2))
                {
                    ensmall = true;
                    if(GuideControl.id==10)
                    {
                        GuideControl.id = 228;
                        GameObject.Find("Main Camera").GetComponent<GuideControl>().Run();
                    }
                }
                oldPos1 = tempPos1;
                oldPos2 = tempPos2;
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            ensmall = true;
            if (GuideControl.id == 10)
            {
                GuideControl.id = 228;
                GameObject.Find("Main Camera").GetComponent<GuideControl>().Run();
            }
        }

    }
    private void FixedUpdate()
    {
        if (enlarge && !large&&!locked)
        {
            ensmall = false;
            gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, targetSize, Time.fixedDeltaTime * sizeSpeed);
            gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, targetPos, ref currentVelocity, smoothTime, maxSpeed);
            if (IsClose(gameObject.GetComponent<Camera>().orthographicSize, targetSize) && IsClose(gameObject.transform.position, targetPos))
            {
                enlarge = false;
                large = true;
            }
        }
        if (ensmall && large&&!locked)
        {
            enlarge = false;
            gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, normalSize, Time.fixedDeltaTime * sizeSpeed);
            gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, normalPos, ref currentVelocity, smoothTime, maxSpeed);
            if (IsClose(gameObject.GetComponent<Camera>().orthographicSize, normalSize) && IsClose(gameObject.transform.position, normalPos))
            {
                ensmall = false;
                large = false;
            }
        }
    }
    private bool IsClose(Vector3 v1, Vector3 v2)
    {
        if ((v2 - v1).magnitude < 0.1f)
            return true;
        else
            return false;
    }
    private bool IsClose(float v1, float v2)
    {

        if (Mathf.Abs(v2 - v1) < 0.01f)
            return true;
        else
            return false;
    }

    private bool IsEnsmall(Vector2 oldPos1, Vector2 oldPos2, Vector3 newPos1, Vector3 newPos2)
    {
        float leng1 = Mathf.Sqrt((oldPos1.x - oldPos2.x) * (oldPos1.x - oldPos2.x) + (oldPos1.y - oldPos2.y) * (oldPos1.y - oldPos2.y));
        float leng2 = Mathf.Sqrt((newPos1.x - newPos2.x) * (newPos1.x - newPos2.x) + (newPos1.y - newPos2.y) * (newPos1.y - newPos2.y));
        if (leng1 < leng2)
            return false;
        else
            return true;
    }

}
