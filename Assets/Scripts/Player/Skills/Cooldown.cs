using UnityEngine;

namespace Skills
{
    /// <summary>
    /// Controlador de estamina.
    /// </summary>
    public class Cooldown : MonoBehaviour
    {
        /// <summary>
        /// Maxima estamina do Cooldown.
        /// </summary>
        [SerializeField]
        private float maxCooldown = 100f;

        /// <summary>
        /// Valor maximo de Cooldown apenas pra consulta.true
        /// </summary>
        public float MaxCooldown { get { return maxCooldown; } }

        private float availableCooldown = 100f;

        /// <summary>
        /// Quantidade atual de estamina.
        /// </summary>
        /// <value></value>
        public float AvailableCooldown { get { return availableCooldown; } private set { availableCooldown = value; } }

        /// <summary>
        /// Usar cooldown.
        /// </summary>
        /// <param name="cooldownSkill">Interface da habilidade que deseja implementar o cooldown</param>
        /// <returns>Retorna True se houver, False caso contrario.</returns>
        public bool Use (ICooldown cooldownSkill)
        {
            /// Caso a habilidade use o cooldown sobre o tempo.
            var cd = cooldownSkill.OverTime ? cooldownSkill.CooldownRequired * Time.deltaTime : cooldownSkill.CooldownRequired;
            if (AvailableCooldown >= cd)
            {
                AvailableCooldown -= cd;
                currentDelay = 0f;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Atraso iniciar a recuperacao do Cooldown.
        /// </summary>
        [SerializeField]
        private float delayRecovery = 3f;

        /// <summary>
        /// Atraso ja passado para recuperar.
        /// </summary>
        private float currentDelay = 0f;

        /// <summary>
        /// Taxa em segundos de recuperacao do Cooldown.
        /// </summary>
        private float recoveryRate = 25f;

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            /// Inicia a recuperacao do Cooldown na taxa do recoveryRate apos passar o atraso de delayRecovery.
            if (availableCooldown < maxCooldown)
            {
                currentDelay += Time.deltaTime;
                if (currentDelay > delayRecovery)
                {
                    availableCooldown += Time.deltaTime * recoveryRate;
                    if (availableCooldown > maxCooldown)
                        availableCooldown = maxCooldown;
                }
            }
        }
    }

    /// <summary>
    /// Interface necessaria que as habilidades implementem.
    /// </summary>
    public interface ICooldown
    {
        /// <summary>
        /// A estamina e usada conforme o tempo?
        /// </summary>
        /// <value>True: conforme o tempo de uso, False: uma vez por uso.</value>
        bool OverTime { get; }

        /// <summary>
        /// Estamina necessaria para uso da habilidade.
        /// Sera estamina por segundo, se overTime for verdadeiro.
        /// </summary>
        /// <value></value>
        float CooldownRequired { get; }
    }
}