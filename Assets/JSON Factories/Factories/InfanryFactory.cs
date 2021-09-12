using UnityEngine;
using Constants;
using Views;

namespace Factories
{
    public class InfantryFactory : UnitFactory
    {

        #region Interfaces Methods

        public override GameObject GetUnit(string health)
        {

            GameObject unit = new GameObject(UnitNames.INFANTRY);

            InfantryView view = unit.AddComponent<InfantryView>();

            if (int.TryParse(health, out var unitHealth))
            {

                view.Health = unitHealth;

            }

            return unit;

        }

        #endregion

    }

}
