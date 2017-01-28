using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudManager : MonoBehaviour {


	public GameObject FurnitureMenu;
	public GameObject TextureMenu;


	public void ToggleTextureMenu()
	{
		TextureMenu.SetActive (!TextureMenu.activeSelf);
	}

	public void ToggleFurnitureMenu()
	{
		FurnitureMenu.SetActive (!FurnitureMenu.activeSelf);
	}

}
