using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CustomerManager : MonoBehaviour {


	public CustomerRegistry myRegistry;
	public bool saveOnStart; 

	FurnitureManager FurnManager;
	MaterialManager matManager;

	// Use this for initialization
	void Start() {
		FurnManager = GameObject.FindObjectOfType<FurnitureManager> ();
		matManager = GameObject.FindObjectOfType<MaterialManager> ();


		if (saveOnStart) {
			//SerializeCustomers ();
		}

		InitializeCustomers ();

	}



	void InitializeCustomers(){
		/*
		string registryName = "CustomerRegistry.txt";
		string RegistryString = File.ReadAllText (registryName);
		myRegistry = JsonUtility.FromJson<CustomerRegistry> (RegistryString);
		*/

		foreach (CustomerInfo pair in myRegistry.customerList) {

			foreach (FurniturePreference pref in pair.furnPrefs) {
				pref.FurniturePick = FurnManager.GetFurniture (pref.FurnitureID);

				foreach (int id in pref.MaterialIDs) {
					pref.materialPicks.Add (matManager.GetMaterial (id));
				}
			}
		}

		//REMOVE THIS LATER
		CustomerFurnitureMenu.instance.loadPreferences(myRegistry.customerList [0]);

		//GameObject.FindObjectOfType<CustomerFurnitureMenu> ().CreateFurntiture (0);
	}
	/*
	void SerializeCustomers(){

		string registryName = "CustomerRegistry.txt";
		string json = JsonUtility.ToJson (myRegistry);
        File.Create(registryName);
        File.SetAttributes(registryName, FileAttributes.Normal);
        File.WriteAllText (registryName, json);
	}*/

	public CustomerInfo GetMaterial(int num){

		foreach (CustomerInfo mat in myRegistry.customerList){
			if (mat.CustomerID == num) {
				return mat;
			}}
		throw new System.Exception("Customer Not Found!");

	}



	[Serializable]
	public class CustomerRegistry
	{
		public List<CustomerInfo> customerList;
	}

	[Serializable]
	public class CustomerInfo
	{
		public string CustomerName;
		public int CustomerID;
		public List<FurniturePreference> furnPrefs;
	}


	[Serializable]
	public class FurniturePreference
	{

		public int FurnitureID;
		public List<int> MaterialIDs;
		public FurniturePreference(FurnitureManager.FurniturePairing pair)
		{
			FurnitureID = pair.id;
			MaterialIDs = new List<int>();
			MaterialIDs.Add(pair.DefaultMaterial);
			FurniturePick = FurnitureManager.instance.GetFurniture (pair.id);

			foreach (int id in MaterialIDs) {
				materialPicks.Add (MaterialManager.instance.GetMaterial (id));
			}
		}	public FurniturePreference()
	{
	}

		[NonSerialized]
		public FurnitureManager.FurniturePairing FurniturePick;
		[NonSerialized]
		public List<MaterialManager.MaterialPairing> materialPicks = new List<MaterialManager.MaterialPairing>();

	}


}
