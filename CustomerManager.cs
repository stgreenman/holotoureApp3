using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
//using LitJson;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour {


	public CustomerRegistry myRegistry;
	public bool saveOnStart;
    private WWW CustomerJson; 

	FurnitureManager FurnManager;
	MaterialManager matManager;

	// Use this for initialization
	void Start() {
		FurnManager = GameObject.FindObjectOfType<FurnitureManager> ();
		matManager = GameObject.FindObjectOfType<MaterialManager> ();

        Debug.Log("Initialize customers");
        string url = "https://s3-us-west-2.amazonaws.com/holoture/JSONfiles/customerList.json";
        CustomerJson = new WWW(url);

        /*if (saveOnStart) {
			//SerializeCustomers ();
		}*/

        StartCoroutine(getCustRegistry());
		//InitializeCustomers();

	}

    public IEnumerator getCustRegistry()
    {
        Debug.Log("get Cust Registry");
        yield return CustomerJson;

       

        if (CustomerJson.error == null)
        {
            //Debug.Log(CustomerJson.text);
            myRegistry = JsonUtility.FromJson<CustomerRegistry>(CustomerJson.text);
            //myRegistry = ProcessRegistryJson(CustomerJson.text);
            InitializeCustomers();

        }
        else
        {
            Debug.Log("ERROR: " + CustomerJson.error);
        }
    }


	void InitializeCustomers(){
        /*
		string registryName = "CustomerRegistry.txt";
		string RegistryString = File.ReadAllText (registryName);
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
	
/*	IEnumerator SerializeCustomers(){
        /*
        string registryName = "CustomerRegistry.txt";
        string json = JsonUtility.ToJson(myRegistry);
        File.Create(registryName);
        File.SetAttributes(registryName, FileAttributes.Normal);
        File.WriteAllText(registryName, json);
       


        Debug.Log("INitialize customers");
        string url = "https://s3-us-west-2.amazonaws.com/holoture/JSONfiles/customerList.json";
        WWW www = new WWW(url);
        yield return www;
        Debug.Log(www.text);

        if (www.error == null)
        {

            //myRegistry = ProcessRegistryJson(www.text);
            // string registryName = File.ReadAllText(www.text);
            string registryName = "CustomerRegistry.txt";
            string json = JsonUtility.ToJson(myRegistry);
            File.Create(registryName);
            File.SetAttributes(registryName, FileAttributes.Normal);
            File.WriteAllText(registryName, json);
            //myRegistry = JsonUtility.FromJson<CustomerRegistry>(RegistryString);
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }

      
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

/*
   private CustomerRegistry ProcessRegistryJson(string jsonString)
    {
        // convert to json-like object with LitJSON library
        JsonData jsonvalue = JsonMapper.ToObject(jsonString);
        //Debug.Log(jsonvalue);
        CustomerRegistry newRegistry = new CustomerRegistry();

        // get each customer
        for (int i = 0; i < jsonvalue["customerList"].Count; i++)
        {
            CustomerInfo newPref = new CustomerInfo();
            newPref.CustomerName = jsonvalue["customerList"][i]["CustomerName"].ToString();
            Debug.Log(newPref.CustomerName);
            newPref.CustomerID = (int)jsonvalue["customerList"][i]["CustomerID"];
            Debug.Log(newPref.CustomerID);

            //get each Furniture Preferences for the customer
            for ( int j = 0; j < jsonvalue["customerList"][i]["furnPrefs"].Count; j++){
                FurniturePreference myPref = new FurniturePreference();
                myPref.FurnitureID = (int)jsonvalue["customerList"][i]["furnPrefs"]["FurnitureID"];
                Debug.Log(myPref.FurnitureID);

                // get each material for each preference of the customer
                // This doesn't work because the data member being accessed is
                for(int k = 0; k < jsonvalue["customerList"][i]["furnPrefs"]["MaterialIDs"].Count; k++)
                {
                    Debug.Log((string)jsonvalue["customerList"][i]["furnPrefs"]["MaterialIDs"][k]);
                    myPref.MaterialIDs.Add((int)jsonvalue["customerList"][i]["furnPrefs"]["MaterialIDs"][k]);
                }

            }
           newRegistry.customerList.Add(newPref);
        }

        //return instatiated and fairly fleshed out CustomerRegistry object.
        return newRegistry;
    }

    */


}
