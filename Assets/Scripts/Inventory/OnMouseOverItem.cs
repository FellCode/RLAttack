using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMouseOverItem : MonoBehaviour
{

    public string onHoverText;
    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log(onHoverText);
        GameObject newGO = new GameObject("myTextGO");
        newGO.transform.SetParent(this.transform);

        Text myText = newGO.AddComponent<Text>();
        myText.text = onHoverText;

    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }
}
