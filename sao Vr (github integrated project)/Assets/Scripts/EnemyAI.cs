using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Animator animator;
    
    public float MaxSpeed;
    public float SightRange;
    public float DetectionRange;
    public Rigidbody rb;
    public GameObject Target;


    private Collider[] hitColliders;
    private RaycastHit Hit;
    
    private float Speed;
    private bool seePlayer;
    
    private bool isIdleState;
    private bool isAttackingState;
    
    private bool isAttacking;
    private string[] attackAnimations = {"IsAttackingSlash", "IsAttackingSlash", "IsAttackingSmash"};
    // Start is called before the first frame update
    
    void Start()
    {
        Speed = MaxSpeed;
        animator = GetComponent<Animator>();
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        float Distance_ = Vector3.Distance(transform.position, Target.transform.position);

        if(!seePlayer)
        {
            hitColliders = Physics.OverlapSphere(transform.position, DetectionRange);
            foreach (var HitCollider in hitColliders)
            {
                if(HitCollider.tag == "Player")
                {
                    Target = HitCollider.gameObject;
                    seePlayer = true;
                }
            }

            //passive AI code:
        }
        else
        {
            if(Physics.Raycast(transform.position, (Target.transform.position - transform.position), out Hit, SightRange))
            {
                if(Hit.collider.tag != "Player")
                {
                    seePlayer = false;
                }
                else
                {
                    var Heading = Target.transform.position - transform.position;
                    var Distance = Heading.magnitude;
                    var Direction = Heading / Distance;

                    Vector3 Move = new Vector3(Direction.x * Speed, 0, Direction.z * Speed);
                    
                    rb.velocity = Move;
                    transform.forward = Move;
                    
                    if(Distance_ < 5)
                    {
                        isAttackingState = true;
                    }
                    else
                    {
                        animator.SetBool("IsChasing", true);
                    } 
                }
            }
        }



        if(isAttackingState)
        {
            if(!isAttacking)
            {
                animator.SetBool("IsChasing", false);
                isAttacking = true;

                string randomAttack = attackAnimations[Random.Range(0, attackAnimations.Length)];
                
                Debug.Log(randomAttack);
                animator.SetTrigger(randomAttack);
                StartCoroutine(WaitForAnimationEnd());
            }
        
            if(Distance_ > 5)
            {
                isAttackingState = false;
            }
        }
        
        if(Distance_ > 30)
            {
                seePlayer = false;
                animator.SetBool("IsChasing", false);
            }
    }



    IEnumerator WaitForAnimationEnd()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false;
    }



    public void EnemieHit()
    {
        //Debug.Log("EnemieHit");
        animator.SetTrigger("IsHit");
    }
    
    public void StunEnemie()
    {
        //Debug.Log("EnemieStuned");
        animator.SetTrigger("IsStunned");
    }

}
