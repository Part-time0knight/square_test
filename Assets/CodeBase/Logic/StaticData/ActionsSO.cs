using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.StaticData
{
    [CreateAssetMenu(fileName = "ActionsSO", menuName = "Data/ActionsSO")]
    public class ActionsSO : ScriptableObject
    {
        [field: SerializeField]
        public List<ActionStruct> Actions { get; private set; }

        [Serializable]
        public struct ActionStruct
        {
            [field: SerializeField]
            public string String { get; private set; }

            [field: SerializeField]
            public ActionEnum Action { get; private set; }
        }
    }
}