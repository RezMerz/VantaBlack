using UnityEngine;
using System.Collections;
using System.Threading;

public class AnimationControler {
    
    public AnimationControler()
    {

    }

    public void MoveAnimation()
    {
        Wall.print("begining");
        for(int i = 0; i < 100; i++)
        {
            Wall.print(i);
        }
        //thread.Start();
    }
}
