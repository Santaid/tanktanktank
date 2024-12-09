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
    public void UpdatePlayerStockArea(int shellstocks){//ストック数の表示
        for (int a = 0; a < 10; a++)
        {
            if (a < shellstocks % 10)
            {
                shell1[a].enabled = true;
            }
            else shell1[a].enabled = false;
        }

        for (int a = 0;a < 5;a++)
        {
            if (a < shellstocks / 10)
            {
                shell10[a].enabled = true;
            }
            else shell10[a].enabled = false; 
        }
    }
}
