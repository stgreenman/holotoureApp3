// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using System.Collections;

namespace HoloToolkit.Unity.InputModule.Tests
{
    public class TogglePopupMenu : MonoBehaviour
    {

		public Vector3 MenuOffSet;

        [SerializeField]
        private PopupMenu popupMenu = null;

        [SerializeField]
        private TestButton button = null;

		FurnitureManager.FurniturePairing currentFurniture;
		public void set(TestButton _button, PopupMenu _popupMenu, FurnitureManager.FurniturePairing currentFurn, bool inFolder){

			Debug.Log ("Setting in pop up " + currentFurn.FurnName);
			button = _button;
			popupMenu = _popupMenu;
			currentFurniture = currentFurn;




			popupMenu.transform.position = this.transform.position + MenuOffSet;
		

			foreach (ButtonObjectScaler but in popupMenu.transform.GetComponentsInChildren<ButtonObjectScaler> ()) {
				but.setObjectToScale (this.gameObject);
			}
			popupMenu.transform.GetComponentInChildren<ChangePref> ().setFurniture (currentFurniture);
			popupMenu.transform.GetComponentInChildren<ChangePref> ().setInFolder (inFolder);
			popupMenu.transform.Find ("TextureMenu").transform.GetComponentInChildren<TextureMenu> ().currentlySelected = this.gameObject;
		}


        private void Awake()
        {
            if (button)
            {
                button.Activated += ShowPopup;
            }

            //Skyler
            //popupMenu.CurrentPopupState = PopupMenu.PopupState.Closed;
           // Debug.Log("popupMenu.CurrentPopupState: " + popupMenu.CurrentPopupState);
        }


        private void OnDisable()
        {
            if (button)
            {
                button.Activated -= ShowPopup;
            }
        }


        private void ShowPopup(TestButton source)
        {
           

            if (popupMenu != null)
            {
                //Skyler
			
				if (popupMenu.CurrentPopupState == PopupMenu.PopupState.Closed) {

					Debug.Log("Opening Pop up menu " + currentFurniture.FurnName);
					popupMenu.transform.position = this.transform.position + MenuOffSet;
					popupMenu.Show (source.gameObject);

					foreach (ButtonObjectScaler but in popupMenu.transform.GetComponentsInChildren<ButtonObjectScaler> ()) {
						but.setObjectToScale (this.gameObject);
					}
					popupMenu.transform.GetComponentInChildren<ChangePref> ().setFurniture (currentFurniture);
					popupMenu.transform.Find ("TextureMenu").transform.GetComponentInChildren<TextureMenu> ().currentlySelected = this.gameObject;
					// pop children set to variables
					StartCoroutine (WaitForPopupToClose ());
				} else {
					
					if (popupMenu.currentSelectedObject != source.gameObject) {
						StartCoroutine (DelayedOpening (source));

					}
					Debug.Log ("Dismissing");
					popupMenu.Dismiss ();


				}
            }
        }

		IEnumerator DelayedOpening(TestButton source)
		{
			yield return new WaitForSeconds (0.1f);
			popupMenu.transform.position = this.transform.position + MenuOffSet;

			popupMenu.Show (source.gameObject);


			StartCoroutine (WaitForPopupToClose ());
		}

        private IEnumerator WaitForPopupToClose()
        {
            if (popupMenu)
            {
                while (popupMenu.CurrentPopupState == PopupMenu.PopupState.Open)
                {
                    yield return null;
                }
            }

            if (button)
            {
                button.Selected = false;
            }
        }

		void OnDrawGizmos()
		{
			Gizmos.DrawSphere (this.transform.position + MenuOffSet, .01f);
		}
    }
}