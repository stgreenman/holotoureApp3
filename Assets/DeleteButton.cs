using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HoloToolkit.Unity.InputModule.Tests
{
public class DeleteButton : MonoBehaviour {



	public TextMesh myText;
	public GameObject currentObject;

	
	
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
			Destroy (currentObject);
			Menu.Instance.SetMenu (null);

	}
}
}

