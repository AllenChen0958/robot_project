using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GymUnity3D
{
    public class DistanceDifferenceRewardProducer : RewardProducer
    {
        [SerializeField]
        private DistanceRewardProducer m_DistRewardProducer;

        private float m_LastDistance;

        public override float GetReward()
        {
            float distance = m_DistRewardProducer.GetReward();
            float diff = m_LastDistance - distance;
            m_LastDistance = distance;
            return diff;
        }

        public override byte[] GetState()
        {
            float r = GetReward();
            return BitConverter.GetBytes(r);
        }

        public override int GetStateSize()
        {
            return sizeof(float);
        }

        public override void Reset()
        {
            m_LastDistance = m_DistRewardProducer.GetReward();
        }

        void Start()
        {
            m_LastDistance = m_DistRewardProducer.GetReward();
        }

    }
}
