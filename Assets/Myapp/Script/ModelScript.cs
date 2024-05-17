using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelScript : MonoBehaviour
{
    public float delay = 2.0f; // 待機時間（秒）
    private float initialZ; // 初期Z座標
    void Start()
    {
        // モデルの初期Z座標を保存
        initialZ = transform.position.z;

        // 指定した時間待機してからFollowCameraメソッドを呼び出す
        StartCoroutine(StartFollowingAfterDelay());
    }

    IEnumerator StartFollowingAfterDelay()
    {
        // 指定された待機時間分待機
        yield return new WaitForSeconds(delay);

        // Updateメソッドを実行してカメラの位置を追従するようにする
        StartFollowingCamera();
    }

    void StartFollowingCamera()
    {
        // UpdateメソッドをUpdateCameraPositionメソッドに差し替える
        this.InvokeRepeating("UpdateCameraPosition", 0.0f, 0.01f); // 0.01秒間隔で呼び出し
    }

    void UpdateCameraPosition()
    {
        // オブジェクトの位置をカメラの位置に追従させ、Z座標は初期値に固定
        //このコードはデバイスのX座標で移動するのでその場でスマホを回転させても追従しない
        gameObject.transform.position = new Vector3(Camera.main.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);


    }
}
