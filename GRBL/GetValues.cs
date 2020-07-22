using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRBL
{
    public static class GetValues
    {
        /// <summary>
        /// Get ID of the GRBL setting
        /// </summary>
        /// <param name="rxData">rx data</param>
        /// <returns>GRBL Setting ID</returns>
        public static int GetDollarID(string rxData)
        {
            return int.Parse(rxData.Split('$', '=')[1]);
        }

        /// <summary>
        /// Get Value of the GRBL setting
        /// </summary>
        /// <param name="rxData">rx data</param>
        /// <returns>GRBL Setting Value</returns>
        public static float GetDollarValue(string rxData)
        {
            return float.Parse(rxData.Split('=')[1].Replace('.', ','));
        }

        /// <summary>
        /// Get new GRBL setting from rx data
        /// </summary>
        /// <param name="rxData">rx data</param>
        /// <returns>New GRBL Setting</returns>
        public static GRBLSetting GetSetting(string rxData)
        {
            return new GRBLSetting() { ID = GetDollarID(rxData), Value = GetDollarValue(rxData) };
        }
    }
}
