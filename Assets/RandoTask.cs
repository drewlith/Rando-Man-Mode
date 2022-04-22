using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandoTask : MonoBehaviour
{
    // Type 1: Skill
    // Type 2: Quest
    // Type 3: Obtain Collection Log Item
    // Type 4: Complete Treasure Trail
    // Type 5: Complete Achievement Diary Task
    // Type 6: Complete Combat Task
    // Type 7: Complete Arbitrary Hiscore Task

    public int type;
    public int difficulty;
    public bool repeatable;
    public bool completed;
    public RandoTask[] requirements;

    void Awake() {
        Randomizer rando = FindObjectOfType<Randomizer>();
        rando.allTasks.Add(this);
    }

    public void CompleteTask() {
        completed = true;
    }
}
