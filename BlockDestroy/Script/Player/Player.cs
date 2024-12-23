using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    private float axisH; // 水平方向 [-1, 1]
    public float speedX = 0.0F; // 移動速度
    // Start is called before the first frame update
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        axisH = 1.0F;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Ball.gameState != "isGamePlaying")
        {
            return;
        }

        axisH = Input.GetAxisRaw("Horizontal");
        if (axisH > 0.0F)
        {
            // 右方向
            transform.localScale = new Vector2(1.0F, 1.0F);
        }
        else
        {
            // 左方向
            transform.localScale = new Vector2(-1.0F, 1.0F);
        }
    }

    private void FixedUpdate()
    {
        if (Ball.gameState != "isGamePlaying")
        {
            return;
        }

        // 速度を更新 右方向1.0 左方向-1.0
        rigidbody.velocity = new Vector2(axisH * speedX, rigidbody.velocity.y);
    }
}
