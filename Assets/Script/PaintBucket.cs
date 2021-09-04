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
            }
        }
    }

    //カラーボタンOnClick用の設定
    public void paint(int colorCode)
    {
        colorCount = colorCode;
    }

    public void DoneButton()
    {
        doneButton.SetActive(false);
        nextButton.SetActive(true);
        retryButton.SetActive(true);
    }
}