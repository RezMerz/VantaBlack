using UnityEngine;
using System.Collections;

public class ExternalDoor : Door {

    public string sceneName;
    // Use this for initialization
    void Start()
    {
        unitType = UnitType.Door;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        try
        {
            sprite = obj.GetComponent<SpriteRenderer>().sprite;
        }
        catch
        {
            sprite = null;
        }
        movable = false;
    }

    // Update is called once per frame
    void Update () {
	
	}

    public override void Next()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public override void OpenDoor()
    {
        open = true;
        obj.GetComponent<SpriteRenderer>().sprite = null;
    }

    public override void CloseDoor()
    {
        open = false;
        obj.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public override void OpenClose()
    {
        if (open)
            CloseDoor();
        else
            OpenDoor();
    }

    public override bool CanMove(UnitType unittype)
    {

        return false;
    }
}
