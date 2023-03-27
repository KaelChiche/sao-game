using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public GameObject AnimatedEnemie;
    public ChaseState chaseState;
    public GameObject Target;
    private bool isAttacking = false;

    // Array of attack animation names
    private string[] attackAnimations = { "IsAttackingSlash", "IsAttackingSpin",};

    public override State RunCurrentState()
    {    
        float Distance_ = Vector3.Distance(AnimatedEnemie.transform.position, Target.transform.position);
        AnimatedEnemie.transform.LookAt(Target.transform.position, Vector3.up);
        if(Distance_ < 3)
        {
            if(!isAttacking)
            {
                isAttacking = true;
                
                // Choose a random attack animation name from the array
                string randomAttack = attackAnimations[Random.Range(0, attackAnimations.Length)];
                
                AnimatedEnemie.GetComponent<Animator>().SetTrigger(randomAttack);
                StartCoroutine(WaitForAnimationEnd());
                return this;
            }
            return this;
        }
        else
        {
            return chaseState;
        }
    }

    IEnumerator WaitForAnimationEnd()
    {
        Animator animator = AnimatedEnemie.GetComponent<Animator>();
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false;
    }
    
    public void EnemieHit()
    {
        Debug.Log("EnemieHit");
        AnimatedEnemie.GetComponent<Animator>().SetTrigger("IsHit");
    }
    
    public void StunEnemie()
    {
        Debug.Log("EnemieStuned");
        AnimatedEnemie.GetComponent<Animator>().SetTrigger("IsStunned");
    }
}