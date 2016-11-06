using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class InMenu : MonoBehaviour {
    private int button;
	// Use this for initialization
	void Start () {
        button = 0;
	}

    public void click()
    {
        if (gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }


	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {     
                
                if (gameObject.transform.GetChild(0).gameObject.activeSelf)
                {
                    if (button == 0)
                    {
                        button = 1;
                        ColorBlock cb = GameObject.Find("inMenuPanel").transform.GetChild(1).GetComponent<Button>().colors;
                         cb.normalColor = Color.red;
                         GameObject.Find("inMenuPanel").transform.GetChild(1).GetComponent<Button>().colors = cb;
                         cb.normalColor = Color.white;
                        GameObject.Find("inMenuPanel").transform.GetChild(0).GetComponent<Button>().colors = cb;
                }  
                }        
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                if (button == 1)
                {
                    button = 0;
                    ColorBlock cb = GameObject.Find("inMenuPanel").transform.GetChild(0).GetComponent<Button>().colors;
                    cb.normalColor = Color.red;
                    GameObject.Find("inMenuPanel").transform.GetChild(0).GetComponent<Button>().colors = cb;
                    cb.normalColor = Color.white;
                    GameObject.Find("inMenuPanel").transform.GetChild(1).GetComponent<Button>().colors = cb;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                if (button == 0)
                    Controls();
                else if (button == 1)
                    Quit();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                if (gameObject.transform.GetChild(1).gameObject.activeSelf)
                {
                    Back_to_Menu();
                }
                else
                    click();
            }
            else 
                click();
        }

	}

    public void Quit()
    {
        Application.Quit();
    }

    public void Controls()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }
    public void Action_End()
    {

    }
    public void Back_to_Menu()
    {
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }
}
