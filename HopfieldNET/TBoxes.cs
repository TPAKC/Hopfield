using System;
using System.Collections;

namespace HopfieldNET
{
    [Serializable]
    class TBoxes
    {
        public int NN;
        readonly ArrayList arr;

        public TBoxes(int NN)
        {
            this.NN = NN;
            arr = new ArrayList();
        }

        public void Add(TBox B)
        {
            arr.Add(B);
        }

        public bool Find(TBox B)
        {
            for (int n = 0; n < Count; n++)
            {
                if (B.IsEq(this[n]))
                {
                    return true;
                }
            }

            return false;
        }

        public int N
        {
            get
            {
                return NN * NN;
            }
        }


        public TBox this[int k]
        {
            get
            {
                return (TBox)arr[k];
            }
        }

        public int Count
        {
            get
            {
                return arr.Count;
            }
        }
    }
}
