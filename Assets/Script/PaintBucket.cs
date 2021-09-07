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

    //フレームのタグ設定用
    public GameObject[] ColorTag;

    //クリア判定用
    public GameObject[] frameObjects;
    public GameObject[] clearObjects;
    public GameObject grayStars;
    public GameObject[] goldStars;
    public Animator[] goldStarSet;
    
    
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

                //スプライトのカラーをColorListのColorCountの色にする
                sp.color = curColor;

                //クリックでカラーTagを設定
                if (curColor == colorList[0])
                {
                    //ヒットしたコライダーのタグをYellowColorTagに変更
                    hit.collider.tag = ColorTag[0].tag;
                }
                else if (curColor == colorList[1])
                {
                    //Blue用
                    hit.collider.tag = ColorTag[1].tag;
                }
                else if (curColor == colorList[2])
                {
                    //Pink用
                    hit.collider.tag = ColorTag[2].tag;
                }
                else if (curColor == colorList[3])
                {
                    //White用
                    hit.collider.tag = ColorTag[3].tag;
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

    //DoneButton OnClick時の処理
    public void DoneButton()
    {
        leftAnimator.SetBool("LeftAnimation", true);
        rightAnimator.SetBool("RightAnimation", true);
        doneButton.SetActive(false);
        nextButton.SetActive(true);
        retryButton.SetActive(true);
        colorsButton.SetActive(false);
        Invoke("SetGrayStars", 1.0f);
        
        IsClear();
        
    }

    //クリア条件分岐
    public void IsClear()
    {
        //クリアの処理
        if (frameObjects[0].CompareTag("YellowColor") == clearObjects[0].CompareTag("Yellow") &&
            frameObjects[1].CompareTag("WhiteColor") == clearObjects[1].CompareTag("White") &&
            frameObjects[2].CompareTag("WhiteColor") == clearObjects[2].CompareTag("White") &&
            frameObjects[3].CompareTag("BlueColor") == clearObjects[3].CompareTag("Blue") &&
            frameObjects[4].CompareTag("BlueColor") == clearObjects[4].CompareTag("Blue") &&
            frameObjects[5].CompareTag("PinkColor") == clearObjects[5].CompareTag("Pink"))
        {
            Invoke("SetStar1", 2f);
            Invoke("SetStar2", 2.5f);
            Invoke("SetStar3", 3.0f);
            Debug.Log("クリアだよ");
        }
        //星2の処理
        else if (frameObjects[0].CompareTag("YellowColor") == clearObjects[0].CompareTag("Yellow") &&
            frameObjects[1].CompareTag("WhiteColor") == clearObjects[1].CompareTag("White") &&
            frameObjects[2].CompareTag("WhiteColor") == clearObjects[2].CompareTag("White") &&
            frameObjects[5].CompareTag("PinkColor") == clearObjects[5].CompareTag("Pink"))
        {
            Invoke("SetStar1", 2f);
            Invoke("SetStar2", 2.5f);
            Debug.Log("星２だよ");
        }
        //星1の処理
        else if (frameObjects[0].CompareTag("YellowColor") == clearObjects[0].CompareTag("Yellow") &&
            frameObjects[3].CompareTag("BlueColor") == clearObjects[3].CompareTag("Blue") &&
            frameObjects[4].CompareTag("BlueColor") == clearObjects[4].CompareTag("Blue"))
        {
            Invoke("SetStar1", 2f);
            Debug.Log("星１だよ");
        }
        //星０の処理
        else
        {
            Debug.Log("星０だよ");
        }
    }

    //グレースターを表示する
    public void SetGrayStars()
    {
        grayStars.SetActive(true);
    }

    //1つ目の星を表示
    public void SetStar1()
    {
        goldStars[0].SetActive(true);
        goldStarSet[0].SetBool("GoldStar1", true);
    }

    //2つ目の星を表示
    public void SetStar2()
    {
        goldStars[1].SetActive(true);
        goldStarSet[1].SetBool("GoldStar2", true);
    }
    //3つ目の星を表示
    public void SetStar3()
    {
        goldStars[2].SetActive(true);
        goldStarSet[2].SetBool("GoldStar3", true);
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