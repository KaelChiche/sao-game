using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public GameObject Cylinders;
    public GameObject Orb;
    public bool playAnimation;
    public float linkStartSpeed;
    public Animator animator;
    
    private string levelToLoad;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playAnimation)
        {
            Debug.Log("playanimation");
            Cylinders.transform.position += Vector3.back * linkStartSpeed * Time.deltaTime;
            Orb.transform.position += Vector3.down * 15 * Time.deltaTime;
            
            if (Cylinders.transform.position.z <= -100)
            {
                FadeToLevel("Scene2");
            }
        }
        
        //if(???)
        //{
        //    playAnimation = true;
        //}
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
