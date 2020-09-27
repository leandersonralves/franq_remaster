using UnityEngine;

namespace Skills
{
    /// <summary>
    /// Controlador de estamina.
    /// </summary>
    public class Cooldown : MonoBehaviour
    {
        [SerializeField]
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
            Debug.Log(Time.deltaTime);
            if (AvailableCooldown >= cd)
            {
                AvailableCooldown -= cd;
                return true;
            }

            return false;
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