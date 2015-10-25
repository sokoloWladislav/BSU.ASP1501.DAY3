using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Polynom
{
    public sealed class Polynom
    {
        public static double epsilon;
        private double[] _coefs = {};
    /*static Polynom()
    {
        epsilon = double.Parse(System.Configuration.ConfigurationManager.AppSettings["epsilon"]);
    }*/

        public Polynom(params double[] coefs)
        {
            if (coefs == null)
                _coefs = new double[1] {0};
            else
            {
                _coefs = new double[coefs.Length];
                Array.Copy(coefs, _coefs, coefs.Length);
                CutZeros(ref _coefs);
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != this.GetType()) return false;

            return this.Equals(obj);
        }

        public bool Equals(Polynom p)
        {
            if (ReferenceEquals(null, p)) return false;
            if (ReferenceEquals(this, p)) return true;

            if (_coefs.Length != p._coefs.Length)
                return false;

            for (int i = 0; i < _coefs.Length; ++i)
                if (_coefs[i] != p._coefs[i])
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            int hashCode = 13;
            for (int i = 0; i < _coefs.Length; ++i)
                hashCode += (int)_coefs[i];
            return (hashCode * (_coefs.Length + 121)) % 237;
        }

        public static bool operator ==(Polynom lhs, Polynom rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return true;
            if (ReferenceEquals(null, lhs)) return false;

            return lhs.Equals(rhs);
        }

        public static bool operator !=(Polynom lhs, Polynom rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return false;
            if (ReferenceEquals(null, lhs)) return true;

            return !lhs.Equals(rhs);
        }

        public override string ToString()
        {
            string result = "";
            for (int i = _coefs.Length - 1; i >= 0; --i)
            {
                if (_coefs[i] < epsilon)
                    continue;
                if (i != _coefs.Length - 1)
                    result += " + ";
                result += _coefs[i];
                if (i == 0)
                    continue;
                result += "x";
                if (i == 1)
                    continue;
                result += "^" + i;
            }
                return result;
        }

        public static Polynom operator +(Polynom p, double num)
        {
            double[] result = new double[p._coefs.Length];
            Array.Copy(p._coefs, result, p._coefs.Length);
            for (int i = 0; i < result.Length; ++i)
                result[i] += num;
            return new Polynom(result);
        }

        public static Polynom operator +(Polynom lhs, Polynom rhs)
        {
            double[] temp = new double[Math.Max(lhs._coefs.Length, rhs._coefs.Length)];
            if(lhs._coefs.Length > rhs._coefs.Length)
            {
                Array.Copy(lhs._coefs, temp, temp.Length);
                for(int i = 0; i < rhs._coefs.Length; ++i)
                        temp[i] += rhs._coefs[i];
            }
            else
            {
                Array.Copy(rhs._coefs, temp, temp.Length);
                for (int i = 0; i < lhs._coefs.Length; ++i)
                    temp[i] += lhs._coefs[i];
            }
                return new Polynom(temp);
        }

        public static Polynom operator -(Polynom p)
        {
            return p * (-1);
        }

        public static Polynom operator -(Polynom p, double num)
        {
            return p + (-num);
        }

        public static Polynom operator -(Polynom lhs, Polynom rhs)
        {
            return lhs + (-rhs);
        }

        public static Polynom operator *(Polynom p, double num)
        {
            double[] result = new double[p._coefs.Length];
            Array.Copy(p._coefs, result, result.Length);
            for (int i = 0; i < result.Length; ++i)
                result[i] *= num;
            return new Polynom(result);
        }

        public static Polynom operator *(Polynom lhs, Polynom rhs)
        {
            double[] result = new double[lhs._coefs.Length + rhs._coefs.Length - 1];
                for (int i = 0; i < lhs._coefs.Length; ++i)
                    for (int j = 0; j < rhs._coefs.Length; ++j )
                        result[i + j] += lhs._coefs[i] * rhs._coefs[j];
            return new Polynom(result);
        }

        private static void CutZeros(ref double[] array)
        {
            int k = 0;
            while (Math.Abs(array[array.Length - 1 - k])  < epsilon && k != array.Length - 1)
                ++k;
            Array.Resize(ref array, array.Length - k);
        }
    }
}
