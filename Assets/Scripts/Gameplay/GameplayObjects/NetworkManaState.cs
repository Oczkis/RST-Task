using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Unity.BossRoom.Gameplay.GameplayObjects
{
    public class NetworkManaState : NetworkBehaviour
    {
        [HideInInspector]
        public NetworkVariable<int> ManaPoints = new NetworkVariable<int>();

        public event Action ManaPointsReplenished;

        public event Action ManaPointsDepleted;

        void OnEnable()
        {
            ManaPoints.OnValueChanged += ManaPointsChanged;
        }

        void OnDisable()
        {
            ManaPoints.OnValueChanged -= ManaPointsChanged;
        }

        void ManaPointsChanged(int previousValue, int newValue)
        {
            if (previousValue > 0 && newValue <= 0)
            {
                ManaPointsDepleted?.Invoke();
            }
            else if (previousValue <= 0 && newValue > 0)
            {
                ManaPointsDepleted?.Invoke();
            }
        }
    }
}
