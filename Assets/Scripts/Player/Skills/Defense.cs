using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skills
{
    /// <summary>
    /// Classe para triggerar o Dash.
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(Cooldown))]
    public class Defense : MonoBehaviour, ICooldown
    {
        /// <summary>
        /// Instancia do Animator.
        /// </summary>
        [SerializeField]
        private Animator m_animator;

        bool ICooldown.OverTime { get { return true; } }

        [SerializeField]
        public float cooldownRequired = 5f;

        float ICooldown.CooldownRequired { get { return cooldownRequired; } }

        /// <summary>
        /// Instancia do Cooldown para checar se tem estamina.
        /// </summary>
        [SerializeField]
        private Cooldown cooldownManager;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(AutoInput.Defense) && cooldownManager.Use(this as ICooldown))
            {
                m_animator.SetBool(AnimatorParams.BOOL_SHIELD, true);
            }
            else
            {
                m_animator.SetBool(AnimatorParams.BOOL_SHIELD, false);
            }
        }
    }
}
