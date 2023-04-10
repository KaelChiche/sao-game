using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    
    Animator animator;
    public float EnemyHealth;
    
    [SerializeField] private float _maxHealth = 50;
    [SerializeField] private HealthBar _healthbar;
    
    // Start is called before the first frame update
    void Start()
    {
        EnemyHealth = _maxHealth;
        animator = GetComponent<Animator>();
        _healthbar.UpdateHealthBar(_maxHealth, EnemyHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyHealth <= 0)
        {
            EnemyIsDead();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerSword")
        {
            EnemyIsHit();
        }
    }
    
    
    void EnemyIsHit()
    {
        EnemyHealth -= 10;
        Debug.Log("Enemy: " + EnemyHealth);
        _healthbar.UpdateHealthBar(_maxHealth, EnemyHealth);
        
    }
    
    void EnemyIsDead()
    {
        //Debug.Log("Enemy is DEAD!");
        animator.SetBool("IsDead", true);
        Destroy(this);
    }
    
}
