using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public float MoveSpeed = 0.06f;
    public GameObject obj;
    private bool IsMoving = false;
    private float BezierVal = 0;
    public Vector3 OrignalPoint,TargetPoint,ControlPoint;
    public void PlayeEffect()
    {

        if (!IsMoving)
        {
            obj.SetActive(true);
            BezierVal = 0;
            StartCoroutine(MoveToward());

        }
    }
    public void InitTrail(Vector3 From,Vector3 To,Vector3 Control)
    {
        OrignalPoint = From;
        TargetPoint = To;
        ControlPoint = Control;
        StartCoroutine(MoveToward());

    }
    //贝塞尔曲线
    Vector3 Bezier(Vector3 a,Vector3 b,Vector3 c,float t)
    {
        return (1 - t) * (1 - t) * a + 2 * t * (1 - t) * b + t * t * c;
    }
    IEnumerator MoveToward()
    {
        int move_times = 0;
        IsMoving = true;
        Vector3 direction = Vector3.zero;
        float distance = 0;
        do
        {
            distance = Vector3.Distance(TargetPoint, obj.transform.position);
            direction = (TargetPoint - obj.transform.position).normalized;
            obj.transform.up = direction;
            obj.transform.position = Bezier(OrignalPoint,ControlPoint,TargetPoint,BezierVal);
            move_times++;
            if (Vector3.Dot((TargetPoint - obj.transform.position), direction) < 0) break;
            yield return new WaitForSeconds(0.02f);
            BezierVal += 0.02f;
        }
        while (distance >= 0.02f);
        IsMoving = false;
        obj.SetActive(false);
        yield return null;
    }
#if UNITY_EDITOR
    //辅助设置
    private void OnDrawGizmosSelected()
    {
        Vector3 first, second;
        first = second = OrignalPoint;
        for(int i = 0;i <= 20; i++)
        {
            second = Bezier(OrignalPoint, ControlPoint, TargetPoint, (float)i / 20);
            Gizmos.DrawLine(first, second);
            first = second;
        }
    }
#endif
}
