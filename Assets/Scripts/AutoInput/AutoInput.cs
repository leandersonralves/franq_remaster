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
    public enum Skills
    {
        Walk,
        Jump,
        Melt,
        FriendlyBubble,
        Dash,
        Defense,
        None
    }

    /// <summary>
    /// Habilidade atual em aprendizado.
    /// </summary>
    public Skills currentSkillLearning = Skills.None;

#region  CONSTANTES DO INPUT MANAGER DOS EIXOS
    /// <summary>
    /// String para consulta do eixo conforme os botoes A,D.
    /// </summary>
    private const string HORIZONTAL_AD = "HorizontalAD";

    /// <summary>
    /// String para consulta do eixo conforme os botoes A,D.
    /// </summary>
    private const string VERTICAL_WS = "VerticalWS";

    /// <summary>
    /// String para consulta de eixo conforme os botoes de setas.
    /// </summary>
    private const string HORIZONTAL_ARROW = "HorizontalArrow";

    /// <summary>
    /// String para consulta de eixo conforme os botoes de setas.
    /// </summary>
    private const string VERTICAL_ARROW = "VerticalArrow";
#endregion

    /// <summary>
    /// Atual String usada para consulta do eixo de leitura.
    /// </summary>
    private static string KeyHorizontal = string.Empty;

    /// <summary>
    /// Atual String usada para consulta do eixo de leitura.
    /// </summary>
    private static string KeyVertical = string.Empty;

    /// <summary>
    /// Valor atual do eixo Horizontal do Input.
    /// Caso o jogador nao tenha aprendido, retorna 0.
    /// </summary>
    /// <returns>Valor de itnervalo [-1,1] -1 para esquerda, 1 para direita.</returns>
    public static float Horizontal { get { return string.IsNullOrEmpty(KeyHorizontal) ? 0 : Input.GetAxis(KeyHorizontal); } }

    /// <summary>
    /// Valor atual do eixo Vertical do Input.
    /// Caso o jogador nao tenha aprendido, retorna 0.
    /// </summary>
    /// <returns>Valor de itnervalo [-1,1] -1 para esquerda, 1 para direita.</returns>
    public static float Vertical { get { return string.IsNullOrEmpty(KeyVertical) ? 0 : Input.GetAxis(KeyVertical); } }

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
    public static KeyCode Dash = KeyCode.None;

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
        KeyVertical = VERTICAL_ARROW;
        Jump = KeyCode.Space;
        Melt = KeyCode.DownArrow;
        Dash = KeyCode.F;
        Defense = KeyCode.D;
        FriendlyBubble = KeyCode.S;
        #endif
    }

    #if (!TEST_DEAFULTBUTTONS)
    void OnGUI()
    {
        if (currentSkillLearning == Skills.None) return;

        switch (currentSkillLearning)
        {
            case Skills.Walk:
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    KeyHorizontal = HORIZONTAL_ARROW;
                    KeyVertical = VERTICAL_ARROW;
                    currentSkillLearning = Skills.None;
                }

                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) 
                {
                    KeyHorizontal = HORIZONTAL_AD;
                    KeyVertical = VERTICAL_WS;
                    currentSkillLearning = Skills.None;
                }
                break;

            case Skills.Jump:
                Event c = Event.current;
                if (c == null || !c.isKey) { break; }
                if (KeyAllowed(c.keyCode))
                {
                    currentSkillLearning = Skills.None;
                    Jump = c.keyCode;
                }
                break;

            case Skills.Melt:
                if (!Event.current.isKey) { break; }
                if (KeyAllowed(Event.current.keyCode))
                {
                    currentSkillLearning = Skills.None;
                    Melt = Event.current.keyCode;
                }
                break;

            case Skills.FriendlyBubble:
                if (!Event.current.isKey) { break; }
                if (KeyAllowed(Event.current.keyCode))
                {
                    currentSkillLearning = Skills.None;
                    FriendlyBubble = Event.current.keyCode;
                }
                break;

            case Skills.Dash:
                if (!Event.current.isKey) { break; }
                if (KeyAllowed(Event.current.keyCode))
                {
                    currentSkillLearning = Skills.None;
                    Dash = Event.current.keyCode;
                }
                break;

            case Skills.Defense:
                if (!Event.current.isKey) { break; }
                if (KeyAllowed(Event.current.keyCode))
                {
                    currentSkillLearning = Skills.None;
                    Defense = Event.current.keyCode;
                }
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
        return  ((keyCode > KeyCode.BackQuote && keyCode < KeyCode.LeftCurlyBracket) || //Alphabeto permitido.
                (keyCode > KeyCode.ScrollLock && keyCode < KeyCode.RightCommand) || //Ctrl, Alt e Shift permitido.
                (keyCode > KeyCode.Slash && keyCode < KeyCode.Colon) || //Alphanumerico permitido.
                (keyCode > KeyCode.Delete && keyCode < KeyCode.KeypadEnter) || //Teclado numerico permitido.
                keyCode == KeyCode.Space) &&
                (keyCode != Jump && keyCode != Melt && keyCode != Dash && keyCode != Defense && keyCode != FriendlyBubble); //Setas ja em uso.
    }
}
