using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handle_cup_mouse : MonoBehaviour
{
    //���W�p�̕ϐ�
    Vector3 mousePos, worldPos;
    //y���W�Œ�̂��߂̒萔
    const float const_y = 0.33F;
    //���W�␮�̂��߂̒萔
    const int conv_corr = 2;

    void Update()
    {
        //�}�E�X���W�̎擾
        mousePos = Input.mousePosition;
        //�X�N���[�����W�����[���h���W�ɕϊ�
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        //���[���h���W�����g�̍��W�ɐݒ�
        transform.position = new Vector3(worldPos.x/conv_corr,const_y, worldPos.z/conv_corr);
    }
}