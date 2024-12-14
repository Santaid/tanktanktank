using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;//画像を入れるため
public class PlayerStockArea : MonoBehaviour
{
    [SerializeField] private Image[] shell1 = new Image[10];
    [SerializeField] private Image[] shell10 = new Image[4];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdatePlayerStockArea(int shellstocks)//ストック数の表示
    {
        Debug.Log($"{shellstocks} stocks");
        int onesplace = (shellstocks % 10 != 0 || shellstocks == 0) ? shellstocks % 10: 10;//一の位　shellstocksを10で割った余り、ただし0以外で余りが0なら10にする
        for (int a = 0; a < 10; a++)
        {
            if (a < onesplace)
            {
                shell1[a].enabled = true;
            }
            else shell1[a].enabled = false;
        }

        for (int a = 0;a < 4;a++)
        {
            if (a < (shellstocks-onesplace) / 10)
            {
                shell10[a].enabled = true;
            }
            else shell10[a].enabled = false; 
        }
    }
}
