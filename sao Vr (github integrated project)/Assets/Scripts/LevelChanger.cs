using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public GameObject Cylinders;
    public GameObject Pedestal;
    public GameObject Button;
    public bool playAnimation;
    public float linkStartSpeed;
    public Animator animator;
    public GameObject InputObject;
 
    ButtonState buttonstate;
    
    private string levelToLoad;
    
    // Start is called before the first frame update
    void Start()
    {
        buttonstate = InputObject.GetComponent<ButtonState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playAnimation)
        {
            Cylinders.transform.position += Vector3.back * linkStartSpeed * Time.deltaTime;
            Pedestal.transform.position += Vector3.down * 15 * Time.deltaTime;
            Button.transform.position += Vector3.down * 15 * Time.deltaTime;
            
            if (Cylinders.transform.position.z <= -50)
            {
                FadeToLevel("Scene2");
            }
        }
        
        if(buttonstate.TheButtonIsPressed)
        {
            playAnimation = true;
        }
    }
    
    public void FadeToLevel (string levelName)
    {
        levelToLoad = levelName;
        animator.SetTrigger("FadeAnimation");
    }
    
    public void OnFadeComplete ()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
