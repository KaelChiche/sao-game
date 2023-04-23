using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarEnemyAi : MonoBehaviour
{
    Animator animator;
    
    public float MaxSpeed;
    public float SightRange;
    public float DetectionRange;
    public Rigidbody rb;
    public GameObject Target;
    
    public float moveSpeed = 3f;
    public float rotSpeed = 100f;
    

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    

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

            if (isWandering == false)
            {
                StartCoroutine(Wander());
            }
            if(isRotatingRight == true)
            {
                transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
            }
            if(isRotatingLeft == true)
            {
                transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
            }
            if(isWalking == true)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                animator.SetBool("IsWalking", true);
            }
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
                
                //Debug.Log(randomAttack);
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

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(1, 2);
        int walkWait = Random.Range(1, 3);
        int walkTime = Random.Range(1,5);


        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        animator.SetBool("IsWalking", false);
        yield return new WaitForSeconds(rotateWait);
        if(rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime); 
            isRotatingRight = false;
        }
        if(rotateLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isWandering = false;
    }


    public void EnemieHit()
    {
        //Debug.Log("EnemieHit");
        animator.SetTrigger("IsHit");
    }
    
    public void StunEnemy()
    {
        //Debug.Log("EnemieStuned");
        animator.SetTrigger("IsStunned");
    }

}

