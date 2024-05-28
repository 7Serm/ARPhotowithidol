using UnityEngine;


public class ModelScript : MonoBehaviour
{

    private GameObject _emptyObject; // 空のオブジェクトを参照
    private float _maxDistance = 0.5f; // モデルが空オブジェクトから離れる最大距離
    private float _speed = 5f; // モデルが空オブジェクトに向かう速度
    private InstiatePrehab instiatePrehab;
    GameObject gmb;
    void Start()
    {
        GameObject gmb = GameObject.Find("Launcher");
        instiatePrehab = gmb.GetComponent<InstiatePrehab>();
        _emptyObject = instiatePrehab.EmptyObjPrehab;
        //↑モデル移動関数で使用
    }


    void Update()
    {
        MoveModel();

    }





    //モデル移動関数
    //カメラ本体の移動があればモデルも追従するがカメラ回転には合わせて移動しない
    private void MoveModel()
    {
        if (instiatePrehab != null && _emptyObject != null)
        {
            if (instiatePrehab != null && _emptyObject != null)
            {
                // X軸とZ軸の距離を計算
                float distanceX = Mathf.Abs(transform.position.x - _emptyObject.transform.position.x);
                float distanceZ = Mathf.Abs(transform.position.z - _emptyObject.transform.position.z);

                // X軸またはZ軸の距離が_maxDistanceを超えている場合
                if (distanceX > _maxDistance || distanceZ > _maxDistance)
                {
                    // モデルを空オブジェクトの位置に向かってX軸とZ軸方向に移動
                    float step = _speed * Time.deltaTime;
                    float targetX = Mathf.MoveTowards(transform.position.x, _emptyObject.transform.position.x, step);
                    float targetZ = Mathf.MoveTowards(transform.position.z, _emptyObject.transform.position.z, step);
                    transform.position = new Vector3(targetX, transform.position.y, targetZ);
                }
            }
        }
    }
}
