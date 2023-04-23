using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBurst : MonoBehaviour
{
    public ParticleSystem deathParticles;
    public MeshRenderer mesh;
    public bool once = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerDeathParticles()
    {
        if(once)
        {
            var em = deathParticles.emission;
            var dur = deathParticles.duration;
            
            em.enabled = true;
            deathParticles.Play();
            
            once = false;
            Destroy(mesh);
            Invoke(nameof(DestroyOBJ), dur);
            
        }
    }
    void DestroyOBJ()
    {
        Destroy(gameObject);
    }
}
