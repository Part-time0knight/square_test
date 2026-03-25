using Logic.StaticData;
using Logic.Zones;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Logic
{
    public class ActionText : MonoBehaviour
    {
        private ActionsSO _actionsData;
        private HoleZone _holeZone;
        private OutOfZones _outOfZones;
        private TowerZone _towerZone;
        private TopBorderZone _topBorderZone;

        private Dictionary<ActionEnum, string> _actions = new();


        [SerializeField] private TMP_Text _actionText;

        [Inject]
        private void Construct(ActionsSO actionsData,
            HoleZone holeZone,
            OutOfZones outOfZones,
            TowerZone towerZone,
            TopBorderZone topBorderZone)
        {
            _actionsData = actionsData;
            _holeZone = holeZone;
            _towerZone = towerZone;
            _topBorderZone = topBorderZone;
            _outOfZones = outOfZones;
            
            _holeZone.OnSet += InvokeThrow;
            _towerZone.OnSet += InvokeSet;
            _topBorderZone.OnSet += InvokeTopBorder;
            _outOfZones.OnSet += InvokeOutOfZones;

            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            foreach (var action in _actionsData.Actions) 
            {
                if (_actions.ContainsKey(action.Action)) continue;

                _actions.Add(action.Action, action.String);
            }
        }

        private void InvokeThrow(Cube cube)
        {
            if (!_actions.ContainsKey(ActionEnum.throwOut)) return;

            _actionText.text = _actions[ActionEnum.throwOut];
        }

        private void InvokeSet(Cube cube)
        {
            if (!_actions.ContainsKey(ActionEnum.Set)) return;

            _actionText.text = _actions[ActionEnum.Set];
        }

        private void InvokeTopBorder(Cube cube)
        {
            if (!_actions.ContainsKey(ActionEnum.TopBorder)) return;

            _actionText.text = _actions[ActionEnum.TopBorder];
        }

        private void InvokeOutOfZones()
        {
            if (!_actions.ContainsKey(ActionEnum.OutOfZones)) return;

            _actionText.text = _actions[ActionEnum.OutOfZones];
        }
    }
}