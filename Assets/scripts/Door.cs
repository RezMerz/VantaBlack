﻿using UnityEngine;
using System.Collections;

public class Door : Unit {

    public string sceneName;
    public bool open;
    public Direction direction;
    Sprite sprite;
	// Use this for initialization
	void Start () {
        unitType = UnitType.Door;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
        try {
            sprite = obj.GetComponent<SpriteRenderer>().sprite;
        }
        catch
        {
            sprite = null;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NextScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void OpenDoor()
    {
        open = true;
        obj.GetComponent<SpriteRenderer>().sprite = null;
    }

    public void CloseDoor()
    {
        open = false;
        obj.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void OpenClose()
    {
        if (open)
            CloseDoor();
        else
            OpenDoor();
    }
}
