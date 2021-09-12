using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace SerializedDictionary
{

    [CustomEditor(typeof(SerializableDictionary))]
    public class DictionaryEditor : Editor
    {

        #region Constants

        private GUILayoutOption _numberColumnWidthOption = GUILayout.Width(60);
        private GUILayoutOption _keyColumnWidthOption = GUILayout.Width(100);
        private GUILayoutOption _valueColumnWidthOption = GUILayout.Width(200);
        private GUILayoutOption _deleteColumnWidthOption = GUILayout.Width(60);

        #endregion

        #region Fields

        private SerializableDictionary _serializedDictionaryWrapper;
        private Dictionary<int, string> _serializableDictionary;

        private bool _addingItemsGroup;
        private bool _observingItemsGroup;

        private int _currentKey;
        private string _currentValue;

        #endregion

        #region Events

        public void OnEnable()
        {

            _serializedDictionaryWrapper = target as SerializableDictionary;

            if(_serializedDictionaryWrapper.items == null)
            {

                _serializedDictionaryWrapper.items = new Dictionary<int, string>();

                if(_serializedDictionaryWrapper.itemsSaveData == null)
                {

                    _serializedDictionaryWrapper.itemsSaveData = new List<DictionaryData>();

                };

                for(int i = 0; i < _serializedDictionaryWrapper.itemsSaveData.Count; i++)
                {

                    DictionaryData keyValuePair = _serializedDictionaryWrapper.itemsSaveData[i];

                    _serializedDictionaryWrapper.items.Add(keyValuePair.Key, keyValuePair.Value);

                };

            }

            _serializableDictionary = _serializedDictionaryWrapper.items;

            _addingItemsGroup       = true;
            _observingItemsGroup    = true;

        }

        public override void OnInspectorGUI()
        {

            CreateAddingItemsGroup();

            CreateClearButton();

            CreateItemsGroup();

            if (GUI.changed) SetSceneDirty();

        }

        #endregion

        #region Methods

        private void CreateAddingItemsGroup()
        {

            _addingItemsGroup = EditorGUILayout.BeginFoldoutHeaderGroup(_addingItemsGroup, "Add item");

            if (!_addingItemsGroup)
            {

                EditorGUILayout.EndFoldoutHeaderGroup();

                return;

            };

            _currentKey     = EditorGUILayout.IntField("Key", _currentKey);
            _currentValue   = EditorGUILayout.TextField("Value", _currentValue);

            if (!GUILayout.Button("Add"))
            {

                EditorGUILayout.EndFoldoutHeaderGroup();

                return;

            };

            if (_serializableDictionary.ContainsKey(_currentKey))
            {

                Debug.LogError($"Dictionary already has value with \"{_currentKey}\" key!");

                EditorGUILayout.EndFoldoutHeaderGroup();

                return;

            }

            _serializableDictionary.Add(_currentKey, _currentValue);

            SetSceneDirty();

            EditorGUILayout.EndFoldoutHeaderGroup();

        }

        private void CreateItemsGroup()
        {

            _observingItemsGroup = EditorGUILayout.BeginFoldoutHeaderGroup(_observingItemsGroup, "Items");

            if (!_observingItemsGroup
                    || _serializableDictionary.Count == 0)
            {

                EditorGUILayout.EndFoldoutHeaderGroup();

                return;

            };

            int itemOrderNumber = 0;

            EditorGUILayout.BeginHorizontal("box");

            EditorGUILayout.LabelField("№", _numberColumnWidthOption);
            EditorGUILayout.LabelField("Key", _keyColumnWidthOption);
            EditorGUILayout.LabelField("Value");
            EditorGUILayout.LabelField("Delete", _deleteColumnWidthOption);

            EditorGUILayout.EndHorizontal();
            
            foreach (var dictionaryItem in _serializableDictionary.ToArray())
            {

                EditorGUILayout.BeginHorizontal("box");

                itemOrderNumber++;

                EditorGUILayout.LabelField($"Item {itemOrderNumber}", _numberColumnWidthOption);
                
                EditorGUILayout.LabelField($"{dictionaryItem.Key}", _keyColumnWidthOption);

                _serializableDictionary[dictionaryItem.Key] = EditorGUILayout.TextArea(dictionaryItem.Value);

                if(GUILayout.Button("X", _deleteColumnWidthOption))
                {

                    _serializableDictionary.Remove(dictionaryItem.Key);

                };

                EditorGUILayout.EndHorizontal();

            }

            EditorGUILayout.EndFoldoutHeaderGroup();
            
        }

        private void CreateClearButton()
        {

            if (_serializableDictionary.Count == 0) return;

            if (!GUILayout.Button("Clear")) return;

            _serializableDictionary.Clear();

        }

        private void SetSceneDirty()
        {

            EditorUtility.SetDirty(_serializedDictionaryWrapper.gameObject);
            EditorSceneManager.MarkSceneDirty(_serializedDictionaryWrapper.gameObject.scene);

            _serializedDictionaryWrapper.itemsSaveData.Clear();

            foreach (var dictionaryItem in _serializableDictionary.ToArray())
            {

                _serializedDictionaryWrapper.itemsSaveData.Add(new DictionaryData() { Key = dictionaryItem.Key, Value = dictionaryItem.Value });

            }

        }

        #endregion

    }

}
