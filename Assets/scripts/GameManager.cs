using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager manager = null;
    public MapMenu map;
	
    void Start()
    {
        if (manager != null && manager != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            
            manager = this;
            set_map();
            
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void set_map()
    {
        map = SaveLoad.LoadMap();
        if (map == null)
        {
            map = new MapMenu(10);
        }
        GameObject.Find("Map").GetComponent<MapController>()._update_rooms(map.rooms);
    }


}
