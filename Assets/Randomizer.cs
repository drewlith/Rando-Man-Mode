using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Randomizer : MonoBehaviour
{
    public string accountName;
    public static RandoTask currentTask;
    public static int difficulty;
    List<RandoTask> allAvailableTasks = new List<RandoTask>();
    public List<RandoTask> allTasks;
    public RandoTask failsafeTask;
    public Text taskText;
    public Text textShadow;
    public Image[] imageDisplay;
    int counter;

    void Start() {
        SelectRandomTask(true);
    }

    void FixedUpdate() {
        counter += 1;
        if (counter % 200 == 0) {
            Save(accountName);
        }
    }

    public void BuildTaskList() {
        allAvailableTasks.Clear();
        DetermineDifficulty();
        for (int i = 0; i < allTasks.Count; i++) {
            bool addTask = true;
            if (allTasks[i].completed) {
                addTask = false;
            }
            if (currentTask != null) {
                if (allTasks[i].type == currentTask.type && Random.Range(0,100) < 80) { // 20% chance for repeat task type
                    addTask = false;
                }
            }
            for (int j = 0; j < allTasks[i].requirements.Length;j++) { 
                if (!allTasks[i].requirements[j].completed) {
                    addTask = false;
                    break;
                }
            }
            if (addTask) {
                if (allTasks[i].difficulty <= difficulty) {
                    allAvailableTasks.Add(allTasks[i]);
                }
            }
        }
        if (allAvailableTasks.Count <= 0) {
            allAvailableTasks.Add(failsafeTask);
        }
    }

    public void SelectRandomTask(bool start = false) {
        if (!start) {
            currentTask.CompleteTask();
        }
        BuildTaskList();
        int rng = Random.Range(0, allAvailableTasks.Count);
        currentTask = allAvailableTasks[rng];
        taskText.text = currentTask.name;
        textShadow.text = currentTask.name;
        Rank.CheckRank();
        DetermineImageToDisplay();
    }

    void DetermineDifficulty() {
        int tasksCompleted = 0;
        for (int i = 0; i < allTasks.Count; i++) {
            if (allTasks[i].completed && !allTasks[i].repeatable) {
                tasksCompleted += 1;
            }
        }
        if (tasksCompleted > 1600) {
            difficulty = 11;
        }
        if (tasksCompleted > 1400) {
            difficulty = 10;
        }
        else if (tasksCompleted > 1000) {
            difficulty = 9;
        }
        else if (tasksCompleted > 700) {
            difficulty = 8;
        }
        else if (tasksCompleted > 480) {
            difficulty = 7;
        }
        else if (tasksCompleted > 340) {
            difficulty = 6;
        }
        else if (tasksCompleted > 220) {
            difficulty = 5;
        }
        else if (tasksCompleted > 120) {
            difficulty = 4;
        }
        else if (tasksCompleted > 70) {
            difficulty = 3;
        }
        else if (tasksCompleted > 40) {
            difficulty = 2;
        }
        else if (tasksCompleted > 20) {
            difficulty = 1;
        } else {
            difficulty = 0;
        }
    }

    void DetermineImageToDisplay() {
        for (int i = 0; i < imageDisplay.Length; i++) {
            imageDisplay[i].gameObject.SetActive(false);
        }
        imageDisplay[currentTask.type].gameObject.SetActive(true);
    }

    public void Save(string accountName) {
        if (accountName.Length <= 0) {
            return;
        }
        using (StreamWriter sw = File.CreateText(Application.dataPath + "/" + accountName + ".txt")) {
            for (int i = 0; i < allTasks.Count; i++) {
                sw.WriteLine(allTasks[i].name + " = " + allTasks[i].completed.ToString());
            }
            sw.WriteLine(currentTask.name + " = " + "current task");
        }
    }

    public void Load(string accountName) {
        if (!File.Exists(Application.dataPath + "/" + accountName + ".txt")) {
            Debug.Log("Error! No account found with that name.");
            return;
        }
        using (StreamReader sr = File.OpenText(Application.dataPath + "/" + accountName + ".txt")) {
            string s = "";
            while ((s = sr.ReadLine()) != null) {
                string[] data = s.Split(new string[] {" = "}, System.StringSplitOptions.None);
                RandoTask t = GameObject.Find(data[0]).GetComponent<RandoTask>();
                //Debug.Log(data[1]);
                if (data[1].Contains("True")) {
                    t.completed = true;
                } else {
                    t.completed = false;
                }
                if (data[1].Contains("current task")) {
                    currentTask = t;
                }
            }
        }
        BuildTaskList();
        DetermineDifficulty();
        Rank.CheckRank();
        taskText.text = currentTask.name;
        textShadow.text = currentTask.name;
        DetermineImageToDisplay();
    }

}
