using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio01
{
    class EmptyException : System.Exception { }
    class Deque
    {

        private int[] arr = new int[5];  // el arreglo circular
        private int head = 0;  // head es el indice del elemento en frente de la cola
        private int tail = -1; // tail es el indice del elemento al final de la cola
        private int size = 0;  // cantidad de datos grabados en el Deque
        private bool addF = false;
        private bool addO = false;

        // Devuelve la cantidad de datos presentes en la cola
        public int Size()
        {
            return size;
        }

        // Agrega el elemento x al final de la cola
        public void AddLast(int x)
        {
            if (arr.Length == size)
                Resize(arr.Length * 2);

            tail++;
            if (tail == arr.Length)
            {
                tail = 0;
                head = 1;
            }
                
            arr[tail] = x;
            size++;
        }

        // Agrega el elemento x al frente de la cola
        public void AddFirst(int x)
        {
            if (arr.Length == size)
                Resize(arr.Length * 2);

            if (size == 0)
            {
                arr[head] = x;
            }
            if (size > 0)
            {
                for (int i = size; i > 0; i--)
                {
                    arr[i] = arr[i - 1];
                }
                arr[head] = x;
            }
            size++;
            tail++;

        }

        // Extrae y devuelve el elemento en frente de la cola
        public int RemoveFirst()
        {
            if (size == 0)
                throw new EmptyException();
            int ret = arr[head];
            arr[head] = 0;
            head++;
            if (head == arr.Length)
                head = 0;

            size--;

            if (size < arr.Length / 4)
                Resize(arr.Length / 2);
            return ret;
           
        }

        // Extrae y devuelve el elemento al final de la cola
        public int RemoveLast()
        {
            if (size == 0)
                throw new EmptyException();
            int ret = arr[tail];
            arr[tail] = 0;
            tail--;
            size--;

            if (size < arr.Length / 4)
                Resize(arr.Length / 2);

            return ret;
        }

        // Agranda/reduce la capacidad del arreglo arr, sin perder los datos que
        // estaban presentes en el Deque.
        private void Resize(int newCapacity)
        {
            int[] temp = new int[newCapacity];
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == temp.Length)
                    break;
                temp[i] = arr[i];
            }
            arr = temp;
        }

        // Devuelve el elemento al frente de la cola
        public int PeekFirst()
        {
            if (size == 0)
                throw new EmptyException();
            return arr[head-1];
        }

        // Devuelve el elemento al final de la cola
        public int PeekLast()
        {
            if (size == 0)
                throw new EmptyException();
            return arr[arr.Length-1];
        }


        // Devuelve el elemento que se encuentra en la posicion pos de la cola,
        // donde Peek(0) equivale a PeekFirst() y Peek(Size()-1) equivale a
        // PeekLast()

        public int Peek(int pos)
        {
            return arr[pos-1];
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
                       
            Deque d = new Deque();
            d.AddFirst(7);
            d.AddFirst(8);
            d.AddLast(9);
            d.AddLast(1);
            d.AddLast(3);
            d.AddFirst(2);
            Console.WriteLine("RemoveLast: {0}", d.RemoveLast());
            Console.WriteLine("RemoveFirst: {0}", d.RemoveFirst());
            Console.WriteLine("RemoveFirst: {0}", d.RemoveFirst());
            Console.WriteLine("RemoveFirst: {0}", d.RemoveFirst());
            Console.WriteLine("RemoveFirst: {0}", d.RemoveFirst());
            d.AddLast(5);
            d.AddFirst(4);
            d.RemoveLast();
            Console.WriteLine("PeekFirst: {0}", d.PeekFirst());
            Console.WriteLine("PeekLast: {0}", d.PeekLast());
            /*
            Resultado esperado:
            RemoveLast: 3
            RemoveFirst: 2
            RemoveFirst: 8
            RemoveFirst: 7
            RemoveFirst: 9
            PeekFirst: 5
            PeekLast: 1
            */
            Console.ReadLine();
        }
    }  
    
}
