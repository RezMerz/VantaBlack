using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Block : Unit {

    public Ability ability;
    public Block Pipedto, Pipedfrom;

    // Use this for initialization
    void Start () {
        unitType = UnitType.Block;
        obj = this.gameObject;
        position = gameObject.transform.position;
        codeNumber = Code;
        Code++;
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void CheckPipe()
    {
        if (Pipedfrom == null)
            return;
        if(ability == null && Pipedfrom.ability != null)
        {
            Pomp();
            Pipedfrom.CheckPipe();
        }
    }

    private void Pomp()
    {
        ability = Pipedfrom.ability;
        Pipedfrom.ClearBlock();
    }

    public void undo(Ability ability)
    {
        this.ability = ability;
    }

    public void ClearBlock()
    {
        ability = null;
    }
}


