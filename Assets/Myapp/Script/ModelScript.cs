using UnityEngine;


public class ModelScript : MonoBehaviour
{

    private GameObject _emptyObject; // ��̃I�u�W�F�N�g���Q��
    private float _maxDistance = 0f; // ���f������I�u�W�F�N�g���痣���ő勗��
    private float _speed = 5f; // ���f������I�u�W�F�N�g�Ɍ��������x
    private InstiatePrehab instiatePrehab;

    private Camera _mainCamera;
    void Start()
    {
        GameObject gmb = GameObject.Find("Launcher");
        instiatePrehab = gmb.GetComponent<InstiatePrehab>();
        _emptyObject = instiatePrehab.EmptyObjPrehab;
        //�����f���ړ��֐��Ŏg�p

        _mainCamera = Camera.main;
    }


    void Update()
    {
        MoveModel();
      //  ModelLookatCamera();
    }





    //���f���ړ��֐�
    //�J�����{�̂̈ړ�������΃��f�����Ǐ]���邪�J������]�ɂ͍��킹�Ĉړ����Ȃ�
    private void MoveModel()
    {
        if (instiatePrehab != null && _emptyObject != null)
        {
            // X����Z���̋������v�Z
            float distanceX = Mathf.Abs(transform.position.x - _emptyObject.transform.position.x);
            float distanceZ = Mathf.Abs(transform.position.z - _emptyObject.transform.position.z);

            // X���܂���Z���̋�����_maxDistance�𒴂��Ă���ꍇ
            if (distanceX > _maxDistance || distanceZ > _maxDistance)
            {
                // ���f������I�u�W�F�N�g�̍ŐV�̈ʒu�Ɍ�������X����Z�������Ɉړ�
                Vector3 targetPosition = new(_emptyObject.transform.position.x, transform.position.y, _emptyObject.transform.position.z);
                Vector3 direction = targetPosition - transform.position;

                // ��]��ݒ�
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _speed * Time.deltaTime);
                }

                // �ړ���ݒ�
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
            direction.y = 0; // Y���̉�]�𖳎�����ꍇ�̓R�����g���O��
            transform.rotation = Quaternion.LookRotation(direction);
        }
    } 
}