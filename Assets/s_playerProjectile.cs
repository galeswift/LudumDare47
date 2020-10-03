using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_playerProjectile : MonoBehaviour
{
    public float ProjectileSpeed = 5f;
    public float ProjectileLifetime = 1.0f;
    public float ProjectileRotation = 0.0f;    
    private float m_flSpawnTime = 0.0f;
    private Vector3 m_vInitialFacing;
    // Start is called before the first frame update
    void Start()
    {
        m_flSpawnTime = Time.time;
        m_vInitialFacing = transform.up;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.
        transform.position = transform.position + m_vInitialFacing * ProjectileSpeed * Time.deltaTime;
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, ProjectileRotation * Time.deltaTime);
        if (Time.time - m_flSpawnTime > ProjectileLifetime)
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject.Destroy(gameObject);
    }
}
