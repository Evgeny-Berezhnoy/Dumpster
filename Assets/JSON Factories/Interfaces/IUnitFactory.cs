using UnityEngine;

namespace Interfaces
{
    public interface IUnitFactory
    {

        #region Methods

        GameObject GetUnit(string health);

        #endregion

    }

}
