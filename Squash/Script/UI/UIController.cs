using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// UIの制御とスコア計算
/// </summary>
public class UIController : MonoBehaviour
{
    private float stageScore;
    public GameObject state; // ゲーム状態画像（デフォルトはゲームスタート画像）
    public Sprite isGameOver; // ゲームオーバ状態の画像データ
    public GameObject scoreText;   // スコアテキスト
    public GameObject buttonPanel; // ボタンパネル

    // Start is called before the first frame update
    private void Start()
    {
        Invoke(nameof(HiddenImage), 0.5F); // 0.5秒後にゲーム状態画像を非表示にする。
        HiddenButtonPanel();
        stageScore = 0.0F;
        UpdateScore();
    }

    // Update is called once per frame
    private void Update()
    {
        if (BallController.gameState == "isGamePlaying")
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            PlayerController player = playerObj.GetComponent<PlayerController>();
            float playerScore = player.GetScore(); // スコア計算のためプレイヤーが取得したスコアを取得する
            if (playerScore != 0)
            {
                // 取得したスコアが0でない場合のみに取得したスコアに応じてステージスコアを計算する
                stageScore += playerScore; // 
                player.ResetScore(); // 加重計算を防ぐためスコアをリセットする。
                UpdateScore();
            }
        }
        else if (BallController.gameState == "isGameOver")
        {
            DisplayImage(); // ゲーム状態画像を表示にする
            stageScore = 0;
            state.GetComponent<Image>().sprite = isGameOver; // ゲーム状態画像をゲームオーバー画像に切り替える。
            buttonPanel.SetActive(true); // ボタンパネルを表示
            BallController.gameState = "isGameEnd";
        }
    }

    /// <summary>
    /// ゲーム状態画像を非表示にする
    /// </summary>
    private void HiddenImage()
    {
        state.SetActive(false);
    }

    /// <summary>
    /// ゲーム状態画像を表示にする
    /// </summary>
    private void DisplayImage()
    {
        state.SetActive(true);
    }

    /// <summary>
    /// ボタンパネルを非表示
    /// </summary>
    private void HiddenButtonPanel()
    {
        buttonPanel.SetActive(false);
    }

    /// <summary>
    /// スコアを更新する。
    /// </summary>
    private void UpdateScore()
    {
        float score = stageScore;
        scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
    
    /// <summary>
    /// 現在のスコアを取得
    /// </summary>
    /// <returns></returns>
    public float GetStageScore(){
        return stageScore;
    }
}
