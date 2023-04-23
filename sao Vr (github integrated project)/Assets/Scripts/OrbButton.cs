using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbButton : MonoBehaviour
{
    
    public LevelChanger levelChanger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Hand")
        {
            levelChanger.playAnimation = true;
            Destroy(other.gameObject);
        }
    }
}
