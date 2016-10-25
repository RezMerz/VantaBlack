using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager manager = null;

	
    void Awake()
    {
        _get_manager_();

        //ba arze salam o khaste nabashid khedmat doost o ham daneshgahie aziz, Reza Mirzaee
        //omid ast hal shoma khob bashad.. va dar kar o zendegi movafagh bashid...
        //be fekre doostanet ham bash
        //kamtar dar in laptop be sar bebar...
        //kholase hal ma khob ast joyaye ahvale shoma
        //etod yashar gom shode
        //commentaye mano pak nakon
        //toro khoda
        //ina daran khar bazi dar miaran
        //etod yahsar hanoz peyda nashode... peyda shod to jibesh bood
        //khodaHafez 
        //emza 5hajan
    }

    void _get_manager_()
    {
        if(manager!=null && manager!=this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            mangaer = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
