using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModifyText : MonoBehaviour
{
    public TMP_Text HealthBarText;
    
    // Start is called before the first frame update
    void Start()
    {   
        HealthBarText.text = "kirito";
    }

    // Update is called once per frame
    void Update()
    {
        HealthBarText.text = StaticPlayerName.Name;
    }
    
}
