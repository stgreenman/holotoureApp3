using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HoloToolkit.Unity.InputModule.Tests
{
public class SecCustomButton: MonoBehaviour {


		public MonoBehaviour OtherScript;
		public string functionName;
		public object variable;
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

			OtherScript.SendMessage (functionName, variable, SendMessageOptions.DontRequireReceiver);
			GameObject.Find ("MenuOpener").SendMessage ("turnOff", SendMessageOptions.DontRequireReceiver);
		button.Selected = false;


	}
}
}

