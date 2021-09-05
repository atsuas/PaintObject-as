using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBucket : MonoBehaviour
{
    //カラー関連
    public Color[] colorList;
    public Color curColor;
    public int colorCount;

    //DoneButton押下後
    public GameObject doneButton;
    public GameObject nextButton;
    public GameObject retryButton;
    public GameObject colorsButton;
    public Animator leftAnimator;
    public Animator rightAnimator;

    public GameObject l1Obj;
    public GameObject l2Obj;
    public GameObject l3Obj;
    public GameObject l4Obj;
    public GameObject l5Obj;
    public GameObject l6Obj;

    
    void Start()
    {
        
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

    //DoneButton押下時の処理
    public void DoneButton()
    {
        leftAnimator.SetBool("LeftAnimation", true);
        rightAnimator.SetBool("RightAnimation", true);
        doneButton.SetActive(false);
        nextButton.SetActive(true);
        retryButton.SetActive(true);
        colorsButton.SetActive(false);
    }

}