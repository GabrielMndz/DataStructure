using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio01___B
{
    class EmptyException : System.Exception { }
    class DynamicArray
    {
        private int[] arr = new int[1];
        private int size = 0;

        // Devuelve la cantidad de datos grabados en el arreglo
        public int Size()
        {
            return size;
        }

        // Agrega el elemento x al final del arreglo
        public void Append(int x)
        {
            if (arr.Length == size)
            {
                Resize(arr.Length * 2);
            }
            arr[size] = x;
            size++;
        }

        // Extrae y devuelve el ultimo elemento grabado en el arreglo
        public int RemoveLast()
        {
            if (size <= 0)
                throw new EmptyException();

            int ret = arr[size - 1];
            arr[size - 1] = 0;
            size--;

            if (size <= arr.Length /4)
            {
                Resize(arr.Length / 2);
            }
            return ret;
        }
                
        public void Modify (int pos, int value)
        {
            if (pos < 0 || pos > size)
                Console.WriteLine("No valido!");

            arr[pos] = value;
        }

        public void Find(int value)
        {
            int pos = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == value)
                    Console.WriteLine("El valor: "+ value + " esta en la posicion: " + (pos+1));
                pos++;
            }
            
        }
        // Agranda/reduce la capacidad del arreglo arr
        private void Resize(int newCapacity)
        {
            int[] temp = new int[newCapacity];
            for (int i = 0; i < size; i++)
            {
                temp[i] = arr[i];
            }
            arr = temp;
        }
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            DynamicArray dynArray = new DynamicArray();
            dynArray.Append(5);
            dynArray.Append(7);
            Console.WriteLine(dynArray.RemoveLast());
            dynArray.Append(17);
            dynArray.Append(5);
            dynArray.Append(4);
            dynArray.Append(3);
            Console.WriteLine(dynArray.RemoveLast());
            Console.ReadLine();

            /*
            Resultado esperado:
            RemoveLast: 7
            RemoveLast: 3

            */
        }
    }
}
