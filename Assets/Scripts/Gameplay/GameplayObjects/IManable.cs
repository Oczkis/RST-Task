using System;
using System.Collections;
using System.Collections.Generic;
using Unity.BossRoom.Gameplay.GameplayObjects.Character;
using UnityEngine;

namespace Unity.BossRoom.Gameplay.GameplayObjects
{
    public interface IManable
    {
        void ReceiveMana(ServerCharacter sender, int MP);

        ulong NetworkObjectId { get; }

        Transform transform { get; }

        bool IsManable();
    }

}
