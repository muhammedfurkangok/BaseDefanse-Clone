using UnityEngine;

namespace Runtime.Managers
{

    public class PlayerHealthManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        public int health = 100;

        #endregion

        #region Serialized Variables

        [SerializeField] private UIManager uiManager;
        #endregion

        #endregion
    }
}