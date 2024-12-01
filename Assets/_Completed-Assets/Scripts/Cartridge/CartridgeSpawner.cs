using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartridgeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject CartridgePrehabs;//ShellCartridgeプレハブ 
    [SerializeField] private float interval = 20;//出現する間隔
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCartridge());
    }
    private IEnumerator SpawnCartridge()
    {
        while (true)
        {
            Instantiate(CartridgePrehabs, new Vector3(Random.Range(-20.0f, 20.0f),10.0f,Random.Range(-10.0f, 25.0f)), Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
    }
}
