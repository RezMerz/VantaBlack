using UnityEngine;
using System.Collections;

public class MapController : MonoBehaviour {
    private bool is_showing;
    private GameObject panel;
    private GameObject[] rooms;
    void Awake()
    {
        panel = gameObject.transform.GetChild(0).gameObject;
        panel.SetActive(false);
        is_showing = false;
        rooms = new GameObject[panel.transform.childCount];
        int i = 0;
        foreach(Transform child in panel.transform)
        {
            rooms[i] = child.gameObject;
            i++;
        }

    }
    public void _show_map()
    {
        panel.SetActive(true);
    }

    public void _hide_map()
    {
        panel.SetActive(false);
    }

    public void _click()
    {
        if (is_showing)
            _hide_map();
        else
            _show_map();
        is_showing = !is_showing;
    }
    public void _update_rooms(Room[] check_rooms)
    {
        int i = 0;
        foreach(GameObject room in rooms)
        {
            if (check_rooms[i].isVisible)
                room.SetActive(true);
            else
                room.SetActive(false);
            if (Database.database.player.GetComponent<Player>().current_scene == room.name)
                room.GetComponent<SpriteRenderer>().color = Color.red;
            i++;
        }
    }
}
