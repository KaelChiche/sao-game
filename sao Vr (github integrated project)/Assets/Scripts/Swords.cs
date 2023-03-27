using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swords : MonoBehaviour
{
    public AttackState attackstate;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemie")
        {
            //Debug.Log("EnemieHit");
            attackstate.EnemieHit();
        }
        
        if(other.gameObject.tag == "EnemieWeapon")
        {
            //Debug.Log("EnemieWeapon");
            attackstate.StunEnemie();
        }
    }
}
