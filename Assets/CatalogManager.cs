using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule.Tests;
using HoloToolkit.Unity.InputModule;


public class CatalogManager : CustomerFurnitureMenu {


    FurnitureManager FurnManager;

    List<CustomerManager.FurniturePreference> CurrentInfo;

    List<FurnitureManager.FurniturePairing> NotAddedFurniture;
	FurnitureManager.FurnitureType currentType = FurnitureManager.FurnitureType.Any;
	public CustomerFurnitureMenu mainCustomerMenu;
    private void Awake()
    {
        FurnManager = GameObject.FindObjectOfType<FurnitureManager>();

    }

    public void OpenCatalog(List<CustomerManager.FurniturePreference> info)
    {
        FurnManager = GameObject.FindObjectOfType<FurnitureManager>();
     
        this.gameObject.transform.parent.gameObject.SetActive(true);
        CurrentInfo = info;

        NotAddedFurniture = new List<FurnitureManager.FurniturePairing>();
        foreach (FurnitureManager.FurniturePairing furn in FurnManager.myRegistry.myPairings)
        {
            bool alreadyUsed = false;
            foreach (CustomerManager.FurniturePreference customerPref in CurrentInfo)
            {
                if (customerPref.FurnitureID == furn.id)
                {
                    alreadyUsed = true;
                    break; }
            }
            if (!alreadyUsed)
            {
                NotAddedFurniture.Add(furn);
            }
        }
		LoadFurnitureType (currentType);
        LoadPage(0);

    }

	void LoadFurnitureType(object furnT)
	{FurnitureManager.FurnitureType furnType = (FurnitureManager.FurnitureType)furnT;
		FurnManager = GameObject.FindObjectOfType<FurnitureManager>();

		this.gameObject.transform.parent.gameObject.SetActive(true);

		NotAddedFurniture = new List<FurnitureManager.FurniturePairing>();
		foreach (FurnitureManager.FurniturePairing furn in FurnManager.myRegistry.myPairings)
		{
			bool alreadyUsed = false;
			Debug.Log ("Current is " + CurrentInfo);
			foreach (CustomerManager.FurniturePreference customerPref in CurrentInfo)
			{
				if (customerPref.FurnitureID == furn.id)
				{
					alreadyUsed = true;
					break; }
			}
			if (!alreadyUsed && (furnType == furn.myType || furnType == FurnitureManager.FurnitureType.Any))
			{
				NotAddedFurniture.Add(furn);
			}
		}

		LoadPage(0);
	
	
	}


   void LoadPage(int num)
    {
        foreach (GameObject obj in myPanels)
        {
            Destroy(obj);
        }
        myPanels.Clear();


        for (int i = num * maxNumPerPage; i < num * maxNumPerPage + Mathf.Min(NotAddedFurniture.Count - num * maxNumPerPage, maxNumPerPage); i++)
        {
            GameObject obj = (GameObject)Instantiate(buttonTemplate, this.transform);
            obj.transform.FindChild("Title").GetComponent<Text>().text = NotAddedFurniture[i].FurnName;

            obj.GetComponent<CustomButton>().OtherScript = this;
            obj.GetComponent<CustomButton>().variable = i - num * maxNumPerPage;

			obj.GetComponent<CustomButton>().functionName = "CreateFurntiture";

            obj.transform.FindChild("Icon").GetComponent<Image>().sprite = NotAddedFurniture[i].myIcon;

            obj.transform.localScale = new Vector3(1, 1, 1);
			Vector3 tempPos = obj.transform.localPosition;
			tempPos.z = -2f;
			obj.transform.localPosition = tempPos;
            myPanels.Add(obj);
        }

        SetButtons();
    }


	public void ReturnPrefrence(FurnitureManager.FurniturePairing pairing)
	{
		if (NotAddedFurniture != null) {
			//NotAddedFurniture.Add (pairing);
			LoadFurnitureType (currentType);
			LoadPage (currentPage);
		}
	}

	public void newCustPref(FurnitureManager.FurniturePairing pairing)
	{
		CustomerManager.FurniturePreference newPref = new CustomerManager.FurniturePreference(pairing);
		mainCustomerMenu.addPrefrence (newPref);


		if (this.gameObject.transform.parent.gameObject.activeSelf) {
			LoadFurnitureType (currentType);
			LoadPage (currentPage);
		}
	}


    public void AddToPrefs(object Ind)
    {
        int Index = (int)Ind;
        CustomerManager.FurniturePreference newPref = new CustomerManager.FurniturePreference();
        newPref.FurniturePick = NotAddedFurniture[currentPage * maxNumPerPage + Index];
        newPref.FurnitureID = newPref.FurniturePick.id;
        newPref.MaterialIDs = new List<int>();
        newPref.MaterialIDs.Add(newPref.FurniturePick.DefaultMaterial);

        GameObject.FindObjectOfType<CustomerFurnitureMenu>().addPrefrence(newPref);

        NotAddedFurniture.RemoveAt(currentPage * maxNumPerPage + Index);

        if (currentPage == NotAddedFurniture.Count / maxNumPerPage)
        {
            currentPage = 0;
        }
		if (this.gameObject.transform.parent.gameObject.activeSelf) {
			LoadFurnitureType (currentType);
			LoadPage (currentPage);
		}
    // MyPrefs[currentPage * maxNumPerPage + Index].FurniturePick.FurnObj, );
    }



	public void CreateFurntiture(object Ind)
	{
		int Index = (int)Ind;
		Vector3 spawnLocation = GameObject.FindObjectOfType<Camera> ().transform.forward * 5 + GameObject.FindObjectOfType<Camera> ().transform.position;

		Debug.Log ("Creating new guy");
		GameObject newFurniture = (GameObject)Instantiate (NotAddedFurniture [currentPage * maxNumPerPage + Index].FurnObj, spawnLocation, Quaternion.identity);

		Vector3 CameraPos = GameObject.FindObjectOfType<Camera> ().transform.position;
		CameraPos.y = newFurniture.transform.position.y;
		newFurniture.transform.LookAt (CameraPos);

		foreach(MeshRenderer mesh in newFurniture.GetComponentsInChildren<MeshRenderer>())
		{		mesh.gameObject.AddComponent<MeshCollider> ();

		}
		TestButton testb = newFurniture.GetComponent<TestButton> ();
		TogglePopupMenu Temp = newFurniture.GetComponent<TogglePopupMenu> ();
		Temp.set (testb,popupMenu, NotAddedFurniture[currentPage * maxNumPerPage + Index], false);


		isDragging = true;
		closeMenu ();

		InputEventData data = new InputEventData (UnityEngine.EventSystems.EventSystem.current);
		data.Initialize (GameObject.FindObjectOfType<RawInteractionSourcesInput> (), 2);
		newFurniture.GetComponent<HandDraggable> ().OnInputDown (data);

		//newFurniture.transform.position = spawnLocation;
	}

}
