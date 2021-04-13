using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();
    int month = 1;
    GameObject taskButton;
    GameObject taskList;
    GameObject taskPanel;
    GameObject tip;
    public GameObject libraryButton;
    public GameObject tips;
    EvaluationPanel evaluationPanel;
    GameManager gameManager;
    bool isFailed = false;
    string name = null;
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
        tips.SetActive(true);
    }
    public void OpenPanel()
    {
        taskPanel.SetActive(true);
        for(int i= tasks.Count-1; i>=0;i--)
        {
            for (int j = taskList.transform.childCount - 1; j >= 0; j--) 
            {
                if (taskList.transform.GetChild(j).gameObject.name == tasks[i].name)
                {
                    if (tasks[i].isFinished && !isFailed)
                    {
                        taskList.transform.GetChild(j).transform.GetChild(4).gameObject.SetActive(true);
                        if (tasks[i].roundCount <= -2)
                        {
                            tasks.Remove(tasks[i]);
                            Destroy(taskList.transform.GetChild(j).gameObject);
                        }

                    }
                    if (tasks[i].roundLimit == 0) taskList.transform.GetChild(j).GetChild(2).GetComponent<Text>().text = tasks[i].bonus;
                    else if (tasks[i].roundCount > 0)
                        taskList.transform.GetChild(j).GetChild(2).GetComponent<Text>().text = "剩余" + "<color=red>" + tasks[i].roundCount + "</color>" + "月" + "   " + tasks[i].bonus;
                    else
                        taskList.transform.GetChild(j).GetChild(2).GetComponent<Text>().text = "剩余时间无";
                }
            }
            
        }
        tip.SetActive(false);
       /* Debug.Log("month:"+month);
        Debug.Log("isfailed" + isFailed);
        for(int i=0;i<2;i++)
        {
            Debug.Log(tasks[i].isDoing + "+" + tasks[i].isFinished + i);
            Debug.Log("roundCount" + i + ":" + tasks[i].roundCount);
            Debug.Log(tasks[i].step);
        }*/
    }
    public void ClosePanel()
    {
        taskPanel.SetActive(false);
    }
    public void Check(List<BaseFragment> newList)
    {
        for(int j = 0; j < newList.Count; j++) 
        {
            for(int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].step == newList[j].name) 
                {
                    if (tasks[i].step == "鉴酒") 
                    {
                        score = evaluationPanel.GetScore(newList[j].baseObject) * 100;
                        Debug.Log(score);
                        name = newList[j].baseObject.GetKind().ToString();
                        if (tasks[i].roundLimit != 0)
                        {
                            if (tasks[i].roundCount > 0)
                            {
                                tasks[i].isFinished = true;
                            }
                            else
                            {
                                tasks[i].isFinished = false;
                                break;
                            }
                        }
                        if(tasks[i].targetScore!=0)
                        {
                            if(tasks[i].targetScore<=score)
                            {
                                tasks[i].isFinished = true;
                            }
                            else
                            {
                                tasks[i].isFinished = false;
                                break;
                            }
                        }
                        if(tasks[i].category!=null)
                        {
                            if(tasks[i].category==name)
                            {
                                tasks[i].isFinished = true;
                            }
                            else
                            {
                                tasks[i].isFinished = false;
                            }
                        }
                    }
                    else
                    {
                        if (tasks[i].roundLimit != 0)
                        {
                            if (tasks[i].roundCount > 0)
                            {
                                tasks[i].isFinished = true;
                            }
                            else
                            {
                                tasks[i].isFinished = false;
                                break;
                            }
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
    IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(1.2f);
        taskButton.SetActive(false);
        libraryButton.SetActive(false);
        Check(gameManager.fragmentOnDisc);
        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[i].roundLimit != 0)
            {
                if (tasks[i].isDoing)
                {
                    tasks[i].roundCount--;
                    if (tasks[i].roundCount == 0)
                    {
                        tasks[i].isDoing = false;
                        tasks[i].roundCount--;
                    }
                }
                else if(tasks[i].roundCount<0)
                    tasks[i].roundCount--;
            }
            else if(tasks[i].isFinished&&tasks[i].isDoing)
            {
                tasks[i].roundCount--;
            }
            if (month == tasks[i].instanceRound)
            {
                tasks[i].isDoing = true;
                tasks[i].isFinished = false;
                InstanceTask(tasks[i]);
            }
        }
        Settle();
    }
    public void NextMonth()
    {
        month++;
        StartCoroutine(WaitFor());
        name = null;
        score = 0;
    }
    IEnumerator WaitFor2()
    {
        yield return new WaitForSeconds(1.2f);
        taskButton.SetActive(true);
        libraryButton.SetActive(true);
    }
    public void Switch()
    {
        StartCoroutine(WaitFor2());
    }
    public void Settle()//jiesuan
    {
        for(int i=0;i<tasks.Count;i++)
        {
            if (!tasks[i].isDoing && !tasks[i].isFinished)
            {
                isFailed = true;
                break;
            }
        }
    }
    void Start()
    {
        libraryButton = GameObject.Find("Library").gameObject;
        taskButton = gameObject.transform.Find("Task").gameObject;
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
        evaluationPanel = GameObject.Find("PanelCanvas").transform.Find("评价Panel").GetComponent<EvaluationPanel>();
        tip = taskButton.transform.GetChild(0).gameObject;
        tips = taskButton.transform.Find("Tips").gameObject;
        taskPanel = gameObject.transform.Find("TaskPanel").gameObject;
        taskList = taskPanel.transform.Find("TaskList").gameObject;
        taskPanel.SetActive(false);
        taskButton.SetActive(false);
        libraryButton.SetActive(false);
        for(int i=0;i<2;i++)
        {
            LoadTask("任务" + (i + 1));
        }
        //gameObject.SetActive(false);
        //Test();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&tips.activeSelf)
            tips.SetActive(false);
    }
}
