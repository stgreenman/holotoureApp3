using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HoloToolkit.Unity.InputModule.Tests
{ 

	public class MenuOpener : MonoBehaviour, IInputHandler {


		public static MenuOpener instance;
		[Tooltip("Time to hold down in empty space before the furniture menu appears.")]
		public float HoldDownTime;
		Coroutine isButtonDown;
		bool inputDown;
	// Use this for initialization
	void Start () {
			instance = this;
			GameObject.FindObjectOfType<InputManager> ().PushFallbackInputHandler(this.gameObject);
	}


		public void OnInputUp(InputEventData eventData){
	
			inputDown = false;


		}
		public 	void OnInputDown(InputEventData eventData){
		//	Debug.Log ("Input down");

			inputDown = true;
			if (isButtonDown != null) {
				StopCoroutine (isButtonDown);}
			isButtonDown =  StartCoroutine (openMenu ());

		}

		public void turnOff()
		{//Debug.Log ("Turning off");
			inputDown = false;
			Invoke ("delayedTurnOff", .11f);
		}
		void delayedTurnOff(){
			//Debug.Log ("Really turn off");
			inputDown = false;
			StopCoroutine (isButtonDown);
		}



		IEnumerator openMenu()
		{
			yield return new WaitForSeconds (HoldDownTime);
			if (inputDown) {
				CustomerFurnitureMenu.instance.openMenu (false);
			}

		}
	}
}