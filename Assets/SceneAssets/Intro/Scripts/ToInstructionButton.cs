using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics;
using GameStates;

public class ToInstructionButton : MonoBehaviour
{
    public void LoadInstructionScene()
    {
        SceneManager.LoadSceneAsync(scenenames.instruction);
    }
}
