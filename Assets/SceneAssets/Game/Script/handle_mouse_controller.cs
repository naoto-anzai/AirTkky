using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handle_cup_mouse : MonoBehaviour
{
    //座標用の変数
    Vector3 mousePos, worldPos;
    //y座標固定のための定数
    const float const_y = 0.33F;
    //座標補整のための定数
    const int conv_corr = 2;

    void Update()
    {
        //マウス座標の取得
        mousePos = Input.mousePosition;
        //スクリーン座標をワールド座標に変換
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        //ワールド座標を自身の座標に設定
        transform.position = new Vector3(worldPos.x/conv_corr,const_y, worldPos.z/conv_corr);
    }
}