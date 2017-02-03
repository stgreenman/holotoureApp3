using System;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

using UnityEngine.EventSystems;


public class Menu : MonoBehaviour
{

    // .NET guarantees thread safety for static initialization

    public GameObject currentMenu;
    private GameObject attachedObj { get; set; }

    public static Menu Instance;
       
   

    private void Awake()
    {
        Instance = this;
    }

    public bool SetMenu(GameObject newMenu)
    {

		bool sameMenu = (newMenu == currentMenu);
		if (currentMenu != null)// && newMenu !\= currentMenu)
        {
			//((Behaviour)currentMenu.GetComponentInChildren("Halo")).enabled = false;

            currentMenu.SendMessage("OnInputClicked", new InputEventData(null), SendMessageOptions.DontRequireReceiver);
        }
		if (sameMenu) {
			return false;}
        currentMenu = newMenu;
		//((Behaviour)currentMenu.GetComponentInChildren("Halo")).enabled = true;
		return true;
    }

  
    public bool hasCurrentMenu()
    {
        return currentMenu != null;
    }
}




