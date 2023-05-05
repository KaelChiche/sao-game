using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Shield : MonoBehaviour
{
    public EnemyAI enemyAi;
    
    public float velocity;
    public float KnockBackMultiplier;
    
    private XRGrabInteractable grabInteractable;
    private Collider objectCollider;

    // Awake is called before the first frame update
    void Awake()
    {
        // Get the XRGrabInteractable component on the object
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Get the Collider component on the object
        objectCollider = GetComponent<Collider>();
    }
    
    void Update()
    {
        StartCoroutine(CalculateVelocity());
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemie"))
        {
            //Debug.Log("EnemieHit");
            enemyAi.KnockBack();

            // Calculate the knockback direction
            Vector3 knockbackDirection = transform.position - other.transform.position;
            knockbackDirection.Normalize();

            // Set the Rigidbody to kinematic to prevent it from being moved by the physics engine
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            
            
            Debug.Log(knockbackDirection);
            Debug.Log(velocity);
            Debug.Log(KnockBackMultiplier);
            Debug.Log(other);
            
            
            // Apply the knockback force in the opposite direction of the collision
            rb.AddForce(knockbackDirection * 50, ForceMode.Impulse);
            Debug.Log("Knocked back");
        }
    }


    private void OnEnable()
    {
        // Subscribe to the selectEntered event of the grab interactable
        grabInteractable.selectEntered.AddListener(OnGrabbed);

        // Subscribe to the selectExited event of the grab interactable
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDisable()
    {
        // Unsubscribe from the selectEntered event of the grab interactable
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);

        // Unsubscribe from the selectExited event of the grab interactable
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Set the collider to be a trigger while it is grabbed
        objectCollider.isTrigger = true;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // Set the collider to not be a trigger when it is released
        objectCollider.isTrigger = false;
    }



    IEnumerator CalculateVelocity()
    {
        Vector3 lastPosition = transform.position;
        yield return new WaitForFixedUpdate();
        velocity = (lastPosition - transform.position).magnitude / Time.deltaTime;
    }
}
