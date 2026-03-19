using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPush : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Invoke("Push", 1);
    }

    // Update is called once per frame
    private void Push()
    {
        PoolMgr.Instance.PushObj(this.name, this.gameObject);
    }
}
