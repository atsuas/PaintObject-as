using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBucket : MonoBehaviour
{
    //カラー関連
    public Color[] colorList;
    public Color curColor;
    public int colorCount;

    //DoneButton OnClick時
    public GameObject doneButton;
    public GameObject nextButton;
    public GameObject retryButton;
    public GameObject colorsButton;
    public Animator leftAnimator;
    public Animator rightAnimator;

    //正当
    public GameObject[] yellowObj;
    public GameObject[] blueObj;
    public GameObject[] pinkObj;
    public GameObject[] whiteObj;
    // public GameObject frameObject;


    // ゲームオブジェクトのタグを変更する
    public void SetTag(string newTag)
    {
        GameObject[] untag = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(untag[0].name);
        Debug.Log(untag[1].name);
        Debug.Log(untag[2].name);
        Debug.Log(untag[3].name);
        Debug.Log(untag[4].name);
        Debug.Log(untag[5].name);

        
        // frameObject.tag = newTag;
    }
    
    void Start()
    {
        //正当フレームのカラー設定
        LegitimateFrame();
    }

    void Update()
    {
        //マウスポジション取得、クリックでカラーを変更
        ClickChangeColor();
    }

    //マウスポジション取得、クリックでカラーを変更
    public void ClickChangeColor()
    {
        //culColorはcolorCountの色ですよ
        curColor = colorList[colorCount];

        //マウスポジション
        Vector3 pos = Input.mousePosition;
        pos.z = 11.0f;
        var ray = Camera.main.ScreenToWorldPoint(pos);
        RaycastHit2D hit = Physics2D.Raycast(ray, -Vector2.up);

        //クリックしたらカラーを変更
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null)
            {
                SpriteRenderer sp = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                Debug.Log(hit.collider.name);

                sp.color = curColor;

                if (sp.color == sp.color)
                {
                    Debug.Log("黄色だよ");
                }

            }
        }
    }

    //カラーボタンOnClick用の設定
    public void paint(int colorCode)
    {
        colorCount = colorCode;
        if (colorCount == 0)
        {
            Debug.Log("黄色だよ");
        }
        else if (colorCount == 1)
        {
            Debug.Log("青色だよ");
        }
        else if (colorCount == 2)
        {
            Debug.Log("ピンク色だよ");
        }
        else if (colorCount == 3)
        {
            Debug.Log("白色だよ");
        }
    }

    // public void isClear(Color[] curColor, int colorCount)
    // {
    //     if (l1Obj.curColor == colorCount.0)
    //     {
    //         Debug.Log("黄色だよ");
    //     }
    // }

    //DoneButton OnClick時の処理
    public void DoneButton()
    {
        leftAnimator.SetBool("LeftAnimation", true);
        rightAnimator.SetBool("RightAnimation", true);
        doneButton.SetActive(false);
        nextButton.SetActive(true);
        retryButton.SetActive(true);
        colorsButton.SetActive(false);
    }


    //正当フレームのカラー設定
    public void LegitimateFrame()
    {
        //取得したオブジェクト達を配列に入れる
        yellowObj = GameObject.FindGameObjectsWithTag("Yellow");

        //配列の中身を１つずつ処理
        //YellowObject用
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
        
        //BlueObject用
        blueObj = GameObject.FindGameObjectsWithTag("Blue");
        foreach (GameObject obj in blueObj)
        {
                Renderer renderer = obj.GetComponent<SpriteRenderer>();

                Color color= renderer.material.color;
                color.r = 30/255f; 
                color.g = 168/255f; 
                color.b = 255/255f; 
                color.a = 255/255f;
                renderer.material.color = color;
        }

        //PinkObject用
        pinkObj = GameObject.FindGameObjectsWithTag("Pink");
        foreach (GameObject obj in pinkObj)
        {
                Renderer renderer = obj.GetComponent<SpriteRenderer>();

                Color color= renderer.material.color;
                color.r = 238/255f;
                color.g = 64/255f;
                color.b = 189/255f;
                color.a = 255/255f;
                renderer.material.color = color;
        }

        //WhiteObject用
        whiteObj = GameObject.FindGameObjectsWithTag("White");
        foreach (GameObject obj in whiteObj)
        {
                Renderer renderer = obj.GetComponent<SpriteRenderer>();

                Color color= renderer.material.color;
                color.r = 255/255f;
                color.g = 255/255f;
                color.b = 255/255f;
                color.a = 255/255f;
                renderer.material.color = color;
        }
    }

}