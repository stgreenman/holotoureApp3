using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class FurnitureManager : MonoBehaviour {


	public enum FurnitureType
	{
		Chair, Table, Couch, Bed, Any
	}
	public FurnitureRegistry myRegistry;
	public bool saveOnStart; 
	public static FurnitureManager instance;

	// Use this for initialization
	void Awake() {
		instance = this;
		if (saveOnStart) {
		//	SerializeFurniture ();
		}

		InitializeFurniture ();

	}



	void InitializeFurniture(){
		/*
		string registryName = "FurnitureRegistry.txt";
		string RegistryString = File.ReadAllText (registryName);
		myRegistry = JsonUtility.FromJson<FurnitureRegistry> (RegistryString);
*/

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
}
