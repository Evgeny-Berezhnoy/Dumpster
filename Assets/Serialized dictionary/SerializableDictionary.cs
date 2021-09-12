using System;
using System.Collections.Generic;
using UnityEngine;


namespace SerializedDictionary
{

    [Serializable]
    public class SerializableDictionary : MonoBehaviour
    {

        public Dictionary<int, string> items;
        [SerializeField] public List<DictionaryData> itemsSaveData; 
        
    }

    [Serializable]
    public class DictionaryData
    {

        #region Fields

        public int Key;
        public string Value;

        #endregion

    }

}