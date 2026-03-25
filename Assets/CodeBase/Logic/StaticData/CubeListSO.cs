using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Logic.StaticData
{
    [CreateAssetMenu(fileName = "CubeListSO", menuName = "Data/CubeListSO")]
    public class CubeListSO : ScriptableObject
    {
        [field: SerializeField] public List<Sprite> Cubes { get; private set; }
    }
}