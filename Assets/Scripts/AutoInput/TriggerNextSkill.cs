using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Altera a proxima Skill a ser aprendida.
/// </summary>
public class TriggerNextSkill : MonoBehaviour
{
    /// <summary>
    /// Instancia do AutoInput para definir proxima Skill a ser aprendida.
    /// </summary>
    private AutoInput autoInput;

    /// <summary>
    /// Define a skill que sera aprendidade no momento que o Player entrar no Trigger.
    /// </summary>
    [SerializeField]
    private AutoInput.Skills nextSkill;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        autoInput = GetComponentInParent<AutoInput>();
    }

    /// <summary>
    /// OnTriggerEnter2D is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            autoInput.currentSkillLearning = nextSkill;
        }
    }
}
