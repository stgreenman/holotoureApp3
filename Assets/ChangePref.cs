using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HoloToolkit.Unity.InputModule.Tests
{
public class ChangePref : MonoBehaviour {

		public SpriteRenderer myRenderer;
		public Sprite Plus;
		public Sprite minus;

	public FurnitureManager.FurniturePairing currentFurniture;
	public	CustomerFurnitureMenu customerMenu;
	public CatalogManager myCatalog;
	public TextMesh myText;
	bool inFolder;

	public void setInFolder(bool inOut)
	{inFolder = inOut;
		if (inOut) {
				myRenderer.sprite = minus;
			myText.text = "Remove From Folder";
		} else {
				myRenderer.sprite = Plus;
			myText.text = "Add To Folder";
		}


	}
		[SerializeField]
		private TestButton button = null;


	

		private void OnEnable()
		{
			button.Activated += OnButtonPressed;
		}

		private void OnDisable()
		{
			button.Activated -= OnButtonPressed;
		}

		public void setFurniture(FurnitureManager.FurniturePairing currentFur)
		{
			currentFurniture = currentFur;
			//Debug.Log ("Setting current furniture to " + currentFur.FurnName);
		}

		private void OnButtonPressed(TestButton source)
		{
			if (inFolder) {
				customerMenu.RemovePrefrence(currentFurniture);
			} else {
				myCatalog.newCustPref(currentFurniture);
			}
			button.Selected = false;
	
			setInFolder (!inFolder);

		}
	}
}

