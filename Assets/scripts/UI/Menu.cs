using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour {

    public string first_scene;
	public void Play()
    {
        Player p = SaveLoad._LoadPlayer();
        string scene;
        if (p != null)
            scene = p.current_scene;
        else
            scene = first_scene;
        SceneManager.LoadScene(scene);
    }
}
