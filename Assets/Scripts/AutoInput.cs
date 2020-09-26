using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que controla os Input do Jogador automaticamente conforme aprende a jogar.
/// Conforme o Tutorial ocorre, as configuracoes de keys sao aplicadas em runtime.
/// Enquanto esse Componente estiver ativo no Update() o Input sempre será redefinido.
/// </summary>
public class AutoInput : MonoBehaviour
{
    /// <summary>
    /// Estado atual da aprendizagem.
    /// </summary>
    private enum Skills
    {
        Walk,
        Jump,
        Melt,
        FriendlyBubble,
        Punch,
        Defense,
        None
    }

    /// <summary>
    /// Habilidade atual em aprendizado.
    /// </summary>
    private static Skills currentSkillLearning = Skills.None;

    /// <summary>
    /// String para consulta do eixo conforme os botoes A,D.
    /// </summary>
    private const string HORIZONTAL_AD = "HorizontalAD";

    /// <summary>
    /// String para consulta de eixo conforme os botoes de setas.
    /// </summary>
    private const string HORIZONTAL_ARROW = "HorizontalArrow";

    /// <summary>
    /// Atual String usada para consulta do eixo de leitura.
    /// </summary>
    private static string KeyHorizontal = string.Empty;

    /// <summary>
    /// Valor atual do eixo Horizontal do Input.
    /// Caso o jogador nao tenha aprendido, retorna 0.
    /// </summary>
    /// <returns>Valor de itnervalo [-1,1] -1 para esquerda, 1 para direita.</returns>
    public static float Horizontal { get { return string.IsNullOrEmpty(KeyHorizontal) ? 0 : Input.GetAxis(KeyHorizontal); } }

    /// <summary>
    /// KeyCode de Pulo.
    /// </summary>
    public static KeyCode Jump = KeyCode.None;

    /// <summary>
    /// KeyCode de Liquefazer (Agachar).
    /// </summary>
    public static KeyCode Melt = KeyCode.None;

    /// <summary>
    /// KeyCode de soco.
    /// </summary>
    public static KeyCode Punch = KeyCode.None;

    /// <summary>
    /// KeyCode de Defesa.
    /// </summary>
    public static KeyCode Defense = KeyCode.None;

    /// <summary>
    /// KeyCode de Defesa.
    /// </summary>
    public static KeyCode FriendlyBubble = KeyCode.None;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        #if TEST_DEAFULTBUTTONS
        KeyHorizontal = HORIZONTAL_ARROW;
        Jump = KeyCode.Space;
        Melt = KeyCode.DownArrow;
        Punch = KeyCode.F;
        Defense = KeyCode.D;
        FriendlyBubble = KeyCode.S;
        #endif
    }

    #if !TEST_DEAFULTBUTTONS
    void Update()
    {
        if (currentSkillLearning == Skills.None) return;

        switch (currentSkillLearning)
        {
            case Skills.Walk:
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) 
                    KeyHorizontal = HORIZONTAL_ARROW;

                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) 
                    KeyHorizontal = HORIZONTAL_AD;
                break;

            case Skills.Jump:
                if (!Event.current.isKey) { break; }
                if (KeyAllowed(Event.current.keyCode)) { Jump = Event.current.keyCode; }
                break;

            case Skills.Melt:
                if (!Event.current.isKey) { break; }
                if (KeyAllowed(Event.current.keyCode)) { Jump = Event.current.keyCode; }
                break;

            case Skills.FriendlyBubble:
                if (!Event.current.isKey) { break; }
                if (KeyAllowed(Event.current.keyCode)) { Melt = Event.current.keyCode; }
                break;

            case Skills.Punch:
                if (!Event.current.isKey) { break; }
                if (KeyAllowed(Event.current.keyCode)) { Jump = Event.current.keyCode; }
                break;

            case Skills.Defense:
                if (!Event.current.isKey) { break; }
                if (KeyAllowed(Event.current.keyCode)) { Jump = Event.current.keyCode; }
                break;
        }
    }
    #endif

    /// <summary>
    /// Funcao para checar se KeyCode e permitido para uso.
    /// Teclas de F1-F12, teclas especiais e as ja utilizadas nao permitidas.
    /// </summary>
    /// <param name="keyCode"></param>
    /// <returns></returns>
    private bool KeyAllowed (KeyCode keyCode)
    {
        return  (keyCode > KeyCode.BackQuote && keyCode < KeyCode.LeftCurlyBracket) || //Alphabeto permitido.
                (keyCode > KeyCode.ScrollLock && keyCode < KeyCode.RightCommand) || //Ctrl, Alt e Shift permitido.
                (keyCode > KeyCode.Slash && keyCode < KeyCode.Colon) || //Alphanumerico permitido.
                (keyCode > KeyCode.Delete && keyCode < KeyCode.KeypadEnter) || //Teclado numerico permitido.
                keyCode == KeyCode.UpArrow || keyCode == KeyCode.DownArrow || //Setas cima e baixo.
                (keyCode != Jump && keyCode != Melt && keyCode != Punch && keyCode != Defense && keyCode != FriendlyBubble); //Setas ja em uso.
    }
}
