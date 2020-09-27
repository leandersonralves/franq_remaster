using UnityEngine;

namespace Skills
{
    /// <summary>
    /// Classe para triggerar o Dash.
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(Cooldown))]
    public class Dash : MonoBehaviour, ICooldown
    {
        /// <summary>
        /// Instancia do Animator.
        /// </summary>
        [SerializeField]
        private Animator m_animator;

        bool ICooldown.OverTime { get { return false; } }

        [SerializeField]
        public float cooldownRequired;

        float ICooldown.CooldownRequired { get { return cooldownRequired; } }

        /// <summary>
        /// Instancia do Cooldown para checar se tem estamina.
        /// </summary>
        [SerializeField]
        private Cooldown cooldownManager;

        void FixedUpdate()
        {
            if (Input.GetKeyDown(AutoInput.Dash))
            {
                if (cooldownManager.Use(this as ICooldown))
                {
                    m_animator.SetTrigger(AnimatorParams.TRIGGER_DASH);
                }
            }
        }
    }
}