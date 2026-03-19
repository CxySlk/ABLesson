using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string name = "我是怪物";

    // Start is called before the first frame update
    private void Start()
    {
        Invoke("Dead", 3);
    }

    private void Dead()
    {
        print("Monster is dead");
        EventCenter.Instance.EventTrigger("MonsterDead", this);
    }
}
