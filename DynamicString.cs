using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1A
{
    class DynString
    {
        private const int FACTOR_CRECIMIENTO = 2;
        private char[] arr;       // arreglo (dinamico) que representa el string
        public int Size { get; private set; }  // cantidad de caracteres en el string


        // constructor de un string vacio
        public DynString()
        {
            arr = new char[1];
            Size = 0;
        }


        // constructor basado en un string de C#
        public DynString(string s)
        {
            // TODO: Implementar. Inicializa correctamente el arreglo arr y la
            //       propiedad Size.  No tienes que usar la estrategia descrita
            //       en el metodo Insert.

            arr = s.ToCharArray();
            Size = arr.Length;


        }
        
        // Devolver el substring que empieza en la posicion start hasta la posicion 
        // end (ambos extremos son incluyentes)
        public DynString Substring(int start, int end)
        {
            DynString subString = new DynString();
            subString.arr = new char[end+1 - start];

            for (int i = 0; i < subString.arr.Length; i++)
            {
                subString.arr[i] = arr[start];
                start++;
            }
            string mySubstring = new string(subString.arr);

            DynString ret = new DynString(mySubstring);
            return ret;
        }

        // Insertar el MyString str justamente antes de la posicion pos. Debes rodar
        // los caracteres desde la posicion pos hasta la ultima hacia la derecha.
        // Si pos == 0, inserta al inicio de MyString; si pos == Size, inserta
        // al final de MyString (ver uso en el metodo Concat)
        public void Insert(DynString str, int pos)
        {
            //DynString mynewString = new DynString();
            //mynewString.arr = new char[Size + str.Size];
            if (Size == 0)
            {
                Size+=3;
            }
            if (Size < str.Size + Size)
            {
                Array.Resize(ref arr, Size * FACTOR_CRECIMIENTO);
                Size *= FACTOR_CRECIMIENTO;
            }
            if (Size > str.Size)
            {        
                for (int i = 0; i < str.Size; i++)
                {
                    arr[pos] = str.arr[i];
                    pos++;
                }
            }                

        }


        // Concatenar el str al final de este string
        public void Concat(DynString str)
        {
            Insert(str, Size);
        }


        // Borrar el substring que va desde la posicion start hasta la posicion end.
        // Debes rodar los caractetes desde la posicion end hasta la ultima hacia
        // la izquierda.
        public void Erase(int start, int end)
        {

            for (int i = start; i <= end; i++)
            {
                arr[start] = ' ';
                start++;
            }

        }


        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Size; i++)
                sb.Append(arr[i]);
            return sb.ToString();
        }

        class Program
        {
            static void Main(string[] args)
            {

                DynString S = new DynString();

                DynString T = new DynString("world");
                S.Concat(T);

                T = new DynString("Hello ");
                S.Insert(T, 0);

                S.Insert(new DynString(" cruel"), 5);
                Console.WriteLine("S = " + S);

                Console.WriteLine("S.Substring(0, 4) = " + S.Substring(0, 4));

                S.Erase(6, 11);
                Console.WriteLine("S = " + S);

                Console.ReadLine();

                /*
                Resultado esperado:
                S = Hello cruel
                S.Substring(0, 4) = Hello
                S = Hello

                */
            }
        }
    }
}