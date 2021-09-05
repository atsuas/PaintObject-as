using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameObject : MonoBehaviour
{

    void Start()
    {
        //取得したオブジェクト達を配列に入れる
        GameObject[] blueObj = GameObject.FindGameObjectsWithTag("Blue");

        //配列の中身を１つずつ処理
        foreach (GameObject obj in blueObj)
        {
                //見付けたオブジェクトに付いているSpriteRendererを取得
                Renderer renderer = obj.GetComponent<SpriteRenderer>();

                Color color= renderer.material.color;
                color.r = 30/255f; 
                color.g = 168/255f; 
                color.b = 255/255f; 
                color.a = 255/255f;
                renderer.material.color = color;

                color.r = 245/255f; 
                color.g = 194/255f; 
                color.b = 73/255f; 
                color.a = 255/255f;
        }


        //取得したオブジェクト達を配列に入れる
        GameObject[] yellowObj = GameObject.FindGameObjectsWithTag("Yellow");

        //配列の中身を１つずつ処理
        foreach (GameObject obj in yellowObj)
        {
                //見付けたオブジェクトに付いているSpriteRendererを取得
                Renderer renderer = obj.GetComponent<SpriteRenderer>();

                Color color= renderer.material.color;
                color.r = 245/255f; 
                color.g = 194/255f; 
                color.b = 73/255f; 
                color.a = 255/255f;
                renderer.material.color = color;
        }


        //取得したオブジェクト達を配列に入れる
        GameObject[] pinkObj = GameObject.FindGameObjectsWithTag("Pink");

        //配列の中身を１つずつ処理
        foreach (GameObject obj in pinkObj)
        {
                //見付けたオブジェクトに付いているSpriteRendererを取得
                Renderer renderer = obj.GetComponent<SpriteRenderer>();

                Color color= renderer.material.color;
                color.r = 238/255f;
                color.g = 64/255f;
                color.b = 189/255f;
                color.a = 255/255f;
                renderer.material.color = color;
        }


        //取得したオブジェクト達を配列に入れる
        GameObject[] whiteObj = GameObject.FindGameObjectsWithTag("White");

        //配列の中身を１つずつ処理
        foreach (GameObject obj in whiteObj)
        {
                //見付けたオブジェクトに付いているSpriteRendererを取得
                Renderer renderer = obj.GetComponent<SpriteRenderer>();

                Color color= renderer.material.color;
                color.r = 255/255f;
                color.g = 255/255f;
                color.b = 255/255f;
                color.a = 255/255f;
                renderer.material.color = color;
        }
    }

    void Update()
    {
        
    }

    // public void OnCollisionEnter2D(Collision2D col2d)
    // {
    //     if (col2d.gameObject.CompareTag(this.gameObject.tag))
    //     {
    //         Debug.Log("クリアだよ");
    //     }
    //     else
    //     {
    //         Debug.Log("間違いだよ");
    //     }
    // }
}
