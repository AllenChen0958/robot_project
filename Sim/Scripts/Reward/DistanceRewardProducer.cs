using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GymUnity3D
{
    public class DistanceRewardProducer : RewardProducer
    {
        [SerializeField]
        private Transform m_Target;

        [SerializeField]
        private Transform m_Source;

        public override float GetReward()
        {
            float dis = Vector3.Distance(m_Source.position, m_Target.position);
            return dis;
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
         
        }
    }

}