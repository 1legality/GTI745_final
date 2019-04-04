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
        
        private NPCCommandable _commandInProgress = null;
        private float _progressAmount = 0.0f;

        private void Update()
        {
            RaycastHit hit;
            var direction = (PlayerEyes.GetWorldLocation() - _fireSocket.transform.position).normalized;
            if (Physics.Raycast(new Ray(_fireSocket.transform.position, direction), out hit))
            {
                _targetAimObject.transform.position = hit.point + hit.normal * 0.001f;
                _targetAimObject.transform.rotation = Quaternion.LookRotation(hit.normal);

                var command = hit.collider.GetComponentInParent<NPCCommandable>();

                if (_commandInProgress && command != _commandInProgress)
                {
                    _commandInProgress.SetFillAmount(0.0f);
                    _progressAmount = 0.0f;
                }

                if (command)
                {
                    _progressAmount = Mathf.MoveTowards(_progressAmount, 1.0f, Time.deltaTime * _fillAmountSpeed);
                    command.SetFillAmount(_progressAmount);
                    _commandInProgress = command;
                }

                if (Math.Abs(_progressAmount - 1.0f) < float.Epsilon)
                    CommandManager.Instance.CompleteProgress(_commandInProgress);
            }
        }
    }
}
