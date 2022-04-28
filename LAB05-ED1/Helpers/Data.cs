using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary;

namespace LAB05_ED1.Helpers
{
    public class Data
    {
        //Singleton
        private static Data _instance = null;
        public static Data Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Data();
                }
                return _instance;
            }
        }

        public static Func<Vehicle, Vehicle, int> Comparer = (vehicle_A, vehicle_B) =>
        {
            return vehicle_A.LicensePlate.CompareTo(vehicle_B.LicensePlate);
        };

        public TwoThreeTree<Vehicle> VehicleTree = new TwoThreeTree<Vehicle>(Comparer);
    }
}
