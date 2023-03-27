using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public ChaseState chaseState;
    
    Animator animator;
    public GameObject AnimatedEnemie;

    private Collider[] hitColliders;
    private RaycastHit Hit;

    public float SightRange;
    public float DetectionRange;

    public Rigidbody rb;
    public GameObject Target;

    private bool seePlayer;

    public override State RunCurrentState()
    {
    
        animator = AnimatedEnemie.GetComponent<Animator>();

        if (!seePlayer)
        {
            float Distance_ = Vector3.Distance(AnimatedEnemie.transform.position, Target.transform.position);
            //Debug.Log(Distance_);
            hitColliders = Physics.OverlapSphere(transform.position, DetectionRange);
            foreach (var HitCollider in hitColliders)
            {
                if (HitCollider.CompareTag("Player"))
                {
                    Target = HitCollider.gameObject;
                    seePlayer = true;
                    break;
                }
            }
            
            //code for passive Ai
            //animator.SetBool("IsIdle", true);
        }
        else
        {
            float Distance_ = Vector3.Distance(AnimatedEnemie.transform.position, Target.transform.position);
            //Debug.Log(Distance_);
            
            if (Physics.Raycast(transform.position, (Target.transform.position - transform.position), out Hit, SightRange))
            {
                if (Hit.collider.CompareTag("Player") && Distance_ < 30)
                {
                    animator.SetBool("IsWalking", false);
                    return chaseState;
                }
                else
                {
                    seePlayer = false;
                }
            }
            else
            {
                seePlayer = false;
            }
        }  
        return this;
    }
}
