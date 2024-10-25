using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using GameStates;

public class SceneLoadManager_test : MonoBehaviour
{

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public IEnumerator GameResultLoader(gameresults isWin) 
    { 
        if (isWin == gameresults.win)
        {
            yield return SceneLoader(scenenames_test.endingwin_test);
        }
        else if (isWin == gameresults.lose)
        {
            yield return SceneLoader(scenenames_test.endinglose_test);
        }
    }
        
    // �C�ӂ̃V�[�������[�h����֐�
    public IEnumerator SceneLoader(string sceneName)
    {
        // �񓯊��ŃV�[�������[�h����
        UnityEngine.AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // ���[�h����������܂őҋ@
        while (!asyncLoad.isDone)
        {
            // ���[�h�̐i�s�󋵂��m�F����UI�ɔ��f�����邱�Ƃ��ł���
            UnityEngine.Debug.Log(asyncLoad.progress);
            yield return null;  // ���̃t���[���܂ő҂�
        }

    }
}
