using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GymUnity3D
{
    public class RewardFilter : RewardProducer
    {
        public enum FilterType
        {
            ONE_OVER_X
        }

        [SerializeField]
        private FilterType m_FilterType;

        [SerializeField]
        private RewardProducer m_Source;

        private float filter(float r)
        {
            switch (m_FilterType)
            {
                case FilterType.ONE_OVER_X:
                    return 1.0f / (r + 1e-8f);
                default:
                    return r;
            }
        }

        public override float GetReward()
        {
            return filter(m_Source.GetReward());
        }

        public override byte[] GetState()
        {
            return m_Source.GetState();
        }

        public override int GetStateSize()
        {
            return m_Source.GetStateSize();
        }

        public override void Reset()
        {
            m_Source.Reset();
        }
    }

}