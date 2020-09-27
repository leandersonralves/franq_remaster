using UnityEngine;

namespace Skills
{
    /// <summary>
    /// Classe que triggera a skill Melt.
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(Cooldown))]
    public class Melt : MonoBehaviour, Skills.ICooldown
    {
        /// <summary>
        /// Instancia do Animator.
        /// </summary>
        [SerializeField]
        private Animator m_animator;

        bool ICooldown.OverTime { get { return true; } }

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
            if (Input.GetKey(AutoInput.Melt) && cooldownManager.Use(this as ICooldown))
            {
                m_animator.SetBool(AnimatorParams.BOOL_MELTING, true);
            }
            else
            {
                m_animator.SetBool(AnimatorParams.BOOL_MELTING, false);
            }
        }
    }
}