using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kobuki
{
    [RequireComponent(typeof(VehicleController))]
    public class VehicleNetworkAdapter : FloatVectorDataListener
    {
        private VehicleController m_VehicleController;

        void Start()
        {
            m_VehicleController = GetComponent<VehicleController>();
        }

        public override void OnReceiveFloatVector(ref float[] data)
        {
			print (data);
			/*if (data.Length >= 2) {
                m_VehicleController.SetVelocity(data[0], data[1]);
            }*/
			if (data.Length >= 1) {
				m_VehicleController.SetVelocity(data[0]);
			}
        }
    }
}
