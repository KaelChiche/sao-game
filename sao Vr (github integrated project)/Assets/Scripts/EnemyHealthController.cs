using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    
    Animator animator;
    public float EnemyHealth;
    public Swords swords;
    
    [SerializeField] private float _maxHealth = 50;
    [SerializeField] private HealthBar _healthbar;
    
    private float cooldownTime = 0.5f;
    private float nextSwingTime = 0.0f;
    
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
        if (Time.time > nextSwingTime)
        {
            if(other.gameObject.tag == "PlayerSword")
            {
                EnemyIsHit();
            }
            
            nextSwingTime = Time.time + cooldownTime;
        }
    }
    
    
    void EnemyIsHit()
    {
        EnemyHealth -= (int)(swords.velocity * 0.25) + 1;
        //Debug.Log("Enemy: " + EnemyHealth);
        _healthbar.UpdateHealthBar(_maxHealth, EnemyHealth);
    }
    
    void EnemyIsDead()
    {
        animator.SetBool("IsDead", true);
        GetComponent<ParticleBurst>().OnTriggerDeathParticles();
        GetComponent<EnemyAI>().enabled = false;
        Destroy(gameObject, 3);
    }
    
}
