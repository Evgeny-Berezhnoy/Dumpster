using UnityEngine;
using Constants;
using Views;

namespace Factories
{
    public class MageFactory : UnitFactory
    {

        #region Interfaces Methods

        public override GameObject GetUnit(string health)
        {

            GameObject unit = new GameObject(UnitNames.MAGE);

            MageView view = unit.AddComponent<MageView>();

            if (int.TryParse(health, out var unitHealth))
            {

                view.Health = unitHealth;

            }

            return unit;

        }

        #endregion

    }

}
