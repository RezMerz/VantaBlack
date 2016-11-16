using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class CheckPoint : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            SaveLoad._SavePlayer(Database.database.player.GetComponent<Player>());
            SaveLoad._SaveScene(Database.database.units,SceneManager.GetActiveScene().name);
        }
    }
}
