using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureMenu : MonoBehaviour {
	public GameObject buttonTemplate;
	private int currentPage = 0; 

	public List<MaterialManager.MaterialPairing> MyMaterials;

	public static TextureMenu instance;
	public TextureCatalog myCatalog;

	public GameObject currentlySelected;
	List<GameObject> myPanels = new List<GameObject>();
	public GameObject prevButton;
	public GameObject nextButton;

	public int maxNumPerPage;

	void Awake()
	{
		instance = this;
	}

	public void nextPage()
	{
		//Debug.Log ("I am getting pressed");
		if (currentPage < MyMaterials.Count / maxNumPerPage) {
			currentPage++;
		}
		LoadPage (currentPage);
	}

	public void prevPage()
	{
		if (currentPage > -1) {
			currentPage--;

		}
		LoadPage (currentPage);
	}

protected	void LoadPage(int num)
	{
		foreach (GameObject obj in myPanels) {
			Destroy (obj);
		}
		myPanels.Clear ();


		for (int i =num*maxNumPerPage; i < num*maxNumPerPage+Mathf.Min ( MyMaterials.Count - num*maxNumPerPage, maxNumPerPage); i++) {
			GameObject obj = (GameObject)Instantiate (buttonTemplate, this.transform);
			obj.transform.FindChild ("Title").GetComponent<Text> ().text = MyMaterials[i].MatName;

			obj.transform.FindChild ("Icon").GetComponent<MeshRenderer> ().materials= new Material[1]{ MyMaterials [i].mat};
			obj.GetComponent<CustomButton> ().OtherScript = this;
			obj.GetComponent<CustomButton> ().variable = MyMaterials [i].mat;

			obj.GetComponent<CustomButton> ().functionName = "setObjectMaterial";
			obj.transform.localScale = new Vector3 (1,1,1);
			Vector3 tempPos = obj.transform.localPosition;
			tempPos.z = -2f;
			obj.transform.localPosition = tempPos;
			obj.transform.localRotation = Quaternion.identity;
			myPanels.Add (obj);
		}

		SetButtons ();
	}

	protected void SetButtons()
	{
		prevButton.SetActive (true);
		nextButton.SetActive (true);
		if (currentPage == 0) {
			prevButton.SetActive (false);
		}
		if (currentPage == ( MyMaterials.Count -1) /  maxNumPerPage) {
			nextButton.SetActive (false);
		}
	}


	public void setObjectMaterial(Object mat)
	{

		Material mater = (Material)mat;
		Debug.Log ("SetObjectMat is being called from " + this.gameObject  + "   " + mater);
		foreach (MeshRenderer rend in currentlySelected.GetComponents<MeshRenderer>()) {
			rend.material = mater;
		}
		foreach (MeshRenderer rend in currentlySelected.GetComponentsInChildren<MeshRenderer>()) {
			rend.material = mater;
		}
	}
	int currentIndex;
	public void CycleMaterials()
	{currentIndex++;
		if (currentIndex >= MyMaterials.Count) {
			currentIndex = 0;}

		setObjectMaterial (MyMaterials[currentIndex].mat);
	
	}

	public void loadPreferences(CustomerManager.FurniturePreference info)
	{//Debug.Log ("Loading ored");
		MyMaterials = info.materialPicks;
		myCatalog.Initialize ();
		LoadPage (0);
	}

	void openCatalog(){
		myCatalog.turnOn ( currentlySelected);

	}


	public void AlterNate(){
		Debug.Log ("Alternating");
		this.transform.parent.gameObject.SetActive (!this.transform.parent.gameObject.activeSelf);

		myCatalog.Alternate( currentlySelected);
	}



}

