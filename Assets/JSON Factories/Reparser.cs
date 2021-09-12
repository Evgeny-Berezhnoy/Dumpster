using System.Collections.Generic;
using UnityEngine;
using Constants;
using Factories;
using Interfaces;
using Models;

namespace Junk
{

    public class Reparser : MonoBehaviour
    {

        #region

        private Dictionary<string, IUnitFactory> _factories = new Dictionary<string, IUnitFactory>();

        #endregion

        #region Events

        public void Start()
        {

            _factories.Add(UnitNames.MAGE, new MageFactory());
            _factories.Add(UnitNames.INFANTRY, new InfantryFactory());

            Read();

        }

        #endregion

        #region Methods

        private void Read()
        {

            string text = Resources.Load<TextAsset>("Units description").text;

            text = "{\"units\" : " + text + "}";
            
            var unitsCollection = JsonUtility.FromJson<UnitsCollection>(text);

            if (unitsCollection == null
                    || unitsCollection.units == null)
            {

                return;

            }

            var units = unitsCollection.units;

            for (int i = 0; i < units.Count; i++)
            {

                UnitInstance unitInstance = units[i];

                if (unitInstance == null) continue;

                UnitData unitData = unitInstance.unit;

                if (unitData == null) continue;

                if(unitData.type == UnitNames.INFANTRY)
                {

                    _factories[UnitNames.INFANTRY].GetUnit(unitData.health);

                }
                else if(unitData.type == UnitNames.MAGE)
                {

                    _factories[UnitNames.MAGE].GetUnit(unitData.health);
                    
                };

            };

        }

        #endregion

    }

}