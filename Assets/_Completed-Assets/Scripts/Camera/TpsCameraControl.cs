using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsCameraControl : MonoBehaviour
{
    [HideInInspector] public Transform targetTank;// 追従対象のタンクのTransformコンポーネントを保持する変数
    [SerializeField] private Vector3 relativePosition;// カメラとタンクの相対的な位置を指定する変数
    [SerializeField] private Vector3 relativeRotation;// カメラとタンクの相対的な回転を指定する変数
    // Start is called before the first frame update
    void Start()
    {
       // distance = transform.position - targetTank.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
        if (targetTank != null)
        {
            transform.position = targetTank.position+ relativePosition;// カメラの位置を更新
            transform.rotation = Quaternion.Euler(relativeRotation) * targetTank.rotation;// カメラの回転を更新
           // transform.rotation = Quaternion.Euler(targetTank.rotation.x, relativeRotation.y*targetTank.rotation.y, targetTank.rotation.z);
        //transform.rotation = Quaternion.Euler(relativeRotation.x*targetTank.rotation.x,relativeRotation.y*targetTank.rotation.y, relativeRotation.z*targetTank.rotation.z);
        }
    }
}
