using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public AttackState attackState;
    public IdleState idleState;
    
    Animator animator;
    
    public bool isInAttackRange;

    public float MaxSpeed;

    public Rigidbody rb;
    public GameObject Target;
    public GameObject AnimatedEnemie;


    public override State RunCurrentState()
    {
    
    animator = AnimatedEnemie.GetComponent<Animator>();
    
    if(isInAttackRange)
    {
        animator.SetBool("IsChasing", false);
        isInAttackRange = false;
        return attackState;
    }
    
    else
    {
        animator.SetBool("IsChasing", true);
        var Heading = Target.transform.position - transform.position;
        var Distance = Heading.magnitude;
        var Direction = Heading / Distance;
        
        Vector3 Move = new Vector3(Direction.x * MaxSpeed, 0, Direction.z * MaxSpeed);
        rb.velocity = Move;
        
        // Make the enemy face the target
        AnimatedEnemie.transform.LookAt(Target.transform.position, Vector3.up);
        
        float Distance_ = Vector3.Distance(AnimatedEnemie.transform.position, Target.transform.position);
        //Debug.Log(Distance_);
        
        if(Distance_ < 3)
        {
            isInAttackRange = true;
        }
        
        if(Distance_ > 30)
        {
            return idleState;
        }
        
        return this;
    }
    }
}