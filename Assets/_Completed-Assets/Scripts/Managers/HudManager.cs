using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Complete;// Complete名前空間をインポート(現状意味なし)

public class HudManager : MonoBehaviour
{

    [SerializeField] GameObject Player1StockArea;
    [SerializeField] GameObject Player2StockArea;
    private Complete.GameManager gamemanager;
    // Start is called before the first frame update
    void Start()
    {   
        gamemanager = FindObjectOfType<Complete.GameManager>();
        gamemanager.OnGameStateChanged += HandleGameStateChanged;// OnGameStateChangedイベントにHandleGameStateChangedを登録
        for (int i = 0; i < gamemanager.m_Tanks.Length;i++)
        {
        gamemanager.m_Tanks[i].OnWeaponStockChanged += HandleWeaponStockChanged;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void HandleGameStateChanged(Complete.GameManager.GameState newState)
    {
        //Debug.Log("Gamemode "+newState);
        if (newState == Complete.GameManager.GameState.RoundPlaying)
        {
            Player1StockArea.SetActive(true);
            Player2StockArea.SetActive(true);
        }
        else
        {
            Player1StockArea.SetActive(false);
            Player2StockArea.SetActive(false);
        }           
    }
    private void HandleWeaponStockChanged(int m_NowBullets, int m_PlayerNumber)
    {
        
        //Debug.Log(m_NowBullets + " bullets left" + m_PlayerNumber);
        if (m_PlayerNumber == 1)
        {
            PlayerStockArea stockarea = Player1StockArea.GetComponent<PlayerStockArea>();
            stockarea.UpdatePlayerStockArea(m_NowBullets);
        } 
        else if (m_PlayerNumber == 2)
        {
            PlayerStockArea stockarea = Player2StockArea.GetComponent<PlayerStockArea>();
            stockarea.UpdatePlayerStockArea(m_NowBullets);
        }
    }
}
