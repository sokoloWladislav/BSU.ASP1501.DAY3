using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polynom
{
    public class Polynom
    {
        private double[] coefs;

        public int Power{ get; private set;}
        public double[] Coefs
        {
            get
            {
                return coefs;
            }
            set
            {
                int k = 0;
                for (int i = value.Length - 1; i >= 0; --i)
                {
                    if (value[i] != 0)
                        break;
                    else
                        ++k;
                }
                if (k == value.Length)
                    coefs = new double[1] { 0 };
                else
                {
                    coefs = new double[value.Length - k];
                    for (int i = 0; i < value.Length - k; ++i)
                        coefs[i] = value[i];
                }
            }
        }

        public Polynom(double[] coefs)
        {
            if (coefs == null)
                Coefs = new double[1] { 0 };
            else
                Coefs = coefs;
            Power = Coefs.Length - 1;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Polynom p = obj as Polynom;
            if (p == null)
                return false;
            
            if (Coefs.Length != p.Coefs.Length)
                return false;

            int k = 0;
            for (int i = 0; i < Coefs.Length; ++i)
                if (Coefs[i] == p.Coefs[i])
                    ++k;

            return (k == Coefs.Length);
        }

        public bool Equals(Polynom p)
        {
            if (p == null)
                return false;

            if (Coefs.Length != p.Coefs.Length)
                return false;

            int k = 0;
            for (int i = 0; i < Coefs.Length; ++i)
                if (Coefs[i] == p.Coefs[i])
                    ++k;

            return (k == Coefs.Length);

        }

        public override int GetHashCode()
        {
            int hashCode = 13;
            for (int i = 0; i < Coefs.Length; ++i)
                hashCode += (int)Coefs[i];
            return (hashCode * (Power + 121)) % 235;
        }

        public override string ToString()
        {
            string result = "";
            for (int i = Coefs.Length - 1; i >= 0; --i)
            {
                if (Coefs[i] == 0)
                    continue;
                if (i != Coefs.Length - 1)
                    result += " + ";
                result += Coefs[i];
                if (i == 0)
                    continue;
                result += "x";
                if (i == 1)
                    continue;
                result += "^" + i;
            }
                return result;
        }

        public static Polynom operator +(Polynom p)
        {
            return new Polynom(p.Coefs);
        }

        public static Polynom operator ++(Polynom p)
        {
            Polynom result = new Polynom(p.Coefs);
            for (int i = 0; i < result.Coefs.Length; ++i)
                ++result.Coefs[i];
            return result;
        }

        public static Polynom operator +(Polynom p, double num)
        {
            Polynom result = new Polynom(p.Coefs);
            for (int i = 0; i < result.Coefs.Length; ++i)
                result.Coefs[i] += num;
            return result;
        }

        public static Polynom operator +(Polynom first, Polynom second)
        {
            double[] temp;
            if(first.Coefs.Length > second.Coefs.Length)
            {
                temp = new double[first.Coefs.Length];
                for(int i = 0; i < first.Coefs.Length; ++i)
                {
                    temp[i] = first.Coefs[i];
                    if(i < second.Coefs.Length)
                        temp[i] += second.Coefs[i];
                }
            }
            else
            {
                temp = new double[second.Coefs.Length];
                for (int i = 0; i < second.Coefs.Length; ++i)
                {
                    temp[i] = second.Coefs[i];
                    if (i < first.Coefs.Length)
                        temp[i] += first.Coefs[i];
                }
            }
                return new Polynom(temp);
        }

        public static Polynom operator -(Polynom p)
        {
            Polynom result = new Polynom(p.Coefs);
            for (int i = 0; i < result.Coefs.Length; ++i)
                result.Coefs[i] *= -1;
            return result;
        }

        public static Polynom operator --(Polynom p)
        {
            Polynom result = new Polynom(p.Coefs);
            for (int i = 0; i < result.Coefs.Length; ++i)
                --result.Coefs[i];
            return result;
        }

        public static Polynom operator -(Polynom p, double num)
        {
            Polynom result = new Polynom(p.Coefs);
            for (int i = 0; i < result.Coefs.Length; ++i)
                result.Coefs[i] -= num;
            return result;
        }

        public static Polynom operator -(Polynom first, Polynom second)
        {
            double[] temp;
            if (first.Coefs.Length > second.Coefs.Length)
            {
                temp = new double[first.Coefs.Length];
                for (int i = 0; i < first.Coefs.Length; ++i)
                {
                    temp[i] = first.Coefs[i];
                    if (i < second.Coefs.Length)
                        temp[i] -= second.Coefs[i];
                }
            }
            else
            {
                temp = new double[second.Coefs.Length];
                for (int i = 0; i < second.Coefs.Length; ++i)
                {
                    temp[i] = second.Coefs[i];
                    if (i < first.Coefs.Length)
                        temp[i] -= first.Coefs[i];
                }
            }
            return new Polynom(temp);
        }

        public static Polynom operator *(Polynom p, double num)
        {
            Polynom result = new Polynom(p.Coefs);
            for (int i = 0; i < result.Coefs.Length; ++i)
                result.Coefs[i] *= num;
            return result;
        }

        public static Polynom operator *(Polynom p, Polynom q)
        {
            double[] result = new double[p.Coefs.Length + q.Coefs.Length - 1];
            if (p.Coefs.Length > q.Coefs.Length)
            {
                for (int i = 0; i < p.Coefs.Length; ++i)
                    for (int j = 0; j < q.Coefs.Length; ++j )
                        result[i + j] += p.Coefs[i] * q.Coefs[j];
            }
            return new Polynom(result);
        }
    }
}
