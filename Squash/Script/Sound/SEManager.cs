using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Squash.Script.Sound
{
    public enum SEType
    {
        CollisionWall,  // 壁に衝突
        CollisionPlayer, // プレイヤーに衝突 
    }

    /// <summary>
    /// 効果音の制御
    /// </summary>
    public class SEManager : MonoBehaviour
    {
        private AudioSource audioSource;
        public AudioClip collisionWall;
        public AudioClip collisionPlayer;
        // public AudioClip collsiionBlock;
        public static SEManager seManager;
        // Start is called before the first frame update
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.volume = 0.5F;
            if (seManager == null)
            {
                seManager = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlaySE(SEType type)
        {
            if (type == SEType.CollisionWall)
            {
                // 壁との衝突音
                GetComponent<AudioSource>().PlayOneShot(collisionWall);
            }
            else if (type == SEType.CollisionPlayer)
            {
                // プレイヤーとの衝突音
                GetComponent<AudioSource>().PlayOneShot(collisionPlayer);
            }
        }
    }

}