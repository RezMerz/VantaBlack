using UnityEngine;
using System.Collections;

public class RoomDetect : MonoBehaviour {
    public string room;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Database.database.player.GetComponent<Player>().current_room = room;
            GameObject.Find("Map").GetComponent<MapController>()._update_current_room();
            Update_Shadows();
        }
    }
    private void Update_Shadows()
    {
        GameObject shadows = GameObject.Find("shadows");
        foreach(Transform child in shadows.transform)
        {
            if (child.gameObject.name != room)
                child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            else
                child.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
