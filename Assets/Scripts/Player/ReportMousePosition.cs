using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ReportMousePosition : MonoBehaviour
{
    private Vector2 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         mousePosition = Mouse.current.position.ReadValue();
        
    }

    public Vector2 getMousePosition(){
        return mousePosition;
    }
}
