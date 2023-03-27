using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade: MonoBehaviour
{
    public GameObject Axe;
    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void activateAxe()
    {
        Axe.GetComponent<Collider>().enabled = true;
        //Debug.Log("collider on");
    }
    public void deactivateAxe()
    {
        Axe.GetComponent<Collider>().enabled = false;
        //Debug.Log("collider off");
    }
}
