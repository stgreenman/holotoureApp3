﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureMenu : MonoBehaviour {
	public GameObject buttonTemplate;
	private int currentPage = 0; 

	public List<MaterialManager.MaterialPairing> MyMaterials;

	public static TextureMenu instance;
	public TextureCatalog myCatalog;

	public GameObject currentlySelected;
	List<GameObject> myPanels = new List<GameObject>();
	public GameObject prevButton;
	public GameObject nextButton;

	public int maxNumPerPage;

	void Awake()
	{
		instance = this;
	}

	public void nextPage()
	{
		Debug.Log ("I am getting pressed");
		if (currentPage < MyMaterials.Count / 7) {
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

protected	void LoadPage(int num)
	{
		foreach (GameObject obj in myPanels) {
			Destroy (obj);
		}
		myPanels.Clear ();


		for (int i =num*maxNumPerPage; i < num*maxNumPerPage+Mathf.Min ( MyMaterials.Count - num*maxNumPerPage, maxNumPerPage); i++) {
			GameObject obj = (GameObject)Instantiate (buttonTemplate, this.transform);
			obj.transform.FindChild ("Title").GetComponent<Text> ().text = MyMaterials[i].MatName;

			obj.transform.FindChild ("Icon").GetComponent<MeshRenderer> ().materials= new Material[1]{ MyMaterials [i].mat};
			obj.GetComponent<CustomButton> ().OtherScript = this;
			obj.GetComponent<CustomButton> ().variable = MyMaterials [i].mat;

			obj.GetComponent<CustomButton> ().functionName = "setObjectMaterial";
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
		if (currentPage == ( MyMaterials.Count -1) / 7) {
			nextButton.SetActive (false);
		}
	}


	public void setObjectMaterial(Object mat)
	{Debug.Log ("SetObjectMat is being called");

		Material mater = (Material)mat;
		foreach (MeshRenderer rend in currentlySelected.GetComponents<MeshRenderer>()) {
			rend.material = mater;
		}
		foreach (MeshRenderer rend in currentlySelected.GetComponentsInChildren<MeshRenderer>()) {
			rend.material = mater;
		}
	}

	public void loadPreferences(CustomerManager.FurniturePreference info)
	{
		MyMaterials = info.materialPicks;
		myCatalog.Initialize ();
		LoadPage (0);
	}

	void openCatalog(){
		myCatalog.turnOn ( currentlySelected);
	}





}
