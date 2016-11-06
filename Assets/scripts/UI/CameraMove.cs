using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
    public float x;
    public float y;
    private static int moving;
    private static bool is_moving;
    public int number;
    public float moveTime = 0.1f;
    private float inverseMoveTime;
    
    void Start()
    {
        inverseMoveTime = 1f / moveTime;
        moving = 0;
    }
    private void _Set_camera()
    {
        Camera.main.transform.position = new Vector3(x, y, -10);
    }

    public void OnCollisionStay2D(Collision2D col){
        if(!is_moving)
        if (moving!=number) {
            is_moving = true;
            moving = number;
            StartCoroutine(Smooth_Move(new Vector3(x, y, -10)));
            
        }     
    }





    protected IEnumerator Smooth_Move(Vector3 end)
    {
        
        float sqrRemainingDistance = (Camera.main.transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            sqrRemainingDistance = (Camera.main.transform.position - end).sqrMagnitude;
            Vector3 newPostion = Vector3.MoveTowards(Camera.main.transform.position, end, inverseMoveTime * Time.deltaTime);


            Camera.main.transform.position =newPostion;


            yield return null;
        }
        is_moving = false;


}







}
