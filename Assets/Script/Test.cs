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
// using App.Services;
using UnityEngine.EventSystems;

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
        SetupColorPallet();

        // 完了ボタンの準備
        SetupDoneButton();
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

            if (i == 0)
            {
                // デフォルトのボタンをコピー
                GameObject newColorPicker = Instantiate(colorPicker, transform.position, Quaternion.identity);
                newColorPicker.transform.SetParent(colorPalette, false);
                newColorPicker.GetComponent<SpriteRenderer>().color = palletColors[i];
                newColorPicker.AddComponent<PolygonCollider2D>();
                newColorPicker.AddComponent<ObservableEventTrigger>().OnPointerClickAsObservable().Subscribe(_ => SelectColor(newColorPicker)).AddTo(this);
            }
            else if (i == 1)
            {
                // デフォルトのボタンをコピー
                GameObject newColorPicker = Instantiate(colorPicker, new Vector3(9.0f, 0.0f, 0.0f), Quaternion.identity);
                newColorPicker.transform.SetParent(colorPalette, false);
                newColorPicker.GetComponent<SpriteRenderer>().color = palletColors[i];
                newColorPicker.AddComponent<PolygonCollider2D>();
                newColorPicker.AddComponent<ObservableEventTrigger>().OnPointerClickAsObservable().Subscribe(_ => SelectColor(newColorPicker)).AddTo(this);
            }
            else if (i == 2)
            {
                // デフォルトのボタンをコピー
                GameObject newColorPicker = Instantiate(colorPicker, new Vector3(18.0f, 0.0f, 0.0f), Quaternion.identity);
                newColorPicker.transform.SetParent(colorPalette, false);
                newColorPicker.GetComponent<SpriteRenderer>().color = palletColors[i];
                newColorPicker.AddComponent<PolygonCollider2D>();
                newColorPicker.AddComponent<ObservableEventTrigger>().OnPointerClickAsObservable().Subscribe(_ => SelectColor(newColorPicker)).AddTo(this);
            }

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

    /// <summary>
    /// 完了ボタンの準備
    /// </summary>
    void SetupDoneButton()
    {
        // タッチ判定を追加
        doneButton.AddComponent<PolygonCollider2D>();
        doneButton.AddComponent<ObservableEventTrigger>().OnPointerClickAsObservable().Subscribe(_ => {
            UniTask.Void(async () => {
                await Finish();
            });
        }).AddTo(this);
    }

    ///<summary>
    ///レイヤーのカラーを変更
    ///<summary>
    void ChangeColor(GameObject layer, Vector3 touchPosition)
    {
        // 前のカラーを保持
        Color beforeColor = layer.GetComponent<SpriteRenderer>().color;

        // カラーを変更
        layer.GetComponent<SpriteRenderer>().color = selectedColor;

        // カラー変更アニメーション
        ShowChangeColorAnimation(layer, touchPosition, beforeColor);

        // SE再生
        // ServiceLocator.Get<AVPlayerService>().Paint();
    }

    ///<summary>
    /// レイヤーのカラー変更アニメーション
    ///<summary>
    void ShowChangeColorAnimation(GameObject layer, Vector3 touchPosition, Color beforeColor)
    {
        // タップ位置を計算
        float touchPositionX = (touchPosition.x / 10) - ((Screen.width / 10) / 2);
        float touchPositionY = (touchPosition.y / 10) - ((Screen.height / 10) / 2);

        // レイヤーをクローン
        GameObject layerClone = Instantiate(layer, transform.position, Quaternion.identity);
        layerClone.transform.SetParent(answerLayers);
        layerClone.transform.position = layer.transform.position;
        layerClone.transform.localScale = new Vector3(1, 1, 1);
        layerClone.GetComponent<SpriteRenderer>().color = beforeColor;
        layerClone.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
        layerClone.GetComponent<SpriteRenderer>().sortingOrder = layer.GetComponent<SpriteRenderer>().sortingOrder + 1;

        // マスクをクローン
        GameObject maskClone = Instantiate(mask, transform.position, Quaternion.identity);
        maskClone.transform.SetParent(answerLayers);
        maskClone.transform.localScale = new Vector3(0.01f, 0.01f, 0); // 小さくしておく
        maskClone.transform.position = new Vector3(touchPositionX, touchPositionY, 0);
        maskClone.GetComponent<SpriteMask>().renderingLayerMask = layerClone.GetComponent<SpriteRenderer>().renderingLayerMask;

        // アニメーション時間
        float duration = 0.7f;

        // スケールアニメーション
        float scale = (layer.GetComponent<RectTransform>().sizeDelta.x / 100) * 2.0f; // レイヤーサイズの2倍
        maskClone.transform.DOScale(scale, duration);

        // レイヤーとマスクを削除
        Observable.Timer(TimeSpan.FromSeconds(duration)).Subscribe(_ => Destroy(layerClone));
        Observable.Timer(TimeSpan.FromSeconds(duration)).Subscribe(_ => Destroy(maskClone));
    }

    /// <summary>
    /// カラーパレットの選択カラーを変更
    /// </summary>
    void SelectColor(GameObject colorPicker)
    {
        // 選択カラーを変更
        selectedColor = colorPicker.GetComponent<SpriteRenderer>().color;

        // セレクターを移動
        colorSelector.transform.DOMove(colorPicker.transform.position, .5f).SetEase(Ease.OutExpo);
    }

    /// <summary>
    /// 比較
    /// </summary>
    void Compare()
    {
        // UIを非表示
        DisappearUI();

        // 解答と正答の親を取得
        Transform answerLayersParent = answerLayers.parent;
        Transform correctLayersParent = correctLayers.parent;

        // 比較アニメーション
        Sequence compareSequence = DOTween.Sequence();
        compareSequence
            .Append(answerLayersParent.DOScale(90f, 0.5f))
            .Join(answerLayersParent.DOLocalMove(new Vector3(250f, 50f), 0.5f))
            .Join(correctLayersParent.DOScale(90f, 0.5f))
            .Join(correctLayersParent.DOLocalMove(new Vector3(-250f, 50f), 0.5f))
            .AppendInterval(0.5f)
            .SetLoops(2, LoopType.Yoyo); // 元に戻す

        // アニメーション完了時
        compareSequence.Play().OnComplete(() => 
        {
            // UIを戻す
            AppearUI();
        });
    }

    /// <summary>
    /// UIを非表示
    /// </summary>
    void DisappearUI()
    {
        // 完了ボタンを非表示
        doneButton.SetActive(false);

        // 比較ボタンを非表示
        compareButton.SetActive(false);

        // ヒントボタンを非表示
        // hintButton.SetActive(false);

        // 回答レイヤーのタッチ判定をオフ
        foreach (Transform layer in answerLayers.transform)
        {
            if (layer.gameObject.GetComponent<ObservableEventTrigger>() != null)
            {
                layer.gameObject.GetComponent<ObservableEventTrigger>().enabled = false;
            }
        }

        // カラーパレットを非表示
        colorSelector.transform.gameObject.SetActive(false);
        colorPalette.transform.gameObject.SetActive(false);

        // ヒントアニメーションの停止
        // for (int i = 0; i < correctColors.Count; i++)
        // {
        //     if (DOTween.TweensById(i) != null)
        //     {
        //         DOTween.TweensById(i).ForEach((tween) =>
        //         {
        //             tween.Restart();
        //             tween.Pause();
        //         });
        //     }
        // }
    }

    /// <summary>
    /// UIを表示
    /// </summary>
    void AppearUI()
    {
        // 完了ボタンを表示
        doneButton.SetActive(true);

        // 比較ボタンを表示
        compareButton.SetActive(true);

        // ヒントボタンを表示
        // hintButton.SetActive(true);

        // 回答レイヤーのタッチ判定をオン
        foreach (Transform layer in answerLayers.transform)
        {
            layer.gameObject.GetComponent<ObservableEventTrigger>().enabled = true;
        }

        // カラーパレットを表示
        colorSelector.transform.gameObject.SetActive(true);
        colorPalette.transform.gameObject.SetActive(true);
    }

    /// <summary>
    /// スコアを計算
    /// </summary>
    public int CalcScore()
    {
        // 現在の回答のカラーリスト
        List<Color> answerColors = new List<Color>();
        foreach (Transform answerLayer in answerLayers.transform)
        {
            if (answerLayer.gameObject.GetComponent<SpriteRenderer>() != null)
            {
                answerColors.Add(answerLayer.gameObject.GetComponent<SpriteRenderer>().color);
            }
        }

        // 正当数をカウント
        int correctCount = 0;
        for (int i = 0; i < correctColors.Count; i++)
        {
            string correctColor = ColorUtility.ToHtmlStringRGB(correctColors[i]);
            string answerColor = ColorUtility.ToHtmlStringRGB(answerColors[i]);

            if (correctColor == answerColor)
            {
                correctCount++;
            }
        }

        // 正答率を計算
        int score = 0;
        if (correctCount != 0) 
        {
            score = (correctCount * 100) / correctColors.Count;
        }

        Debug.Log($"CalcScore: correctCount={correctCount} maxCorrectCount={correctColors.Count} score={score}");

        return score;
    }

    /// <summary>
    /// 終了
    /// </summary>
    async UniTask Finish()
    {
        // スコアを計算
        int score = CalcScore();

        // 結果を表示
        await Result(score);

        // 完了を判定
        bool completed = score > 0 ? true : false;

        // 終了を通知
        OnFinish.OnNext(completed);
    }

    /// <summary>
    /// 結果を表示
    /// </summary>
    public async UniTask Result(int score)
    {
        // UIを非表示
        DisappearUI();

        // 結果演出を再生
        ShowScan();
        ShowStar(score);
        // if (score > 0) ShowPaperShower();

        // アニメーション終了を待つ
        await UniTask.Delay(2500);
    }

    /// <summary>
    /// スキャンアニメーションを再生
    /// </summary>
    void ShowScan()
    {
        // スキャン演出のエフェクトを生成
        // GameObject scanLight = Instantiate(scanLightPrefab, transform.position, Quaternion.identity);
        // scanLight.transform.SetParent(result, false);
        // scanLight.transform.localPosition = new Vector3(0f, -350f);

        // 解答と正答の親を取得
        Transform answerLayersParent = answerLayers.parent;
        Transform correctLayersParent = correctLayers.parent;

        // 比較配置に移動
        Sequence layersSequence = DOTween.Sequence();
        layersSequence
            .Append(answerLayersParent.DOScale(90f, 0.5f))
            .Join(answerLayersParent.DOLocalMove(new Vector3(250f, 50f), 0.5f))
            .Join(correctLayersParent.DOScale(1f, 0.5f))
            .Join(correctLayersParent.DOLocalMove(new Vector3(-2.6f, 1.4f, 7.7f), 0.5f));
        layersSequence.Play();

        // スキャンエフェクト
        // Sequence scanSequence = DOTween.Sequence();
        // scanSequence
        //     .SetDelay(0.55f)
        //     .AppendCallback(() => scanLight.SetActive(true))
        //     .Append(scanLight.transform.DOLocalMove(new Vector3(0f, 350f), 1f))
        //     .OnComplete(() => scanLight.SetActive(false));
        // scanSequence.Play();
    }

    /// <summary>
    /// スター評価を表示
    /// </summary>
    void ShowStar(int score)
    {
        // スターのリストを作成
        List<GameObject> starList = new List<GameObject>();
        foreach (Transform star in starBox.transform)
        {
            starList.Add(star.gameObject);
        }

        // 正答率からスター数を決定
        int starNum = 0;
        if (0 < score & score < 50)
        {
            starNum = 1;
        }
        else if (50 <= score & score < 100)
        {
            starNum = 2;
        }
        else if (100 <= score)
        {
            starNum = 3;
        }
        
        // スターの準備
        Sequence starSequence = DOTween.Sequence().SetId("starSequence");
        starSequence.AppendCallback(() => starBox.SetActive(true))
            .SetDelay(1.5f)
            .Join(starBox.transform.DOScale(0f, 0f))
            .Append(starBox.transform.DOScale(1f, 0.3f));

        // for (int i = 0; i < starNum; i++)
        // {
        //     starSequence.AppendCallback(() => ServiceLocator.Get<AVPlayerService>().Star())
        //         .Append(starList[i].transform.DOScale(Vector3.one * 300f, 0f).SetDelay(0.05f)) // 星を大きくする
        //         .Join(starList[i].GetComponent<SpriteRenderer>().DOColor(Color.white, 0f))     // 同時に色を明るくする
        //         .Append(starList[i].transform.DOScale(100f, 0.3f).SetEase(Ease.OutBounce));    // 次にバウンドするように元の大きさに戻す
        // }

        // アニメーションを再生
        starSequence.Play();
    }

    // /// <summary>
    // /// 紙吹雪を出す
    // /// </summary>
    // void ShowPaperShower()
    // {
    //     // 紙吹雪エフェクトを生成
    //     GameObject paperShower = Instantiate(paperShowerPrefab, transform.position, Quaternion.identity);
    //     paperShower.transform.SetParent(result, false);
    //     paperShower.transform.localPosition = new Vector3(0f, 0f);

    //     DOTween.TweensById("starSequence").ForEach((tween) => tween.OnComplete(() => { // 星のアニメーションの完了を取得
    //         paperShower.GetComponent<ParticleSystem>().Play(); // 紙吹雪を出す
    //         ServiceLocator.Get<AVPlayerService>().Complete();
    //     }));
    // }

}