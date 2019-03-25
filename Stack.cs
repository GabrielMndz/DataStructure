using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trie
{
    class DuplicateKeyException : Exception { }
    class KeyNotFoundException : Exception { }

    class Stack<Item>
    {
        private Item[] arr;
        public int size { get; private set; }


        public Stack()
        {
            size = 0;
            arr = new Item[1];
        }



        public void Push(Item value)
        {
            if (size == arr.Length)
            {
                const int FACTOR_CRECIMIENTO = 2;
                Resize(arr.Length * FACTOR_CRECIMIENTO);
            }

            arr[size] = value;
            size++;

        }

        public Item Peek()
        {
            if (size == 0)
            {
                throw new StackVaciaException();
            }

            return arr[size - 1];
        }
        public Item Pop()
        {
            if (size == 0)
            {
                throw new StackVaciaException();
            }



            Item ret = arr[size - 1];
            size--;

            if (size * 4 <= arr.Length)
            {
                const int FACTOR_DECREMENTO = 2;
                Resize(arr.Length / FACTOR_DECREMENTO);
            }

            return ret;
        }

        public bool isEmpty()
        {
            return size == 0;
        }

        public void Resize(int newCapacity)
        {
            Item[] newArr = new Item[newCapacity];
            for (int i = 0; i < size; i++)
            {
                newArr[i] = arr[i];
            }

            arr = newArr;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < size; i++)
            {
                sb.Append(arr[i] + " ");

            }
            return sb.ToString();
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> s1 = new Stack<int>();
            s1.Push(3);
            s1.Push(4);
            Console.WriteLine("Size = {0} , Stack: {1}", s1.size, s1);
            s1.Push(5);
            s1.Push(6);

            Console.WriteLine("Size = {0} , Stack: {1}", s1.size, s1);
            s1.Pop();
            s1.Pop();
            s1.Pop();
            Console.WriteLine("Size = {0} , Stack: {1}", s1.size, s1);

            
            /*
            Resultado esperado:
            .Find(123): ciento veinte y tres
            .Remove(123): ciento veinte y tres
            .KeyNotFoundException : "123 no existe como key"
            .Find(-123) : negativo ciento veinte y tres
            */
        }
    }
}
