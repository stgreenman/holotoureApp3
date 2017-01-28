using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyManager : MonoBehaviour {

    public GameObject texturesPanel;
    public GameObject optionsPanel;
    public GameObject furniturePanel;

    // Use this for initialization
    void Start () {

        


       // texturesPanel = GameObject.Find("Textures Panel");

        if (texturesPanel != null)
        {
            texturesPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Error: Tried to find TexturesPanel. It is null");
        }
        //  optionsPanel = GameObject.Find("Options Panel");
        //  furniturePanel = GameObject.Find("Furniture Panel");

        //   optionsPanel.gameObject.SetActive(false);
        //    furniturePanel.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
