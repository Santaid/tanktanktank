﻿using UnityEngine;
using UnityEngine.UI;
using System;//新たにインポート
namespace Complete
{
    public class TankShooting : MonoBehaviour
    {
        public int m_PlayerNumber = 1;              // Used to identify the different players.
        public Rigidbody m_Shell;                   // Prefab of the shell.
        public Transform m_FireTransform;           // A child of the tank where the shells are spawned.
        public Slider m_AimSlider;                  // A child of the tank that displays the current launch force.
        public AudioSource m_ShootingAudio;         // Reference to the audio source used to play the shooting audio. NB: different to the movement audio source.
        public AudioClip m_ChargingClip;            // Audio that plays when each shot is charging up.
        public AudioClip m_FireClip;                // Audio that plays when each shot is fired.
        public float m_MinLaunchForce = 15f;        // The force given to the shell if the fire button is not held.
        public float m_MaxLaunchForce = 30f;        // The force given to the shell if the fire button is held for the max charge time.
        public float m_MaxChargeTime = 0.75f;       // How long the shell can charge for before it is fired at max force.


        private string m_FireButton;                // The input axis that is used for launching shells.
        private float m_CurrentLaunchForce;         // The force that will be given to the shell when the fire button is released.
        private float m_ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
        private bool m_Fired;                       // Whether or not the shell has been launched with this button press.

        private const int m_FirstBullets = 10;//初期状態の弾数
        private int m_NowBullets;//現在の弾数
        private const int m_MaxBullets = 50;//持てる弾数の最大値
        private const int m_RefillBullets = 10;//補充される弾数
        private bool arrowstretch = true;//飛距離ゲージが伸びているならtrue、縮むならfalse
        public event Action<int> OnShellStockChanged;//弾数が変化した際のイベント

        private void OnEnable()
        {
            // When the tank is turned on, reset the launch force and the UI
            m_CurrentLaunchForce = m_MinLaunchForce;
            m_AimSlider.value = m_MinLaunchForce;
        }


        private void Start ()
        {
            // The fire axis is based on the player number.
            m_FireButton = "Fire" + m_PlayerNumber;

            // The rate that the launch force charges up is the range of possible forces by the max charge time.
            m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;

            m_NowBullets = m_FirstBullets;//現在の弾数の再設定
            OnShellStockChanged?.Invoke(m_NowBullets);//弾数が変化した際のイベントを実行
        }


        private void Update ()
        {
            //Debug.Log(m_NowBullets);//現在の弾数の確認用
            //0以下なら何もせず返す
            if (m_NowBullets <= 0) 
            {
                m_NowBullets = 0;
                return;
            }
        //    Debug.Log($"{arrowstretch},{m_PlayerNumber},{m_CurrentLaunchForce}");
            // The slider should have a default value of the minimum launch force.
            m_AimSlider.value = m_MinLaunchForce;

            // If the max force has been exceeded and the shell hasn't yet been launched...
            if (m_CurrentLaunchForce > m_MaxLaunchForce && !m_Fired)//>=を>に変更
            {
                // ... use the max force and launch the shell.
                m_CurrentLaunchForce = m_MaxLaunchForce;
                //Fire ();
                arrowstretch = false;//縮める
            }
            else if (m_CurrentLaunchForce < m_MinLaunchForce && !m_Fired)
            {
                m_CurrentLaunchForce = m_MinLaunchForce;
                arrowstretch = true;//伸ばす　   
            }
            // Otherwise, if the fire button has just started being pressed...
            else if (Input.GetButtonDown (m_FireButton))
            {
                // ... reset the fired flag and reset the launch force.
                m_Fired = false;
                arrowstretch = true;//伸ばし始める
                m_CurrentLaunchForce = m_MinLaunchForce;

                // Change the clip to the charging clip and start it playing.
                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.Play ();
            }
            // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
            else if (Input.GetButton (m_FireButton) && !m_Fired)
            {
                if (arrowstretch == true)//arrowstretch がtrueなら伸ばす
                {
                    // Increment the launch force and update the slider.
                    m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
                }
                else if (arrowstretch == false)//縮み始めたら減らす
                {
                    m_CurrentLaunchForce -= m_ChargeSpeed * Time.deltaTime;
                } 

                m_AimSlider.value = m_CurrentLaunchForce;
            }
            // Otherwise, if the fire button is released and the shell hasn't been launched yet...
            else if (Input.GetButtonUp (m_FireButton) && !m_Fired)
            {
                // ... launch the shell.
                Fire ();
            }
        }


        private void Fire ()
        {
            //１度撃つごとに弾丸を１減らす
            m_NowBullets--;
            OnShellStockChanged?.Invoke(m_NowBullets);//弾数が変化した際のイベントを実行
            // Set the fired flag so only Fire is only called once.
            m_Fired = true;

            // Create an instance of the shell and store a reference to it's rigidbody.
            Rigidbody shellInstance =
                Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward; 

            // Change the clip to the firing clip and play it.
            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play ();

            // Reset the launch force.  This is a precaution in case of missing button events.
            m_CurrentLaunchForce = m_MinLaunchForce;
        }


        private void Refill()//弾丸を補充した際のメソッド
        {
            m_NowBullets += m_RefillBullets;
            if (m_NowBullets >= m_MaxBullets) 
            {
                m_NowBullets = m_MaxBullets;
            }
            OnShellStockChanged?.Invoke(m_NowBullets);//弾数が変化した際のイベントを実行
        }

        void OnCollisionEnter(Collision Cartridge)//カートリッジと衝突した際のメソッド
        {   
            if(Cartridge.gameObject.tag == "ShellCartridge")
            {
                Refill();
                Destroy(Cartridge.gameObject);
            }
        }
    
    }
}