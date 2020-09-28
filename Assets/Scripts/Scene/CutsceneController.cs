using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe para controle do estado das cutscenes.
/// </summary>
public class CutsceneController : SceneController
{
    /// <summary>
    /// Cache para indicar se proxima cena esta carregando.
    /// </summary>
    private bool hasLoading = false;
    void Update()
    {
        if (Input.anyKeyDown)
        {
            hasLoading = true;
            LoadScene(nextScene);
        }
    }
}
