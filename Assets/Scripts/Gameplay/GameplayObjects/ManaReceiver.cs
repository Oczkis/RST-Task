using System;
using System.Collections;
using System.Collections.Generic;
using Unity.BossRoom.Gameplay.GameplayObjects.Character;
using Unity.Netcode;
using UnityEngine;


namespace Unity.BossRoom.Gameplay.GameplayObjects
{
    public class ManaReceiver : NetworkBehaviour, IManable
    {
        public event Action<ServerCharacter, int> ManaReceived;

        [SerializeField]
        NetworkLifeState m_NetworkLifeState;

        public void ReceiveMana(ServerCharacter inflicter, int HP)
        {
            if (IsManable())
            {
                ManaReceived?.Invoke(inflicter, HP);
            }
        }

        public bool IsManable()
        {
            return m_NetworkLifeState.LifeState.Value == LifeState.Alive;
        }
    }
}
