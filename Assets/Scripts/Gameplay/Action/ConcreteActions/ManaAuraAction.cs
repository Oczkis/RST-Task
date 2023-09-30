using System.Collections;
using System.Collections.Generic;
using Unity.BossRoom.Gameplay.GameplayObjects;
using Unity.BossRoom.Gameplay.GameplayObjects.Character;
using UnityEngine;

namespace Unity.BossRoom.Gameplay.Actions
{
    [CreateAssetMenu(menuName = "BossRoom/Actions/ManaAura Action")]
    public class ManaAuraAction : AuraAction
    {
        protected override void CharacterReceiveAura(ServerCharacter clientCharacter)
        {
            if (clientCharacter.TryGetComponent(out IManable mana))
                mana.ReceiveMana(clientCharacter, Config.Amount);
        }
    }
}
