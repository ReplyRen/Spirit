using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks = new List<Task>();
    GameObject taskList;
    GameObject finishBtn;
    Text taskDescription;
    Text reward;
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
                task.roundCount = int.Parse(des[i]);
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
        tasks.Add(task);
    }
    public void InstanceTask(Task task)
    {
        GameObject temp = Resources.Load("Assets/Resources/Prefab/UIPrefab/Task") as GameObject;
        Button btn = temp.GetComponent<Button>();
        temp.transform.SetParent(taskList.transform);
        temp.name = task.name;
        temp.transform.GetChild(0).GetComponent<Text>().text = task.name;
        btn.onClick.AddListener(OnClickTask);
    }
    public void OnClickTask()
    {
        var btn = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (btn.gameObject.transform.GetChild(1).gameObject.activeSelf) btn.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        for(int i=0;i<tasks.Count;i++)
        {
            if(tasks[i].name==btn.name)
            {
                taskDescription.text = tasks[i].description;
                reward.text = tasks[i].bonus;
                if (tasks[i].isFinished) SetFinish();
                else ResetFinish();
            }
        }
    }
    void ResetFinish()
    {
        finishBtn.GetComponent<Image>().color = new Color(1, 1, 1, 0.35f);
        finishBtn.transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 0.35f);
        finishBtn.GetComponent<Button>().interactable = false;
    }
    void SetFinish()
    {
        finishBtn.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        finishBtn.transform.GetChild(0).GetComponent<Text>().color = new Color(1, 1, 1, 1);
        finishBtn.GetComponent<Button>().interactable = true;
    }
    void Test()
    {

    }
    void Start()
    {
        taskList = gameObject.transform.Find("TaskList").gameObject;
        taskDescription = gameObject.transform.Find("TaskDes").GetComponent<Text>();
        reward = gameObject.transform.Find("Reward").GetComponent<Text>();
        finishBtn = gameObject.transform.Find("Finish").gameObject;
    }
    void Update()
    {
        
    }
}
