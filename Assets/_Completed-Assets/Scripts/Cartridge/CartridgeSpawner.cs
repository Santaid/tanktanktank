using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Complete; // Complete名前空間をインポート
public class CartridgeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject CartridgePrehabs;//ShellCartridgeプレハブ 
    [SerializeField] private float interval = 20;//出現する間隔
   // private Complete.GameManager gameManager; // GameManagerへの参照を保持
    // Start is called before the first frame update
    void Start()
    {   
    //    gameManager = FindObjectOfType<Complete.GameManager>();
    //    Debug.Log(gameManager);//何故見つからない？
        Complete.GameManager.OnGameStateChanged += HandleGameStateChanged;// OnGameStateChangedイベントにHandleGameStateChangedを登録  
    }

    public IEnumerator SpawnRoutine()//弾の箱を出現させるコルーチン
    {
        while (true)
        {
            SpawnCartridge();
            yield return new WaitForSeconds(interval);
        }
    }
    private void SpawnCartridge()//弾の箱を出現させるメソッド
    {
        Instantiate(CartridgePrehabs, new Vector3(Random.Range(-20.0f, 20.0f),10.0f,Random.Range(-10.0f, 25.0f)), Quaternion.identity); 
    }

    private void HandleGameStateChanged(Complete.GameManager.GameState newState)//ゲームイベントを受け取った時に実行する
    {
        //Debug.Log("Gamemode "+newState);
        if(newState == Complete.GameManager.GameState.RoundPlaying)
        {
            StartCoroutine(SpawnRoutine());
        }
        else StopCoroutine(SpawnRoutine());
    }    
}
