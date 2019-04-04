using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class AbilityNpcCommander: AbilityBase
    {
        [SerializeField] private GameObject _targetAimObject = null;
        [SerializeField] private GameObject _fireSocket = null;
        [SerializeField] private float _fillAmountSpeed = 8.0f;

        public override Abilities Ability => Abilities.NPC_Commander;

        private NPCCommand _currentNpcControlled = null;
        private NPCCommand _currentCommandInProgress = null;
        private float _currentFilledAmount = 0.0f;

        private void Update()
        {
            RaycastHit hit;
            var direction = (PlayerEyes.GetWorldLocation() - _fireSocket.transform.position).normalized;
            if (Physics.Raycast(new Ray(_fireSocket.transform.position, direction), out hit))
            {
                _targetAimObject.transform.position = hit.point + hit.normal * 0.001f;
                _targetAimObject.transform.rotation = Quaternion.LookRotation(hit.normal);

                var command = hit.collider.GetComponentInParent<NPCCommand>();

                if (_currentCommandInProgress && command != _currentCommandInProgress)
                {
                    _currentCommandInProgress.SetFillAmount(0.0f);
                    _currentFilledAmount = 0.0f;
                }

                if (command)
                {
                    _currentFilledAmount = Mathf.MoveTowards(_currentFilledAmount, 1.0f, Time.deltaTime * _fillAmountSpeed);
                    command.SetFillAmount(_currentFilledAmount);
                    _currentCommandInProgress = command;
                }

                if (Math.Abs(_currentFilledAmount - 1.0f) < float.Epsilon)
                {
                    _currentNpcControlled = command;
                    _currentFilledAmount = 0.0f;
                    _currentNpcControlled.Command();
                }
            }
        }
        
    }
}
