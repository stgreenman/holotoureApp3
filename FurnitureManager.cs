using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
//using LitJson;
using UnityEngine.UI;

public class FurnitureManager : MonoBehaviour {


	public enum FurnitureType
	{
		Chair, Table, Couch, Bed, Any
	}
	public FurnitureRegistry myRegistry;
	public bool saveOnStart; 
	public static FurnitureManager instance;
    private WWW registryJson;

	// Use this for initialization
    // make an OnStart Function
	void Awake() {
        Debug.Log("Awake");
		instance = this;
		if (saveOnStart) {
		   // SerializeFurniture ();
		}
        InitializeFurniture();
        /*
         * Add this back in later if we want to allow different furniture registries.
         * No current need.
        string url = "https://s3-us-west-2.amazonaws.com/holoture/JSONfiles/registry.json";
        registryJson = new WWW(url);
        StartCoroutine(getRegistry());
        */


    }


    public IEnumerator getRegistry()
    {
       
        Debug.Log("get Furn registry");
        yield return registryJson;
       

        if (registryJson.error == null)
        {
            Debug.Log(registryJson.text);
            myRegistry = JsonUtility.FromJson<FurnitureRegistry>(registryJson.text);
            InitializeFurniture();

        }
        else
        {
            Debug.Log("ERROR: " + registryJson.error);
        }
    }

	void InitializeFurniture(){
        /*
		string registryName = "FurnitureRegistry.txt";
		string RegistryString = File.ReadAllText (registryName);
		myRegistry = JsonUtility.FromJson<FurnitureRegistry> (RegistryString);

        */

        /*       Links that were helpful to Harrison
        
        http://answers.unity3d.com/questions/935800/read-json-file-data-which-saved-in-server.html
        https://forum.unity3d.com/threads/www-is-not-ready-downloading-yet.131989/
        http://lbv.github.io/litjson/docs/quickstart.html#introduction   
        http://answers.unity3d.com/questions/333829/including-a-dll-in-unity.html
        https://forum.unity3d.com/threads/error-cannot-be-an-iterator-block-because-void-is-not-an-iterator-interface-type.144248/

        ***http://answers.unity3d.com/questions/11021/how-can-i-send-and-receive-data-to-and-from-a-url.html
        */

        Debug.Log("Init furniture");



        foreach (FurniturePairing pair in myRegistry.myPairings) {

			Debug.Log ("MYPAIR is" + pair.FurnName);
			pair.FurnObj = (GameObject)Resources.Load (pair.FurnName);
			//Debug.Log ("Sprite type is " +Resources.Load (pair.IconName).GetType());

			Texture2D tempText = (UnityEngine.Texture2D)Resources.Load (pair.IconName);
			pair.myIcon = Sprite.Create( tempText,new Rect(0,0,tempText.width,tempText.height),Vector2.zero);
		}
	}
	/*
	void SerializeFurniture(){
        Debug.Log("serialize");


        string registryName = "FurnitureRegistry.txt";
		string json = JsonUtility.ToJson (myRegistry);
        File.Create(registryName);
        File.SetAttributes(registryName, FileAttributes.Normal);
        File.WriteAllText (registryName, json);
	}
    */
	public FurniturePairing GetFurniture(int num){

		foreach (FurniturePairing mat in myRegistry.myPairings){
			if (mat.id == num) {
				return mat;
			}}
		throw new System.Exception("GameObject Not Found!");

	}

	[Serializable]
	public class FurniturePairing{

		public string FurnName;
		public int id;
		public int DefaultMaterial;
		public string IconName;
		[NonSerialized]
		public GameObject FurnObj;
		[NonSerialized]
		public Sprite myIcon;

		public FurnitureType myType;

	}
    
	[Serializable]
	public class FurnitureRegistry
	{
		public List<FurniturePairing> myPairings;
	}


  
    /*
    private FurnitureRegistry ProcessRegistryJson(string jsonString)
    {
        // convert to json-like object with LitJSON library
        JsonData jsonvalue = JsonMapper.ToObject(jsonString);
        //Debug.Log(jsonvalue);
        FurnitureRegistry newRegistry = new FurnitureRegistry();

        // add each JSON value-field to a new FurniturePairing object, add the object to the Registry pairing list.
        for (int i = 0; i < jsonvalue["myPairings"].Count; i++)
        {
            FurniturePairing newPair = new FurniturePairing();
            newPair.FurnName = jsonvalue["myPairings"][i]["FurnName"].ToString();
            newPair.id = (int) jsonvalue["myPairings"][i]["id"];
            newPair.DefaultMaterial = (int) jsonvalue["myPairings"][i]["DefaultMaterial"];
            newPair.IconName = jsonvalue["myPairings"][i]["IconName"].ToString();

            newRegistry.myPairings.Add(newPair);
        }

        //return instatiated and mostly fleshed out FurniturePairing object.
        return newRegistry;
    }

    */

}
