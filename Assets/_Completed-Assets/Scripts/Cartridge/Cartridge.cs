using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cartridge : MonoBehaviour
{
    [SerializeField] private Renderer ShellCartridge;//カートリッジのゲームオブジェクトの参照

    [SerializeField] private float cycle = 1;//明滅するサイクルの秒数

    [SerializeField] private float destroylimit = 15;//カートリッジが消滅するまでの時間
    
    [SerializeField] private float flickerlimit = 10;//明滅し始めるまでの時間　destroylimitより短く
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, destroylimit);        
    }

    // Update is called once per frame
    void Update()
    {
     // 内部時刻を経過させる
        time += Time.deltaTime;
        if (time > flickerlimit)
        {
            // 周期cycleで繰り返す値の取得
            // 0～cycleの範囲の値が得られる
            var repeatValue = Mathf.Repeat(time, cycle);

            // 内部時刻timeにおける明滅状態を反映
            ShellCartridge.enabled = repeatValue >= cycle * 0.5f;
        }   
    }
}
