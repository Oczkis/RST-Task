using System.Collections;
using System.Collections.Generic;
using Unity.BossRoom.Gameplay.GameplayObjects;
using Unity.BossRoom.Gameplay.GameplayObjects.Character;
using UnityEngine;

namespace Unity.BossRoom.Gameplay.Actions
{
    public class AuraAction : Action
    {
        private float m_lastTickTime;
        private List<ServerCharacter> m_characterList = new();

        public override bool OnStart(ServerCharacter serverCharacter)
        {
            bool canceledActiveAura = serverCharacter.ActionPlayer.CancelRunningActionsByLogic(Config.Logic, true, this);

            if (canceledActiveAura)
            {
                return ActionConclusion.Stop;
            }
            else
            {
                OnAuraStart(serverCharacter);
                return ActionConclusion.Continue;
            }
        }

        public override bool OnUpdate(ServerCharacter clientCharacter)
        {
            if (!TickTimeInterval()) return true;

            ResetTimer();

            if (UpdateAuraTargets(clientCharacter))
                CharactersReceiveAura(clientCharacter);

            return true;
        }

        protected virtual void OnAuraStart(ServerCharacter serverCharacter)
        {
            ResetTimer();
        }

        protected virtual void CharacterReceiveAura(ServerCharacter clientCharacter) { }

        private bool UpdateAuraTargets(ServerCharacter clientCharacter)
        {
            m_characterList.Clear();

            var colliders = Physics.OverlapSphere(clientCharacter.physicsWrapper.Transform.position, Config.Radius, LayerMask.GetMask("PCs"));
            for (var i = 0; i < colliders.Length; i++)
            {
                var character = colliders[i].GetComponent<ServerCharacter>();
                if (character != null)
                    m_characterList.Add(character);
            }

            return m_characterList.Count > 0;
        }

        private void CharactersReceiveAura(ServerCharacter clientCharacter)
        {
            foreach (var character in m_characterList)
                CharacterReceiveAura(character);
        }

        private bool TickTimeInterval() => Time.time - m_lastTickTime > Config.TickIntervalSeconds;
        private void ResetTimer() => m_lastTickTime = Time.time;
    }
}

