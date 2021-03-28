using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();
    GameObject taskList;
    GameObject taskPanel;
    bool isFailed = false;
    public void LoadTask(string name)
    {
        Task task = new Task();
        TextAsset taskData = Resources.Load("Data/" + name) as TextAsset;
        string[] des = taskData.text.Split('|');
        task.name = des[0];
        for(int i=1;i<des.Length-2;i++)
        {
            if(des[i][0]=='#')
            {
                des[i] = des[i].Replace("#", "");
                task.roundLimit = int.Parse(des[i]);
                task.roundCount = task.roundLimit;
                des[i] = "<color=red>" + des[i] + "</color>";
            }
            if(des[i][0]=='$')
            {
                des[i] = des[i].Replace("$", "");
                task.targetScore = float.Parse(des[i]);
                des[i] = "<color=red>" + des[i] + "</color>";
            }
            if (des[i][0] == '%')
            {
                des[i] = des[i].Replace("%", "");
                task.category = des[i];
                des[i] = "<color=red>" + des[i] + "</color>";
            }
            task.description += des[i];
        }
        task.bonus = des[des.Length-2];
        task.isDoing = true;
        tasks.Add(task);
    }
    public void InstanceTask(Task task)
    {
        Object a = Resources.Load("Prefab/UIPrefab/Task") as GameObject;
        GameObject temp = Instantiate(a) as GameObject;
        temp.transform.SetParent(taskList.transform);
        temp.transform.localScale = new Vector3(1, 1, 1);
        temp.name = task.name;
        temp.transform.GetChild(0).GetComponent<Text>().text = task.name;
        temp.transform.GetChild(1).GetComponent<Text>().text = task.description;
        if (task.roundLimit == 0) temp.transform.GetChild(2).GetComponent<Text>().text = task.bonus;
        else temp.transform.GetChild(2).GetComponent<Text>().text = "剩余" + "<color=red>" + task.roundCount + "</color>" + "月" + "   " + task.bonus;
    }
    public void OpenPanel()
    {
        taskPanel.SetActive(true);
        for(int i=0;i<tasks.Count;i++)
        {
            if(tasks[i].isFinished)
            {
                taskList.transform.GetChild(i).transform.GetChild(4).gameObject.SetActive(true);
            }
        }
    }
    public void ClosePanel()
    {
        taskPanel.SetActive(false);
    }
    void Test()
    {
        for(int i=0;i<2;i++)
        {
            LoadTask("任务" + (i + 1));
            InstanceTask(tasks[i]);
        }
    }
    public void NextMonth()
    {
        for(int i=0;i<tasks.Count;i++)
        {
            if (tasks[i].isDoing) tasks[i].roundCount--;
            else tasks[i].roundCount++;
        }

        gameObject.SetActive(false);
    }
    public void Settle()
    {
        gameObject.SetActive(true);
    }
    void Start()
    {        
        taskPanel = gameObject.transform.Find("TaskPanel").gameObject;
        taskList = taskPanel.transform.Find("TaskList").gameObject;
        gameObject.SetActive(false);
        Test();
    }
    void Update()
    {
        
    }
}
