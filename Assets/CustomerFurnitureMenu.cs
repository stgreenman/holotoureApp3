﻿

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using HoloToolkit.Unity.InputModule.Tests;
using HoloToolkit.Unity.InputModule;

public class CustomerFurnitureMenu : MonoBehaviour {

	[SerializeField]
	protected PopupMenu popupMenu = null;

    public CatalogManager Catalog;
	public GameObject buttonTemplate;
	protected int currentPage = 0; 


	public static CustomerFurnitureMenu instance;
	public List<CustomerManager.FurniturePreference> MyPrefs;


	protected List<GameObject> myPanels = new List<GameObject>();
	public GameObject prevButton;
	public GameObject nextButton;

	public int maxNumPerPage;
	protected bool isDragging;
	void Awake()
	{instance = this;
		//LoadPage (0);

	}
   
    public void OpenCatalog()
    {
        Debug.Log("Trying to open catalog");
        Catalog.OpenCatalog(MyPrefs );
    }


	public void nextPage()
	{

		if (currentPage < MyPrefs.Count / maxNumPerPage) {
			currentPage++;
		}
		LoadPage (currentPage);
	}

	public void prevPage()
	{
		if (currentPage > -1) {
			currentPage--;

		}
		LoadPage (currentPage);
	}

	void LoadPage(int num)
	{
		foreach (GameObject obj in myPanels) {
			Destroy (obj);
		}
		myPanels.Clear ();

 
		for (int i =num*maxNumPerPage; i < num*maxNumPerPage+Mathf.Min (MyPrefs.Count - num*maxNumPerPage, maxNumPerPage); i++) {
			GameObject obj = (GameObject)Instantiate (buttonTemplate, this.transform);
			Debug.Log ("ID is " + MyPrefs [i].FurnitureID);
			obj.transform.FindChild ("Title").GetComponent<Text> ().text =MyPrefs[i].FurniturePick.FurnName;

			obj.GetComponent<CustomButton> ().OtherScript = this;
			obj.GetComponent<CustomButton> ().variable = i - num * maxNumPerPage;

			obj.GetComponent<CustomButton> ().functionName = "CreateFurntiture";

			obj.transform.FindChild ("Icon").GetComponent<Image> ().sprite = MyPrefs [i].FurniturePick.myIcon;

			obj.transform.localScale = new Vector3 (1,1,1);
			myPanels.Add (obj);
		}

		SetButtons ();
	}



	protected void SetButtons()
	{
		prevButton.SetActive (true);
		nextButton.SetActive (true);
		if (currentPage == 0) {
			prevButton.SetActive (false);
		}
		if (currentPage == (MyPrefs.Count -1) / 7) {
			nextButton.SetActive (false);
		}
	}

    public void addPrefrence(CustomerManager.FurniturePreference preference)
    {
        MyPrefs.Add(preference);
        LoadPage(currentPage);

    }

	public void RemovePrefrence(FurnitureManager.FurniturePairing preference)
	{
		for (int i = 0; i < MyPrefs.Count; i++) {
			if (MyPrefs [i].FurniturePick == preference) {
				MyPrefs.RemoveAt (i);
				break;
			}
		}

		Catalog.ReturnPrefrence (preference);

		LoadPage(currentPage);

	}

    public void loadPreferences(CustomerManager.CustomerInfo info)
	{
		MyPrefs = info.furnPrefs;
		LoadPage (0);
	}

	void openCatalog(){

	}


	public void CreateFurntiture(object Ind)
	{
		int Index = (int)Ind;
		Vector3 spawnLocation = GameObject.FindObjectOfType<Camera> ().transform.forward * 5 + GameObject.FindObjectOfType<Camera> ().transform.position;

		Debug.Log ("Creating new guy");
		GameObject newFurniture = (GameObject)Instantiate (MyPrefs [currentPage * maxNumPerPage + Index].FurniturePick.FurnObj, spawnLocation, Quaternion.identity);
		newFurniture.transform.LookAt (GameObject.FindObjectOfType<Camera> ().transform.position);

		foreach(MeshRenderer mesh in newFurniture.GetComponentsInChildren<MeshRenderer>())
			{		mesh.gameObject.AddComponent<MeshCollider> ();

			}
		TestButton testb = newFurniture.GetComponent<TestButton> ();
		TogglePopupMenu Temp = newFurniture.GetComponent<TogglePopupMenu> ();
		Temp.set (testb,popupMenu, MyPrefs[currentPage * maxNumPerPage + Index].FurniturePick, true);


		isDragging = true;
		closeMenu ();

		InputEventData data = new InputEventData (UnityEngine.EventSystems.EventSystem.current);
		data.Initialize (GameObject.FindObjectOfType<RawInteractionSourcesInput> (), 2);
		newFurniture.GetComponent<HandDraggable> ().OnInputDown (data);

		//newFurniture.transform.position = spawnLocation;
	}

	public void openMenu()
	{		gameObject.transform.parent.gameObject.SetActive (true);
		Vector3 spawnLocation = GameObject.FindObjectOfType<Camera> ().transform.forward * 7 + GameObject.FindObjectOfType<Camera> ().transform.position;

		gameObject.transform.parent.position = spawnLocation;
		gameObject.transform.parent.LookAt(gameObject.transform.parent.position*2 -  GameObject.FindObjectOfType<Camera> ().transform.position);
		gameObject.transform.parent.Translate (Vector3.right , Space.Self);
		isDragging = false;
	}

	public void closeMenu()
	{		gameObject.transform.parent.gameObject.SetActive (false);}

	public void stopDragging()
	{
		if (isDragging) {
			openMenu ();
		}

	}

}



