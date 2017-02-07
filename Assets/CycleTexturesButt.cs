using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HoloToolkit.Unity.InputModule.Tests
{
	
public class CycleTexturesButt  : MonoBehaviour {


		public TextureMenu currentTextureMenu;
		public TextureMenu currentCatalog;

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


	private void OnButtonPressed(TestButton source)
	{
			if (currentCatalog.transform.parent.gameObject.activeSelf) {
				currentCatalog.CycleMaterials ();
			} else {
				currentTextureMenu.CycleMaterials ();
			}
	}

}

}