using UnityEngine;
using Interfaces;

namespace Views
{
    public class InfantryView : MonoBehaviour, IHealth
    {

        #region Fields

        [SerializeField] private int _health = 0;

        #endregion

        #region Interfaces Properties

        public int Health { get => _health; set => _health = value; }

        #endregion

    }

}
