using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler {

	public MonoBehaviour OtherScript;
	public string functionName;
	public object variable;



	public void OnPointerEnter(PointerEventData data)
	{
		//Debug.Log ("Entered OnPointerEnter");

	}

	public void OnPointerExit(PointerEventData data)
	{
		//Debug.Log ("Entered OnPointerExit");

	}

	public void OnPointerDown(PointerEventData data)
	{
		OtherScript.SendMessage (functionName, variable, SendMessageOptions.DontRequireReceiver);
		GameObject.Find ("MenuOpener").SendMessage ("turnOff", SendMessageOptions.DontRequireReceiver);


	}

	public void OnPointerUp(PointerEventData data)
	{
		//Debug.Log ("Entered OnPointerUp");


	}


	}
