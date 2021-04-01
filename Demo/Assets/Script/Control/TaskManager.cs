using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();
    int month = 1;
    GameObject taskList;
    GameObject taskPanel;
    GameObject tip;
    bool isFailed = false;
    string name;
    float score;
    public void LoadTask(string name)
    {
        Task task = new Task();
        TextAsset taskData = Resources.Load("Data/" + name) as TextAsset;
        string[] des = taskData.text.Split('|');
        task.instanceRound = int.Parse(des[0]);
        task.name = des[1];
        for(int i=2;i<des.Length-3;i++)
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
        task.bonus = des[des.Length-3];
        task.step = des[des.Length - 2];
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
        tip.SetActive(true);
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
        tip.SetActive(false);
    }
    public void ClosePanel()
    {
        taskPanel.SetActive(false);
    }
    public void Check(List<BaseFragment> newList)
    {
        for(int i=0;i<tasks.Count;i++)
        {
            for(int j=0;j<newList.Count;j++)
            {
                if (tasks[i].step == newList[i].name) 
                {
                    if (tasks[i].step == "鉴酒") 
                    {
                        if (tasks[i].roundLimit != 0)
                        {
                            if (tasks[i].roundCount > 0)
                            {
                                tasks[i].isFinished = true;
                                tasks[i].isDoing = false;
                            }
                            else
                            {
                                tasks[i].isFinished = false;
                            }
                        }
                    }
                    else
                    {

                    }
                }
            }
        }
    }
    public void Check1(List<BaseFragment> newList)
    {
        for (int j = 0; j < newList.Count; j++)
        {
            if (newList[j].name == "鉴酒")
            {
                /*name =;
                score =;*/
                for (int i = 0; i < tasks.Count; i++)
                {
                    tasks[i].isDoing = false;
                    if (tasks[i].name != null)
                    {
                        if (tasks[i].name == name)
                        {
                            tasks[i].isFinished = true;
                            tasks[i].isDoing = false;
                        }
                        else continue;
                    }
                    if(tasks[i].targetScore!=0)
                    {
                        if(score>= tasks[i].targetScore)
                        {
                            tasks[i].isFinished = true;
                            tasks[i].isDoing = false;
                        }
                        else
                        {
                            tasks[i].isFinished = false;
                            continue;
                        }
                    }
                    if(tasks[i].roundLimit!=0)
                    {
                        if(tasks[i].roundCount>0)
                        {
                            tasks[i].isFinished = true;
                            tasks[i].isDoing = false;
                        }
                        else
                        {
                            tasks[i].isFinished = false;
                        }
                    }
                }
            }
        }
    }
    void Test()
    {
        for(int i=0;i<2;i++)
        {
            LoadTask("任务" + (i + 1));
            InstanceTask(tasks[i]);
        }
    }
    public void NextMonth()//xiecheng
    {
        month++;
        for(int i=0;i<tasks.Count;i++)
        {
            if(month==tasks[i].instanceRound)
            {
                tasks[i].isDoing = true;
                tasks[i].isFinished = false;
                InstanceTask(tasks[i]);
            }
            if (tasks[i].isDoing)
            {
                tasks[i].roundCount--;
                if (tasks[i].roundLimit != 0 && tasks[i].roundCount == 0)
                    tasks[i].isDoing = false;
            }
        }
        name = null;
        score = 0;
        gameObject.SetActive(false);
    }
    public void Settle()//jiesuan
    {
        gameObject.SetActive(true);
    }
    void Start()
    {
        tip = GameObject.Find("Task").transform.GetChild(0).gameObject;
        taskPanel = gameObject.transform.Find("TaskPanel").gameObject;
        taskList = taskPanel.transform.Find("TaskList").gameObject;
        gameObject.SetActive(false);
        Test();
    }
    void Update()
    {
        
    }
}
