using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// スコア
/// </summary>
public class BallScore : MonoBehaviour
{
    public float score = 0; // 基本スコア
    /// <summary>
    /// ボールの速度に応じてスコアを計算して返します。
    /// </summary>
    /// <returns>スコアを返す</returns>
    public float CalcScore(Vector2 velocity)
    {
        // 難易度（ボールの速度変更）に応じて取得できるスコアを計算できるようにする。 
        float nowScore = score;
        if (velocity.x == 2.0F && velocity.y == 2.0F)
        {
            nowScore *= 1.0F;
        }
        else if (velocity.x == 3.0F && velocity.y == 3.0F)
        {
            nowScore *= 2.0F;
        }
        else if (velocity.x == 4.0F && velocity.y == 4.0F)
        {
            nowScore *= 3.0F;
        }
        else if (velocity.x == 3.5F && velocity.y == 3.5F)
        {
            nowScore *= 2.5F;
        }
        else if (velocity.x == 5.0F && velocity.y == 5.0F)
        {
            nowScore *= 4.0F;
        }
        return nowScore;
    }
}
