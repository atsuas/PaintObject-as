using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintBucket : MonoBehaviour
{
    public Color[] colorList;
    public Color curColor;
    public int colorCount;

    //カーソル
    public Texture2D cursor;

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
            //カーソルを表示
            Cursor.SetCursor(cursor, new Vector2(cursor.width / 2.0f, cursor.height / 5.5f), CursorMode.Auto);

            if (hit.collider != null)
            {
                //カーソルを消す
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                
                SpriteRenderer sp = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                Debug.Log(hit.collider.name);

                sp.color = curColor;
            }
        }
    }

    //カラー設定
    public void paint(int colorCode)
    {
        colorCount = colorCode;
    }
}