using Assets.Squash.Script.Sound;
using UnityEngine;
/// <summary>
/// BallController
/// </summary>
[RequireComponent(typeof(BallScore))]
public class BallController : MonoBehaviour
{
    private BallScore score; // 基本スコア
    private new Rigidbody2D rigidbody; // Rigidbody2D
    private Vector2 velocity; // 速度
    public static string gameState = "isGameStart"; // ゲーム状態
    // Start is called before the first frame update
    private void Start()
    {
        score = GetComponent<BallScore>();
        rigidbody = GetComponent<Rigidbody2D>();
        Invoke(nameof(GameStateStart), 0.5F); // 0.5秒後にスタート
    }

    private void FixedUpdate()
    {

        if (gameState != "isGamePlaying")
        {
            return;
        }

        rigidbody.velocity = velocity; // 速度の更新
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="collision">衝突オブジェクト</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WallX"))
        {
            // ぶつかった場合速度を反転する
            velocity.x *= -1.0F;

            SEManager.seManager.PlaySE(SEType.CollisionWall);
        }

        if (collision.gameObject.CompareTag("WallY"))
        {
            // ぶつかった場合速度を反転する
            velocity.y *= -1.0F;

            SEManager.seManager.PlaySE(SEType.CollisionWall);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーにぶつかった場合移動方向を反転する
            float random = Random.Range(-2.0F, 2.0F);
            if (random < 0)
            {
                velocity.x *= -1;
            }
            else
            {
                velocity.x *= 1;
            }
            velocity.y *= -1;
            SpeedUp(); // ボールがプレイヤーにぶつかったタイミングで速度計算を行う。
        }

        if (collision.gameObject.CompareTag("GameOver"))
        {
            GameStateOver(); // ゲームオーバー状態にする
        }
    }

    /// <summary>
    /// ゲームスタート
    /// </summary>
    private void GameStateStart()
    {
        gameState = "isGamePlaying";
        velocity = new Vector2(2.0F, 2.0F); // ボールの速度を初期化
    }

    /// <summary>
    /// ゲームオーバー状態に変更します。
    /// </summary>
    private void GameStateOver()
    {
        gameState = "isGameOver";
        GameStop();
    }

    /// <summary>
    /// ゲームを停止します。
    /// </summary>
    private void GameStop()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.zero;
    }

    /// <summary>
    /// プレイヤーと衝突した場合取得したスコアに応じてボールの速度が変わります。
    /// スコアは今まで取得したスコアから比較する必要があります。
    /// </summary>
    private void SpeedUp()
    {
        GameObject UIObject = GameObject.FindGameObjectWithTag("UI");
        UIController controller = UIObject.GetComponent<UIController>();
        float nowScore = controller.GetStageScore(); // 今まで取得したすべてのスコアを取得
        if (nowScore >= 0.0F && nowScore < 50.0F)
        {
            velocity = new Vector2(2.0F, 2.0F);
        }
        else if (nowScore >= 50.0F && nowScore < 100.0F)
        {
            velocity = new Vector2(3.0F, 3.0F);
        }
        else if (nowScore >= 100.0F && nowScore < 150.0F)
        {
            velocity = new Vector2(4.0F, 4.0F);
        }
        else if (nowScore >= 150.0F && nowScore < 200.0F)
        {
            velocity = new Vector2(3.5F, 3.5F);
        }
        else if (nowScore >= 200.0F && nowScore < 250.0F)
        {
            velocity = new Vector2(5.0F, 5.0F);
        }
    }

    /// <summary>
    /// スコアを取得
    /// </summary>
    /// <returns>取得したスコアを返す</returns>
    public float GetScore()
    {
        return score.CalcScore(velocity);
    }
}
