using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class Swords : MonoBehaviour
{
    public InputActionReference triggerReference = null;
    
    public EnemyAI enemyAi;
    
    public GameObject Player;
    public GameObject Tip;
    
    private XRGrabInteractable grabInteractable;
    private Collider objectCollider;
    private Rigidbody rb;
    
    public bool Held;
    public float velocity;
    
    private float cooldownTime = 0.5f;
    private float nextSwingTime = 0.0f;
    
    private float cooldownTimeBoost = 3f;
    private float nextBoostTime = 0f;
    
    // Awake is called before the first frame update
    void Awake()
    {
        // Get the XRGrabInteractable component on the object
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Get the Collider component on the object
        objectCollider = GetComponent<Collider>();
        
        rb = Player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CalculateVelocity());
        
        bool buttonPressed = triggerReference.action.ReadValue<float>() > 0.3;
        
        if(buttonPressed && Held && Time.time > nextBoostTime)
        {
            Boost();
            
            nextBoostTime = Time.time + cooldownTimeBoost;
            
            buttonPressed = false;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (Time.time > nextSwingTime)
        {
            if(other.gameObject.tag == "Enemie")
            {
                //Debug.Log("EnemieHit");
                enemyAi.EnemieHit();
            }

            if(other.gameObject.tag == "EnemieWeapon")
            {
                //Debug.Log("EnemieWeapon");
                enemyAi.StunEnemy();
            }
            
            nextSwingTime = Time.time + cooldownTime;
        }
    }
    
    
    void Boost()
    {
        Debug.Log("boost!");
            
        Vector3 forceDirection = Tip.transform.position - Player.transform.position;
        //rb.AddForce(BoostDirection * 100, ForceMode.Impulse);
        rb.AddForce(forceDirection * 100, ForceMode.Impulse);


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
        Held = true;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        // Set the collider to not be a trigger when it is released
        objectCollider.isTrigger = false;
        Held = false;
    }
    
    
    IEnumerator CalculateVelocity()
    {
        Vector3 lastPosition = transform.position;
        yield return new WaitForFixedUpdate();
        velocity = (lastPosition - transform.position).magnitude / Time.deltaTime;
    }
}
