using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        EventCenter.Instance.AddEventListener<Monster>("MonsterDead", PlayerAction);
    }

    // Update is called once per frame
    private void PlayerAction(Monster info)
    {
        print("Player: 怪物死亡" + (info as Monster).name);
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener<Monster>("MonsterDead", PlayerAction);
    }
}
