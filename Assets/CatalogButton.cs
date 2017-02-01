using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogButton : CustomButton {

	public FurnitureManager.FurnitureType myType;

	void Start()
	{
		variable = myType;
	}

}
