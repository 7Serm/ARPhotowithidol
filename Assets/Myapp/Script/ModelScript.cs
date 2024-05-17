using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelScript : MonoBehaviour
{
    public float delay = 2.0f; // �ҋ@���ԁi�b�j
    private float initialZ; // ����Z���W
    void Start()
    {
        // ���f���̏���Z���W��ۑ�
        initialZ = transform.position.z;

        // �w�肵�����ԑҋ@���Ă���FollowCamera���\�b�h���Ăяo��
        StartCoroutine(StartFollowingAfterDelay());
    }

    IEnumerator StartFollowingAfterDelay()
    {
        // �w�肳�ꂽ�ҋ@���ԕ��ҋ@
        yield return new WaitForSeconds(delay);

        // Update���\�b�h�����s���ăJ�����̈ʒu��Ǐ]����悤�ɂ���
        StartFollowingCamera();
    }

    void StartFollowingCamera()
    {
        // Update���\�b�h��UpdateCameraPosition���\�b�h�ɍ����ւ���
        this.InvokeRepeating("UpdateCameraPosition", 0.0f, 0.01f); // 0.01�b�Ԋu�ŌĂяo��
    }

    void UpdateCameraPosition()
    {
        // �I�u�W�F�N�g�̈ʒu���J�����̈ʒu�ɒǏ]�����AZ���W�͏����l�ɌŒ�
        //���̃R�[�h�̓f�o�C�X��X���W�ňړ�����̂ł��̏�ŃX�}�z����]�����Ă��Ǐ]���Ȃ�
        gameObject.transform.position = new Vector3(Camera.main.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);


    }
}
