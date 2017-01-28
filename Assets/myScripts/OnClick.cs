using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.UI;


public class OnClick : MyManager, IInputClickHandler
{
   

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnInputClicked(InputEventData eventData)
    {


         if (eventData.currentInputModule.gameObject != null)
          {
            Debug.Log("eventData.currentInputModule.gameObject.name: " + eventData.currentInputModule.gameObject.name);

            texturesPanel.SetActive(true);
        }
        else
        {
            Debug.Log("I've been unclicked");
            texturesPanel.SetActive(false);
        }


        /*
                optionsPanel = GameObject.Find("Options Panel");
                optionsPanel.gameObject.SetActive(true);

                furniturePanel = GameObject.Find("Furniture Panel");
                furniturePanel.gameObject.SetActive(true);
                */

        // Get ActionPanel's children elements
        /*
        GameObject[] children = GameObject.FindGameObjectsWithTag("picture");
        for (int i = 0; i < children.Length; i++)
        {
            // Vector3 pos = children[i].gameObject.transform.position;
            //Vector3 onTop = new Vector3(pos.x, pos.y, pos.z + 0.01F);

            // Offset images in front of the actionPanel so they don't get washed out by actionPanel.
            // Calling transform.position (setting it on the right side of the equation) in C# doesn't the object's position--only a copy of the variable; 
            // You must save it to a temp variable and reassign it (See below).
            Vector3 temp = children[i].gameObject.transform.position;
            temp.z = temp.z - .01f;
            children[i].gameObject.transform.position = temp;
            //(children[i].gameObject.transform.position.z + 0.01F);
        }
        */

    }


}