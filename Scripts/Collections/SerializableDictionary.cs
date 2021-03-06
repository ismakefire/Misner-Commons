﻿using UnityEngine;
using System.Collections.Generic;
using System;

/*
* Original author user "christophfranke123" from Unity Answers
* Unity Answers Profile: http://answers.unity3d.com/users/456555/christophfranke123.html
* 
* A serializable dictionary class.
*/
namespace christophfranke123
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        #region Variables
        [SerializeField]
        private List<TKey> keys = new List<TKey>();
        
        [SerializeField]
        private List<TValue> values = new List<TValue>();
        #endregion


        #region ISerializationCallbackReceiver
        /// <summary>
        /// Save the dictionary to lists.
        /// </summary>
        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }
        
        /// <summary>
        /// Load dictionary from lists.
        /// </summary>
        public void OnAfterDeserialize()
        {
            this.Clear();
            
            if (keys.Count != values.Count)
            {
                throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable.", keys.Count, values.Count));
            }
            
            for (int i = 0; i < keys.Count; i++)
            {
                this.Add(keys[i], values[i]);
            }
        }
        #endregion
    }
}
