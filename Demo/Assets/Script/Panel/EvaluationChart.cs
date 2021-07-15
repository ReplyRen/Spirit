using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluationChart : MonoBehaviour
{
    List<Vector2> points = new List<Vector2>();

    Vector2 left = new Vector2(-218.7f, 71.8f);
    Vector2 up = new Vector2(-2.8f, 236.4f);
    Vector2 right = new Vector2(217.5f, 78.1f);
    Vector2 downLeft = new Vector2(-136f, -185f);
    Vector2 downRight = new Vector2(136.9f,-184.2f);
    private void Start()
    {
        
    }
    public void Init(Evaluation obj)
    {
        points.Add(SetPoint(obj.flavor, left));
        points.Add(SetPoint(obj.rich, up));
        points.Add(SetPoint(obj.intensity, right));
        points.Add(SetPoint(obj.continuity, downRight));
        points.Add(SetPoint(obj.fineness, downLeft));
        points.Add(SetPoint(obj.flavor, left));
        DrawLine();
    }
    private Vector2 SetPoint(float num,Vector2 maxPos)
    {
        float a = num / 100;
        return maxPos * a;
    }
    private void DrawLine()
    {
        LineRenderer lineRenderer = gameObject.GetComponentInChildren<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        for (int i = 0; i < points.Count; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }
        points.Clear();
    }
}
