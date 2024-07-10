using UnityEngine;
using UnityEngine.UI;


public class ModelScript : MonoBehaviour
{

    private GameObject _emptyObject; // �J�����̐�ɂ���ڕW�ƂȂ�I�u�W�F�N�g���Q��
    private float _maxDistance = 0.1f; // ���f�����ڕW�I�u�W�F�N�g���痣���ő勗��
    private float _speed = 5f; // ���f�����ڕW�I�u�W�F�N�g�Ɍ��������x
    private InstiatePrehab instiatePrehab;

    private Camera _mainCamera;�@//���C���J�����̎Q��
    private Animator _animator;�@//���f�������A�j���[�^�[�R���|�[�l���g�p

    private UIManager _uiManager;
    void Start()
    {
        GameObject gmb = GameObject.Find("Launcher");
        instiatePrehab = gmb.GetComponent<InstiatePrehab>();
        _emptyObject = instiatePrehab.EmptyObjPrehab;
        //�����f���ړ��֐��Ŏg�p

        GameObject _canvas = GameObject.Find("Canvas");
        _uiManager = _canvas.GetComponent<UIManager>();

        _mainCamera = Camera.main;
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (_uiManager.MoveActive)
        {
            MoveModel();
        }
        if (_uiManager.LookAtActive)
        {
            ModelLookatCamera();
        }
        //UpdateAnimatorParameters();
    }





    //���f���ړ��֐�
    //�ړ���������Ɍ������ĉ�]�܂ł���
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


    //�X�}�z�J�����̕�������Ɍ�������
    private void ModelLookatCamera()
    {
        {
            
        }
        if (_mainCamera != null)
        {
            Vector3 direction = _mainCamera.transform.position - transform.position;
            direction.y = 0; // Y���̉�]�𖳎�����ꍇ�̓R�����g���O��
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }


    //X�����ւ̍��������ꍇ�Ɉړ����邽�߂��̎��ɃA�j���[�V�����̕������Đ�����悤�J�ڂ�����
    private void UpdateAnimatorParameters()
    {
        if (_animator != null)
        {
            float distanceX = Mathf.Abs(transform.position.x - _emptyObject.transform.position.x);
            // �Ⴆ�΁A���f�����ړ����Ă��邩�ǂ����𔻒肵�ăA�j���[�^�[�ɓ`����
            if (distanceX > _maxDistance)
            {
                _animator.SetBool("isMoving", true);
            }
            else
            {
                _animator.SetBool("isMoving", false);
            }              
             
        }
    }

   
}