using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameObject : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D col2d)
    {
        if (col2d.gameObject.CompareTag(this.gameObject.tag))
        {
            Debug.Log("クリアだよ");
        }
        else
        {
             Debug.Log("間違いだよ");
        }
    }
}
