using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FingerCursor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //カーソル
    public Texture2D cursor;


    /// <summary>
    /// UIのカーソル
    /// </summary>
    // カーソルが対象オブジェクトに入った時
    public void OnPointerEnter(PointerEventData eventData )
    {
        //カーソルを表示
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2.0f, cursor.height / 5.5f), CursorMode.Auto);
    }

    // カーソルが対象オブジェクトから出た時
    public void OnPointerExit(PointerEventData eventData )
    {
        //カーソルを消す
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }


    /// <summary>
    /// Colliderのカーソル
    /// </summary>
    /// カーソルが対象オブジェクトに入った時
    void OnMouseEnter()
    {
        //カーソルを表示
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2.0f, cursor.height / 5.5f), CursorMode.Auto);
    }

    // カーソルが対象オブジェクトから出た時
    void OnMouseExit()
    {
        //カーソルを消す
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
