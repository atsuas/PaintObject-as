﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PaintBucket : MonoBehaviour
{
    //カラー関連
    [Header("カラー関連")]
    public Color[] colorList;
    public Color curColor;
    public int colorCount;

    //セレクター表示
    public GameObject[] Selectors;

    //正当カラー
    [Header("正当カラー")]
    [SerializeField]
    private GameObject[] yellowObj;
    [SerializeField]
    private GameObject[] blueObj;
    [SerializeField]
    private GameObject[] pinkObj;
    [SerializeField]
    private GameObject[] whiteObj;
    [SerializeField]
    private GameObject[] greenObj;
    [SerializeField]
    private GameObject[] redObj;
    [SerializeField]
    private GameObject[] blackObj;


    //フレームのタグ設定用
    [Header("フレームタグ設定")]
    public GameObject[] ColorTag;

    
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

    /// <summary>
    /// スタート時〜DoneButton押下までの処理
    /// </summary>
    
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

        //GreenObject用
        greenObj = GameObject.FindGameObjectsWithTag("Green");
        foreach (GameObject obj in greenObj)
        {
                Renderer renderer = obj.GetComponent<SpriteRenderer>();

                Color color= renderer.material.color;
                color.r = 86/255f;
                color.g = 195/255f;
                color.b = 96/255f;
                color.a = 255/255f;
                renderer.material.color = color;
        }

        //RedObject用
        redObj = GameObject.FindGameObjectsWithTag("Red");
        foreach (GameObject obj in redObj)
        {
                Renderer renderer = obj.GetComponent<SpriteRenderer>();

                Color color= renderer.material.color;
                color.r = 255/255f;
                color.g = 112/255f;
                color.b = 112/255f;
                color.a = 255/255f;
                renderer.material.color = color;
        }

        //BlackObject用
        blackObj = GameObject.FindGameObjectsWithTag("Black");
        foreach (GameObject obj in blackObj)
        {
                Renderer renderer = obj.GetComponent<SpriteRenderer>();

                Color color= renderer.material.color;
                color.r = 96/255f;
                color.g = 96/255f;
                color.b = 96/255f;
                color.a = 255/255f;
                renderer.material.color = color;
        }
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
            //カラーボタンを押下時にセレクター表示
            Selectors[0].SetActive(true);
            Selectors[1].SetActive(false);
            Selectors[2].SetActive(false);
            Selectors[3].SetActive(false);
            Debug.Log("黄色だよ");
        }
        else if (colorCount == 1)
        {
            Selectors[0].SetActive(false);
            Selectors[1].SetActive(true);
            Selectors[2].SetActive(false);
            Selectors[3].SetActive(false);
            Debug.Log("青色だよ");
        }
        else if (colorCount == 2)
        {
            Selectors[0].SetActive(false);
            Selectors[1].SetActive(false);
            Selectors[2].SetActive(true);
            Selectors[3].SetActive(false);
            Debug.Log("ピンク色だよ");
        }
        else if (colorCount == 3)
        {
            Selectors[0].SetActive(false);
            Selectors[1].SetActive(false);
            Selectors[2].SetActive(false);
            Selectors[3].SetActive(true);
            Debug.Log("白色だよ");
        }
    }

/// <summary>
/// DoneButton押下後
/// </summary>

    //DoneButton OnClick時
    [Header("DoneButton、OnClick用")]
    public GameObject doneButton;
    public GameObject nextButton;
    public GameObject retryButton;
    public GameObject colorsButton;
    public Animator leftAnimator;
    public Animator rightAnimator;

    //クリア判定用
    [Header("クリア判定用")]
    public GameObject[] frameObjects;
    public GameObject[] clearObjects;
    public GameObject grayStars;
    public GameObject[] goldStars;
    public Animator[] goldStarSet;


    //DoneButton OnClick時の処理
    public void ClickDoneButton()
    {
        leftAnimator.SetBool("LeftAnimation", true);
        rightAnimator.SetBool("RightAnimation", true);
        doneButton.SetActive(false);
        nextButton.SetActive(true);
        retryButton.SetActive(true);
        colorsButton.SetActive(false);
        Invoke("SetGrayStars", 1.0f);
        
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            //クリア条件分岐
            Level1Clear();
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            Level2Clear();
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            Level3Clear();
        }

    }

    //RetryButton OnClick時の処理
    public void ClickRetryButton()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level1");
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("Level2");
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            SceneManager.LoadScene("Level3");
        }

    }

    //NextButton OnClick時の処理
    public void ClickNextButton()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("Level3");
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            SceneManager.LoadScene("Level4");
        }

    }

    //Level1 クリア条件分岐
    public void Level1Clear()
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

    //Level2 クリア条件分岐
    public void Level2Clear()
    {
        //クリアの処理
        if (frameObjects[0].CompareTag("YellowColor") == clearObjects[0].CompareTag("Yellow") &&
            frameObjects[1].CompareTag("PinkColor") == clearObjects[1].CompareTag("Pink") &&
            frameObjects[2].CompareTag("PinkColor") == clearObjects[2].CompareTag("Pink") &&
            frameObjects[3].CompareTag("WhiteColor") == clearObjects[3].CompareTag("White") &&
            frameObjects[4].CompareTag("BlueColor") == clearObjects[4].CompareTag("Blue"))
        {
            Invoke("SetStar1", 2f);
            Invoke("SetStar2", 2.5f);
            Invoke("SetStar3", 3.0f);
            Debug.Log("クリアだよ");
        }
        //星2の処理
        else if (frameObjects[0].CompareTag("YellowColor") == clearObjects[0].CompareTag("Yellow") &&
            frameObjects[1].CompareTag("PinkColor") == clearObjects[1].CompareTag("Pink") &&
            frameObjects[2].CompareTag("PinkColor") == clearObjects[2].CompareTag("Pink"))
        {
            Invoke("SetStar1", 2f);
            Invoke("SetStar2", 2.5f);
            Debug.Log("星２だよ");
        }
        //星1の処理
        else if (frameObjects[0].CompareTag("YellowColor") == clearObjects[0].CompareTag("Yellow"))
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

    //Level3 クリア条件分岐
    public void Level3Clear()
    {
        //クリアの処理
        if (frameObjects[0].CompareTag("GreenColor") == clearObjects[0].CompareTag("Green") &&
            frameObjects[1].CompareTag("RedColor") == clearObjects[1].CompareTag("Red") &&
            frameObjects[2].CompareTag("BlackColor") == clearObjects[2].CompareTag("Black") &&
            frameObjects[3].CompareTag("BlackColor") == clearObjects[3].CompareTag("Black") &&
            frameObjects[4].CompareTag("BlackColor") == clearObjects[4].CompareTag("Black") &&
            frameObjects[5].CompareTag("BlackColor") == clearObjects[5].CompareTag("Black") &&
            frameObjects[6].CompareTag("BlackColor") == clearObjects[6].CompareTag("Black"))
        {
            Invoke("SetStar1", 2f);
            Invoke("SetStar2", 2.5f);
            Invoke("SetStar3", 3.0f);
            Debug.Log("クリアだよ");
        }
        //星2の処理
        else if (frameObjects[0].CompareTag("GreenColor") == clearObjects[0].CompareTag("Green") &&
            frameObjects[1].CompareTag("RedColor") == clearObjects[1].CompareTag("Red"))
        {
            Invoke("SetStar1", 2f);
            Invoke("SetStar2", 2.5f);
            Debug.Log("星２だよ");
        }
        //星1の処理
        else if (frameObjects[1].CompareTag("RedColor") == clearObjects[1].CompareTag("Red"))
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

}