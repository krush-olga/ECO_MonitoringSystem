using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculations
{

    class EcologCalculation
    {
        private Dictionary<int, double> riskToK = new Dictionary<int, double>()
        {
            {1, 1.5 },
            {2, 1.3},
            {3, 1.04},
            {4, 0.85}
        };

        private Dictionary<int, double> riskToKn = new Dictionary<int, double>() // Block 1 dodatok 1
        {
            {1, 4},
            {2, 3},
            {3, 2.5},
            {4, 1.5}
        };

        private Dictionary<int, double> riskToKnv = new Dictionary<int, double>() // Block 1 dodatok 5
        {
            {1, 3},
            {2, 2},
            {3, 1.5},
            {4, 1}
        };

        private Dictionary<int, double> stupinZasmichToKzz = new Dictionary<int, double>() // Block 1 dodatok 5
        {
            {1, 1.25},
            {2, 1.5},
            {3, 2},
            {4, 2.5},
            {5, 3},
            {6, 4},
        };

        private Dictionary<double, double> depthToIndex = new Dictionary<double, double>() // Block 1 dodatok 3
        {
            {0.2, 0.1},
            {0.4, 0.082},
            {0.6, 0.07},
            {0.8, 0.06},
            {1, 0.054},
            {1.2, 0.049},
            {1.4, 0.044},
            {1.6, 0.04},
            {1.8, 0.037},
            {2, 0.037},
        };

        private double getNearest(double value, Dictionary<double, double> dict)
        {
            var keys = new List<double>(dict.Keys);
            var index = ~keys.BinarySearch(value);
            return dict[index];
        }

        public double IZA(double C, double gdk, int riskClass)
        {
            return Math.Pow(C / gdk, riskToK[riskClass]);
        }

        public double shkodaZabrZemel(/*double A = 0.5,*/ double Goz, double Pd, double Kz, int riskClass, double Keg)
        {
            return 0.5 * Goz * Pd * Kz * riskToKn[riskClass] * Keg;
        }

        public double koefZabrZemel(double Ozr, double Tz, double Pd, double depth)
        {
            return Ozr / (Tz * Pd * getNearest(depth, depthToIndex));
        }

        public double Ozr(double Vzr, double SHzr)
        {
            return Vzr / SHzr;
        }

        public double Kz(double Szr, double Gp, double Tzsh)
        {
            return Szr * Gp / (Tzsh * getNearest(Gp, depthToIndex) * 1000000);
        }

        public double zagalniiRozmirVidshkod(double[] Rsh)
        {
            var RshList = Rsh.ToList();
            var max = RshList.Max();
            RshList.Remove(max);

            return max + 0.5 * RshList.Aggregate(1.0, (a, b) => a * b);
        }

        public double shkodaZasm(double A, double B, double Goz, double Pdz, int stupinZasm, int riskClass, double Keg)
        {
            return A * B * Goz * Pdz * riskToKnv[riskClass] * stupinZasmichToKzz[stupinZasm] * Keg;
        }

        public double MiOsnDzherel(double roVi, double roVnorm, double q, double T) // 7
        {
            return 3.6e-6 * (roVi - roVnorm) * q * T;
        }

        public double MiGaz(double qMi, double qMNorm, double T) // 8
        {
            return 3.6e-3 * (qMi - qMNorm) * T;
        }

        public double roVi(double roVi, double fiVregl, double fiVvymir) // 9
        {
            return roVi * (21 - fiVregl) / (21 - fiVvymir);
        }

        public double qV(double B, double Vr, double Vv, double Alfa) // 10
        {
            return B * (Vr + Vv * (Alfa - 1));
        }

        public double Z(double mi, double P, double Ai, double Kt, double Kzi) // 11
        {
            return mi * 1.1 * P * Ai * Kt * Kzi;
        }

        public double Ai(double Gdk) // 12
        {
            if (Gdk > 1) 
                return 10 / Gdk;
            else
                return 1 / Gdk;
        }

        public double Kt (double Knas, double Kf) // 13
        {
            return Knas * Kf;
        }

        public double Kzi (double roVi, double GDKsdi)
        {
            return roVi / GDKsdi;
        }
    }

}
