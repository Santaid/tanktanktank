using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Complete;// Complete名前空間をインポート

public class HudManager : MonoBehaviour
{

    [SerializeField] GameObject Player1StockArea;
    [SerializeField] GameObject Player2StockArea;  
    // Start is called before the first frame update
    void Start()
    {
        Complete.GameManager.OnGameStateChanged += HandleGameStateChanged;// OnGameStateChangedイベントにHandleGameStateChangedを登録
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
}
