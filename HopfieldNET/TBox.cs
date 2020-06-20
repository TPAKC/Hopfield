using System;

namespace HopfieldNET
{
    [Serializable]
    class TBox
    {
        readonly int NN; // размерность матрицы
        public int N; // количество элементов
        public string name;

        readonly int[] B;

        public TBox(int NN, string name, int d = -1)
        {
            this.NN = NN;
            this.name = name;
            N = NN * NN;

            B = new int[N];

            for (int n = 0; n < N; n++)
            {
                B[n] = d; 
            }
        }

        public TBox(TBox TB)
        {
            NN = TB.NN;
            N = TB.N;
            name = TB.name;
            B = new int[N];

            for (int n = 0; n < N; n++)
            {
                B[n] = TB[n];
            }
        }

        public bool IsEq(TBox B)
        {
            for (int i = 0; i < N; i++)
            {
                if (this[i] != B[i])
                {
                    return false;
                }
            }

            return true;
        }

        public int this[int n]
        {
            get
            {
                return B[n];
            }
            set
            {
                B[n] = value;
            }
        }

        public int this[int i, int j]
        {
            get
            {
                return this[i * NN + j];
            }
            set
            {
                this[i * NN + j] = value;
            }
        }
    }
}
