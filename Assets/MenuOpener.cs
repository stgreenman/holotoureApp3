using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule.Tests
{ 
	public class MenuOpener : MonoBehaviour, IInputClickHandler {

		[Tooltip("Time to hold down in empty space before the furniture menu appears.")]
		public float HoldDownTime;
	// Use this for initialization
	void Start () {
			GameObject.FindObjectOfType<InputManager> ().PushFallbackInputHandler (this.gameObject);
	}
	
		float DownTime;

		public void OnInputUp(InputEventData eventData){
			Debug.Log ("Input up ");
			if (Time.time >= DownTime + HoldDownTime) {
				CustomerFurnitureMenu.instance.openMenu ();

			}


		}
		public 	void OnInputDown(InputEventData eventData){
			Debug.Log ("Input down");
			DownTime = Time.time;
		}
		public void OnInputClicked(InputEventData eventData)
		{
		}
}
}