using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; //Linp使用時
using UnityEngine;
using UnityEngine.UI;
using UniRx; //UniRx使用時
using Cysharp.Threading.Tasks; //UniTask使用時
using UniRx.Triggers;

public class Test : MonoBehaviour
{
    // 公開フィールド
    [SerializeField] Transform correctLayers = default;
    [SerializeField] Transform correctFrame = default;
    [SerializeField] Transform colorPalette = default;
    [SerializeField] GameObject colorPicker = default;
    [SerializeField] GameObject colorSelecter = default;
    [SerializeField] GameObject mask = default;
    [SerializeField] GameObject doneButton = default;
    [SerializeField] GameObject compareButton = default;
    [SerializeField] GameObject hintButton = default;
    [SerializeField] Transform result = default;
    [SerializeField] GameObject starBox = default;
    [SerializeField] GameObject scanLightPrefab = default;
    [SerializeField] GameObject paperShowerPrefab = default;

    // ステージのオブジェクト
    Transform stage = default;
    Transform answerLayers = default;
    GameObject answerFrame = default;
    GameObject avPlayer = default;

    // 選択カラー
    Color selectedColor = default;

    // 正解のカラーリスト
    List<Color> correctColors = new List<Color>();

    // プライベートプロパティ
    Subject<bool> OnFinish { get; } = new Subject<bool>();

    ///<summary>
    ///初回動作開始時
    ///<summary>
    void Start()
    {
        // 正当レイヤーの準備
        // SetupCorrectLayers();
    }

    ///<summary>
    ///正答レイヤーの準備
    ///<summary>
    void SetupCorrectLayers()
    {
        // 正当例を作成
        foreach (Transform layer in answerLayers)
        {
            GameObject layerClone = Instantiate(layer.gameObject);
            layerClone.transform.SetParent(correctLayers, false);
            layerClone.GetComponent<SpriteRenderer>().sortingOrder = layerClone.GetComponent<SpriteRenderer>().sortingOrder - 1000;
        }

        // フレームをコピー
        GameObject frameClone = Instantiate(answerFrame);
        frameClone.transform.SetParent(correctFrame, false);
        frameClone.GetComponent<SpriteRenderer>().sortingOrder = frameClone.GetComponent<SpriteRenderer>().sortingOrder-1000;
    }

    ///<summary>
    ///回答レイヤーの準備
    ///<summary>
    void SetupAnswerLayers()
    {
        foreach (Transform layer in answerLayers)
        {
            layer.gameObject.AddComponent<PolygonCollider2D>();
            layer.gameObject.AddComponent<ObserVableEventTrigger>().OnPointerClickAsObservable().Subscribe(_ => ChangeColor(layer.gameObject, _.position)).AddTo(this);

            correctColors.Add(layer.gameObject.GetComponent<SpriteRenderer>().color);

            layer.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

}