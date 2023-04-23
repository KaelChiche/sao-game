using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasButtonControl : MonoBehaviour
{
    public string ClickedButtonName;
    
    void Start()
    {
    
    }
    
    public void AsignName()
    {
        string ClickedButtonName = gameObject.name;
        StaticPlayerName.Name = ClickedButtonName;
    }
}