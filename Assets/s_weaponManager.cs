using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_weaponManager : MonoBehaviour
{
    private float m_flLastFireTime = 0.0f;
    public GameObject ProjectilePrefab;
    public List<AudioClip> ProjectileSounds = new List<AudioClip>();
    private bool bFireWhenReady = false;
    private GameObject m_gameManager;
    private int m_nSteps = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_nSteps++;

        if (bFireWhenReady)
        {
          
            double flShotsPerSecond = m_gameManager.GetComponent<s_gameManager>().BeatsPerMinute / 60.0;
            double flShotInterval = 1.0 / flShotsPerSecond;
            int nStepsPerShot = (int)(flShotInterval / Time.fixedDeltaTime);
            if ( (int)((m_nSteps-1)/nStepsPerShot) < (int)(m_nSteps/nStepsPerShot))
            {
                bFireWhenReady = false;
                Fire();
            }
        }
    }

    public void TryFiring()
    {
        bFireWhenReady = true;            
    }

    void Fire()
    {        
        GameObject projectile = GameObject.Instantiate(ProjectilePrefab);
        projectile.transform.position = transform.parent.position;
        projectile.transform.rotation = transform.parent.rotation;

        GetComponent<AudioSource>().clip = ProjectileSounds[Random.Range(0, 3)];
        GetComponent<AudioSource>().Play();

    }
}
