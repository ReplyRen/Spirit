using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineChart : MonoBehaviour
{
    GameObject point;
    List<Vector3> pointPos = new List<Vector3>();
    private void Start()
    {
        point = GameObject.Find("Point");
        point.SetActive(false);
        Test();
    }
    private void Test()
    {
        Queue<float> que = new Queue<float>();
        que.Enqueue(0.3f);
        que.Enqueue(0.6f);
        que.Enqueue(1f);
        que.Enqueue(0f);
        que.Enqueue(0.51f);
        que.Enqueue(0.16f);
        Init(que);
    }
    public void Init(Transform parent,Vector3 localPos, Queue<float> nums)
    {
        gameObject.transform.SetParent(parent);
        gameObject.transform.localPosition = localPos;
        SetPoint(nums);
        DrawLine();
    }
    public void Init(Queue<float> nums)
    {
        SetPoint(nums);
        DrawLine();
    }
    private void SetPoint(Queue<float> nums)
    {
        int count = nums.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(point, gameObject.transform);
            obj.SetActive(true);
            obj.transform.localPosition = new Vector2(-145 + 60 * i, -188 + 362 * nums.Dequeue());
            pointPos.Add(obj.transform.localPosition);
        }
    }
    private void DrawLine()
    {
        LineRenderer lineRenderer = gameObject.GetComponentInChildren<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        Debug.Log(pointPos.Count);
        for (int i = 0; i < pointPos.Count; i++) 
        {
            lineRenderer.SetPosition(i, pointPos[i]);
        }
    }
}
