using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; //Linp使用時
using UnityEngine;
using UnityEngine.UI;
using UniRx; //UniRx使用時
using Cysharp.Threading.Tasks; //UniTask使用時
using DG.Tweening;
using UniRx.Triggers;

public class Test : MonoBehaviour
{
    // 公開フィールド
    [SerializeField] Transform correctLayers = default;
    [SerializeField] Transform correctFrame = default;
    [SerializeField] Transform colorPalette = default;
    [SerializeField] GameObject colorPicker = default;
    [SerializeField] GameObject colorSelector = default;
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

    /// <summary>
    /// 初期化時
    /// </summary>
    void Awake()
    {
        // タグでオブジェクトを検索
        stage = GameObject.FindWithTag("Stage").transform;

        // ステージを構成するオブジェクトを設定
        answerFrame = stage.transform.Find("Answer/Frame").gameObject;
        answerLayers = stage.transform.Find("Answer/Layers").transform;
    }

    ///<summary>
    ///初回動作開始時
    ///<summary>
    void Start()
    {
        // 正当レイヤーの準備
        SetupCorrectLayers();

        //回答レイヤーの準備
        SetupAnswerLayers();

        // カラーパレットの準備
        // SetupColorPallet();
    }

    ///<summary>
    ///正答レイヤーの準備
    ///<summary>
    void SetupCorrectLayers()
    {
        // 正当例を作成
        foreach (Transform layer in answerLayers)
        {
            // レイヤーをコピー
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
        // レイヤーをセットアップ
        foreach (Transform layer in answerLayers)
        {
            // タッチ判定を追加
            layer.gameObject.AddComponent<PolygonCollider2D>();
            layer.gameObject.AddComponent<ObservableEventTrigger>().OnPointerClickAsObservable().Subscribe(_ => ChangeColor(layer.gameObject, _.position)).AddTo(this);

            // 正解のカラーリストに追加
            correctColors.Add(layer.gameObject.GetComponent<SpriteRenderer>().color);

            // レイヤーのカラーを初期化
            layer.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    /// <summary>
    /// カラーパレットの作成
    /// </summary>
    void SetupColorPallet()
    {
        // 文字列型の使用カラーリスト
        List<string> stageColors = new List<string>();

        // デフォルトのカラーを保持
        Color defaultColor = colorPicker.GetComponent<SpriteRenderer>().color;

        // 使用カラーをリストに追加
        foreach (Color color in correctColors)
        {
            // デフォルトのカラーと同じでなければ追加
            if (color != defaultColor)
            {
                // 文字列でカラーリストに追加
                string htmlColor = "#" + ColorUtility.ToHtmlStringRGB(color);
                stageColors.Add(htmlColor);
            }
        }

        // 使用カラーの重複を削除
        stageColors = stageColors.Distinct().ToList();

        // カラー型の使用カラーリストを作成
        List<Color> palletColors = new List<Color>();
        foreach (string htmlColor in stageColors)
        {
            Color color = default(Color);
            if (ColorUtility.TryParseHtmlString(htmlColor, out color)) 
            {
                palletColors.Add(color);
            }
        }

        // デフォルトのカラーボタンからカラーパレットを作成
        for (int i = 0; i < palletColors.Count; i++)
        {
            // デフォルトのボタンをコピー
            GameObject newColorPicker = Instantiate(colorPicker, transform.position, Quaternion.identity);
            newColorPicker.transform.SetParent(colorPalette, false);
            newColorPicker.GetComponent<SpriteRenderer>().color = palletColors[i];
            newColorPicker.AddComponent<PolygonCollider2D>();
            newColorPicker.AddComponent<ObservableEventTrigger>().OnPointerClickAsObservable().Subscribe(_ => SelectColor(newColorPicker)).AddTo(this);
        }

        // デフォルトのカラーボタンにカラーパレットの設定
        colorPicker.AddComponent<PolygonCollider2D>();
        colorPicker.AddComponent<ObservableEventTrigger>().OnPointerClickAsObservable().Subscribe(_ => SelectColor(colorPicker)).AddTo(this);

        // デフォルトカラーをパレットの最後尾に変更
        colorPicker.transform.SetAsLastSibling();

        // キャンバスを強制更新
        Canvas.ForceUpdateCanvases();

        // 初期カラーを選択
        SelectColor(colorPalette.GetChild(0).gameObject);
    }

    ///<summary>
    ///レイヤーのカラーを変更
    ///<summary>
    void ChangeColor(GameObject layer, Vector3 touchPosition)
    {
        //前のカラーを保持
        Color beforeColor = layer.GetComponent<SpriteRenderer>().color;

        //カラーを変更
        layer.GetComponent<SpriteRenderer>().color = selectedColor;
    }

    /// <summary>
    /// カラーパレットの選択カラーを変更
    /// </summary>
    void SelectColor(GameObject colorPicekr)
    {
        // 選択カラーを変更
        selectedColor = colorPicekr.GetComponent<SpriteRenderer>().color;

        // セレクターを移動
        colorSelector.transform.DOMove(colorPicker.transform.position, .5f).SetEase(Ease.OutExpo);
    }
}