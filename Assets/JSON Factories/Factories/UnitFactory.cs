using UnityEngine;
using Interfaces;

namespace Factories
{
    public abstract class UnitFactory : IUnitFactory
    {

        #region Interfaces Methods

        public abstract GameObject GetUnit(string health);

        #endregion

    }

}
