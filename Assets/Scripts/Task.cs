using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        EventCenter.Instance.AddEventListener<Monster>("MonsterDead",TaskAction);
    }

    // Update is called once per frame
    private void TaskAction(object info)
    {
        print("Task: 怪物死亡" + (info as Monster).name);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<Monster>("MonsterDead", TaskAction);
    }
}
