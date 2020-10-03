using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_scroller : MonoBehaviour
{
    public float theScrollSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - theScrollSpeed * Time.deltaTime, transform.position.z);
        
    }
}
