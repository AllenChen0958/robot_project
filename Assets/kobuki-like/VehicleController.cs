using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kobuki
{
    public class VehicleController : MonoBehaviour
    {
        [SerializeField]
        private AxleInfo axleInfo; 
        [SerializeField]
        private float maxMotorTorque;

        [SerializeField]
        private float m_Linear;
        [SerializeField]
        private float m_Angular;

        private float m_LeftMotor;
        private float m_RightMotor;

        [SerializeField]
        private bool m_HumanControl = false;

        private int m_FrameCount = 0;

        [SerializeField]
        private Vector3 m_Vel;
        private Rigidbody m_Rigidbody;

        [SerializeField]
        private int m_FrameSkip = 5;

        public float GetMaxTorque()
        {
            return maxMotorTorque;
        }

        public void SetVelocity(float linear, float angular)
        {
            m_Linear = linear;
            m_Angular = angular;
            Vector2 motor = toMotor(m_Linear, m_Angular);
            setMotor(motor[0], motor[1]);
            m_FrameCount = Time.frameCount;
        }

		public void SetVelocity(float angular)
		{
			m_Linear = 0.7f;
			m_Angular = angular;
			Vector2 motor = toMotor(m_Linear, m_Angular);
			setMotor(motor[0], motor[1]);
			m_FrameCount = Time.frameCount;
		}

        void Awake()
        {
            string[] args = System.Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                if ("skip".Equals(args[i]))
                {
                    m_FrameSkip = int.Parse(args[i + 1]);
                }
            }
        }

        void Start()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (m_HumanControl) {
                SetVelocity(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
            }
            else
            {
                if (Time.frameCount - m_FrameCount > m_FrameSkip)
                {
                    m_LeftMotor = m_RightMotor = 0.0f;
                }
            }
        }

        public void FixedUpdate()
        {
            axleInfo.leftWheel.motorTorque = m_LeftMotor;
            axleInfo.rightWheel.motorTorque = m_RightMotor;
            m_Vel = m_Rigidbody.velocity;
        }

        private Vector2 toMotor(float linear, float angular)
        {
            float leftMotor = linear * maxMotorTorque;
            float rightMotor = linear * maxMotorTorque;

            if (angular != 0.0)
            {
                if (angular > 0)
                {
                    rightMotor = rightMotor * 0.2f + 0.8f * (1f - angular);
                }
                else if (angular < 0)
                {
                    leftMotor = leftMotor * 0.2f + 0.8f * (1f - Mathf.Abs(angular));
                }
            }

            return new Vector2(leftMotor, rightMotor);
        }

        private void setMotor(float l_motor, float r_motor)
        {
            m_LeftMotor = l_motor;
            m_RightMotor = r_motor;
            m_FrameCount = Time.frameCount;
        }

    }

    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
    }
}