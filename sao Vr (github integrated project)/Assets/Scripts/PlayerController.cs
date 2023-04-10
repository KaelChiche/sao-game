using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float PlayerHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = 100;
        //Debug.Log(PlayerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerHealth <= 0)
        {
            IsDead();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EnemieWeapon")
        {
            isHit();
        }
    }
    
    void isHit()
    {
        PlayerHealth -= 10;
        Debug.Log("Player: " + PlayerHealth);
        
    }
    
    void IsDead()
    {
        //Debug.Log("Player is DEAD!");
        transform.position = new Vector3(0, 40, 0);
        PlayerHealth = 100;
        
    }
}
