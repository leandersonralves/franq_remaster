using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe abstrata Utilitaria para carregamento de cenas.
/// </summary>
public class SceneController : MonoBehaviour
{
    /// <summary>
    /// Define qual a próxima cena, caso passe de fase.
    /// </summary>
    [SerializeField]
    protected string nextScene;

    /// <summary>
    /// Canvas da tela de Loading.
    /// </summary>
    [SerializeField]
    private Canvas canvasLoadScene;

    /// <summary>
    /// Recarrega a cena atual.
    /// </summary>
    public void ReloadScene ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Carrega uma cena do Unity com o nome especificado.
    /// </summary>
    public void LoadScene (string sceneName)
    {
        canvasLoadScene.enabled = true;

        var asyncLoadScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        // var asyncUnloadScene = SceneManager.UnloadSceneAsync(sceneName);
        // asyncUnloadScene.completed += (x) => {
        // };
    }

    /// <summary>
    /// Sai do Jogo.
    /// </summary>
    public void Quit ()
    {
        Application.Quit();
    }
}
