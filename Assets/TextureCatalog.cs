using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureCatalog : TextureMenu {

	public TextureMenu mainMenu;

	
	public void turnOn (GameObject currentSelection){

		//if (!this.gameObject.activeSelf) {
			this.transform.parent.gameObject.SetActive (true);
			currentlySelected = currentSelection;
			LoadPage (0);
		//} else {
			//close ();}
	}


	public void AlterNate()
	{
		mainMenu.AlterNate ();
	
	}
	public void Alternate(GameObject currentSelection)
	{

		this.transform.parent.gameObject.SetActive (!this.transform.parent.gameObject.activeSelf);
		currentlySelected = currentSelection;
		LoadPage (0);
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
