using UnityEngine;
using System.Collections;

public class SwitchControler : Switch
{

    public GameObject otherSwitch;
    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override bool Run()
    {
        MovingSwitch[] t1 = otherSwitch.GetComponents<MovingSwitch>();
        DoorSwitch[] t2 = otherSwitch.GetComponents<DoorSwitch>();
        for (int i = 0; i < t1.Length; i++)
        {
            t1[i].Run();
        }
        for (int i = 0; i < t2.Length; i++)
        {
            t2[i].Run();
        }
        return true;
    }
}