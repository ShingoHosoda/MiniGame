using Assets.Squash.Script.Sound;
using UnityEngine;
/// <summary>
/// プレイヤー制御
/// プレイヤの操作を行う
/// </summary>
public class PlayerController : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private float score;  // プレイヤーのスコア
    private float axisH; // キャラクターの向き -> 水平方向
    public float speedX = 1.0F; // 移動速度

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        axisH = 0.0F; // 水平方向
        InitScore(0);
    }

    // Update is called once per frame
    private void Update()
    {
        if (BallController.gameState != "isGamePlaying")
        {
            return;
        }

        // プレイキャラクターの向きを制御
        axisH = Input.GetAxisRaw("Horizontal"); // AキーとDキーの入力を受付
        if (axisH > 0.0F)
        {
            // axisHが1の場合右向きになる。
            transform.localScale = new Vector2(1.0F, 1.0F);
        }
        else
        {
            // axisHが-1の場合左向きになる。
            transform.localScale = new Vector2(-1.0F, 1.0F);
        }
    }

    private void FixedUpdate()
    {
        if (BallController.gameState != "isGamePlaying")
        {
            return;
        }

        rigidbody.velocity = new Vector2(axisH * speedX, rigidbody.velocity.y);
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="collision">衝突オブジェクト</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // ボールに衝突した場合ボールのスコアを取得する。
            GameObject ballObject = GameObject.FindGameObjectWithTag("Ball");
            BallController ballController = ballObject.GetComponent<BallController>();
            score = ballController.GetScore(); // ボールのスコアを取得
            SEManager.seManager.PlaySE(SEType.CollisionPlayer);
        }
    }

    /// <summary>
    /// プレイヤーが取得したスコアを初期化
    /// </summary>
    /// <param name="initScore">初期化するスコア</param>
    private void InitScore(int initScore)
    {
        score = initScore;
    }

    /// <summary>
    /// プレイヤーが取得したスコア。
    /// </summary>
    /// <returns>プレイヤーが取得したスコアを返す。</returns>
    public float GetScore()
    {
        return score;
    }

    /// <summary>
    /// プレイヤーが取得したスコアを0にリセットする（加重計算を防ぐため）
    /// </summary>
    public void ResetScore()
    {
        score = 0.0F;
    }
}
