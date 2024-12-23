using System.Collections;
using System.Collections.Generic;
using Assets.BlockDestroy.Script.Sound;
using UnityEngine;
/// <summary>
/// ボールの制御
/// </summary>
public class Ball : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private Vector2 velocity;
    public float speedX = 0.0F;
    public float speedY = 0.0F;
    public static string gameState = "isGameStart";
    // Start is called before the first frame update
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        Invoke(nameof(GameStart), 0.5F); // 0.5秒後にゲームスタート
        gameState = "isGamePlaying";
    }

    private void FixedUpdate()
    {
        if (gameState != "isGamePlaying")
        {
            return;
        }

        rigidbody.velocity = velocity; // 速度を更新
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="collision">衝突オブジェクト</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WallX"))
        {
            // 壁にぶつかった場合移動方向を反転する
            velocity.x *= -1.0F;
            SEManager.seManager.PlaySE(SEType.CollisionWall);
        }

        if (collision.gameObject.CompareTag("WallY"))
        {
            // 壁にぶつかった場合移動方向を反転する
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
            SEManager.seManager.PlaySE(SEType.CollisionPlayer);
        }

        if (collision.gameObject.CompareTag("GameOver"))
        {
            // ゲームオーバー状態に変化する
            GameOver();
        }

        if (collision.gameObject.CompareTag("Block"))
        {
            SEManager.seManager.PlaySE(SEType.CollisionBlock);
            Destroy(collision.gameObject);
            velocity.x *= -1.0F;
            velocity.y *= -1.0F;         
            GameObject[] blockObj = GameObject.FindGameObjectsWithTag("Block");
            // Debug.Log(blockObj.Length);
            if (blockObj.Length <= 1)
            {
                GameClear();
            }
        }
    }

    /// <summary>
    /// ゲームスタート
    /// </summary>
    private void GameStart()
    {
        float random = Random.Range(-1.0F, 1.0F);
        if (random < 0)
        {
            speedX *= -1.0F; // 速度Xを反転
        }
        velocity = new Vector2(speedX, speedY); // ボールの速度を初期化
    }

    /// <summary>
    /// ゲームクリア
    /// </summary>
    private void GameClear()
    {
        gameState = "isGameClear";
        GameStop();
    }

    /// <summary>
    /// ゲームオーバー
    /// </summary>
    private void GameOver()
    {
        gameState = "isGameOver";
        GameStop();
    }

    /// <summary>
    /// ゲーム停止
    /// </summary>
    private void GameStop()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.zero;
    }
}
