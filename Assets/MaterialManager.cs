using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Resources;


public class MaterialManager : MonoBehaviour {


	public MaterialRegistry myRegistry;
	public bool saveOnStart; 
	public static MaterialManager instance;
	// Use this for initialization
	void Awake() {

		instance = this;
		if (saveOnStart) {
			//SerializeMaterials ();
		}

		InitializeMaterials ();

	}
	



	void InitializeMaterials(){
		/*
		string registryName = "MaterialRegistry.txt";
		string RegistryString = File.ReadAllText (registryName);
		myRegistry = JsonUtility.FromJson<MaterialRegistry> (RegistryString);
*/

		foreach (MaterialPairing pair in myRegistry.myPairings) {
			pair.mat = (Material)Resources.Load (pair.MatName);
		}
	}
	/*
	void SerializeMaterials(){

		string registryName = "MaterialRegistry.txt";
		string json = JsonUtility.ToJson (myRegistry);
        File.Create(registryName);
        File.SetAttributes(registryName, FileAttributes.Normal);
		File.WriteAllText (registryName, json);


	}*/
		
	public MaterialPairing GetMaterial(int num){

		foreach (MaterialPairing mat in myRegistry.myPairings){
			if (mat.id == num) {
				return mat;
			}}
			throw new System.Exception("Material Not Found!");

	}

	[Serializable]
	public class MaterialPairing{

		public string MatName;
		public int id;

		[NonSerialized]
		public Material mat;

		public MaterialPairing(Material _mat, int _id){
			mat = _mat;
			id = _id;
		}

	}

	[Serializable]
	public class MaterialRegistry
	{
		public List<MaterialPairing> myPairings;
	}
}
