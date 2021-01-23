using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentsUpdate : MonoBehaviour
{
    private void OnEnable()
    {
        InitialFragments();
    }
    public void InitialFragments()
    {
        ClearFragments();
        foreach(var fragment in FragmentsManager.GetFragments())
        {
            fragment.obj = StaticMethod.LoadPrefab("Prefab/Fragments/" + fragment.name);
            fragment.obj.transform.SetParent(transform);
            fragment.obj.GetComponent<FragmentsControl>().name = fragment.name;
        }
    }
    private void ClearFragments()
    {
        for (int i = transform.childCount - 1; i >= 0; i--) 
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
