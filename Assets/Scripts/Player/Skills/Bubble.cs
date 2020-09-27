using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills
{
    /// <summary>
    /// Classe para triggerar o Dash.
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(Cooldown))]
    public class Bubble : MonoBehaviour, ICooldown
    {
        /// <summary>
        /// Instancia do Animator.
        /// </summary>
        [SerializeField]
        private Animator m_animator;

        bool ICooldown.OverTime { get { return false; } }

        [SerializeField]
        public float cooldownRequired = 10f;

        float ICooldown.CooldownRequired { get { return cooldownRequired; } }

        /// <summary>
        /// Instancia do Cooldown para checar se tem estamina.
        /// </summary>
        [SerializeField]
        private Cooldown cooldownManager;

        /// <summary>
        /// Instancia da Bubble usada.
        /// </summary>
        [SerializeField]
        private BubbleController bubble;

        void Update()
        {
            if (Input.GetKeyDown(AutoInput.FriendlyBubble))
            {
                if (cooldownManager.Use(this as ICooldown))
                {
                    m_animator.SetTrigger(AnimatorParams.TRIGGER_BUBBLING);
                    StopAllCoroutines();
                    StartCoroutine(EnableBubble());
                }
            }
        }

        /// <summary>
        /// Enumerator para ativar a bolha apos o tempo de animacao do Player.
        /// </summary>
        /// <returns></returns>
        IEnumerator EnableBubble ()
        {
            yield return new WaitForSeconds(0.6f);
            bubble.Create();
        }
    }
}
