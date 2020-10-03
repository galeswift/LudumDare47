using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_boss : MonoBehaviour
{
    public List<GameObject> ProjectileSpawnPoints = new List<GameObject>();
    public GameObject Projectile;
    public float FireRate = 2.0f;
    private float m_flLastFireTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time - m_flLastFireTime > FireRate)
        {
            m_flLastFireTime = Time.time;
            foreach (GameObject spawnPoint in ProjectileSpawnPoints)
            {
                GameObject.Instantiate(Projectile, spawnPoint.transform.position, spawnPoint.transform.rotation);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        gameObject.GetComponent<s_flasheffect>().Flash();
    }
}
