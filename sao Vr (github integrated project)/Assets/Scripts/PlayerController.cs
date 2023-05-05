using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float PlayerHealth;
    
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private HealthBar _healthbar;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth = _maxHealth;
        _healthbar.UpdateHealthBar(_maxHealth, PlayerHealth);
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
            isHit(10);
        }
    }
    
    void isHit(float damage)
    {
        PlayerHealth -= damage;
        _healthbar.UpdateHealthBar(_maxHealth, PlayerHealth);
        Debug.Log("player hit");
        
    }
    
    void IsDead()
    {
        //Debug.Log("Player is DEAD!");
        transform.position = new Vector3(0, 40, 0);
        PlayerHealth = 100;
        
    }
}
