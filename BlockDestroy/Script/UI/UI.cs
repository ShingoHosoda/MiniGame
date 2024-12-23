using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.BlockDestroy.Script.UI
{
    /// <summary>
    /// UI関係の処理
    /// </summary>
    public class UI : MonoBehaviour
    {
        public GameObject state; // ゲーム状態
        public Sprite isGameClear; // ゲームクリア画像
        public Sprite isGameOver;  // ゲームオーバー画像
        public GameObject buttonPanel; // ボタンパネル
        public GameObject replayButton; // リプレイボタン
        public GameObject retryButton;  // リトライボタン
        // Start is called before the first frame update
        private void Start()
        {
            Invoke(nameof(HiddenImage), 0.5F);
            HiddenButtonPanel();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Ball.gameState == "isGameClear")
            {
                // Debug.Log("GameClear");
                // ゲームクリア処理
                DisplayImage(isGameClear);
                DisplayButtonPanel();
                Button button = retryButton.GetComponent<Button>();
                button.interactable = false;
                Ball.gameState = "isGameEnd";
            }
            else if (Ball.gameState == "isGameOver")
            {
                // Debug.Log("GameOver");
                // ゲームオーバー処理
                DisplayImage(isGameOver);
                DisplayButtonPanel();
                Button button = replayButton.GetComponent<Button>();
                button.interactable = false;
                Ball.gameState = "isGameEnd";
            }
        }

        /// <summary>
        /// 画像の表示
        /// </summary>
        /// <param name="sprite">表示する画像データ</param>
        private void DisplayImage(Sprite sprite)
        {
            state.SetActive(true);
            state.GetComponent<Image>().sprite = sprite;
        }

        /// <summary>
        /// 画像を非表示にする
        /// </summary>
        private void HiddenImage()
        {
            state.SetActive(false);
        }

        /// <summary>
        /// ボタンパネルを表示
        /// </summary>
        private void DisplayButtonPanel()
        {
            buttonPanel.SetActive(true);
        }

        /// <summary>
        /// ボタンパネル非表示
        /// </summary>
        private void HiddenButtonPanel()
        {
            buttonPanel.SetActive(false);
        }
    }

}
