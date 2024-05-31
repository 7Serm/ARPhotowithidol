using UnityEngine;


public class ModelScript : MonoBehaviour
{

    private GameObject _emptyObject; // 空のオブジェクトを参照
    private float _maxDistance = 0f; // モデルが空オブジェクトから離れる最大距離
    private float _speed = 5f; // モデルが空オブジェクトに向かう速度
    private InstiatePrehab instiatePrehab;

    private Camera _mainCamera;
    void Start()
    {
        GameObject gmb = GameObject.Find("Launcher");
        instiatePrehab = gmb.GetComponent<InstiatePrehab>();
        _emptyObject = instiatePrehab.EmptyObjPrehab;
        //↑モデル移動関数で使用

        _mainCamera = Camera.main;
    }


    void Update()
    {
        MoveModel();
      //  ModelLookatCamera();
    }





    //モデル移動関数
    //カメラ本体の移動があればモデルも追従するがカメラ回転には合わせて移動しない
    private void MoveModel()
    {
        if (instiatePrehab != null && _emptyObject != null)
        {
            // X軸とZ軸の距離を計算
            float distanceX = Mathf.Abs(transform.position.x - _emptyObject.transform.position.x);
            float distanceZ = Mathf.Abs(transform.position.z - _emptyObject.transform.position.z);

            // X軸またはZ軸の距離が_maxDistanceを超えている場合
            if (distanceX > _maxDistance || distanceZ > _maxDistance)
            {
                // モデルを空オブジェクトの最新の位置に向かってX軸とZ軸方向に移動
                Vector3 targetPosition = new(_emptyObject.transform.position.x, transform.position.y, _emptyObject.transform.position.z);
                Vector3 direction = targetPosition - transform.position;

                // 回転を設定
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * Time.deltaTime);
                }

                // 移動を設定
                float step = _speed * Time.deltaTime;
                transform.position = Vector3.Slerp(transform.position, targetPosition, step);
            }
        }
    }


    private void ModelLookatCamera()
    {
        if (_mainCamera != null)
        {
            Vector3 direction = _mainCamera.transform.position - transform.position;
            direction.y = 0; // Y軸の回転を無視する場合はコメントを外す
            transform.rotation = Quaternion.LookRotation(direction);
        }
    } 
}