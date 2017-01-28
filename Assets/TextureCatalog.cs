using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCatalog : TextureMenu {


	
	public void turnOn (GameObject currentSelection){

		if (!this.gameObject.activeSelf) {
			this.transform.parent.gameObject.SetActive (true);
			currentlySelected = currentSelection;
			LoadPage (0);
		} else {
			close ();}
	}

	public void close()
	{
		this.transform.parent.gameObject.SetActive (false);
	}

	public void Initialize()
	{
		MyMaterials = GameObject.FindObjectOfType<MaterialManager> ().myRegistry.myPairings;
		LoadPage (0);

	}
}
