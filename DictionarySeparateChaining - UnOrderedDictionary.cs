using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionarySeparateChaining
{
    class DuplicaKey : System.Exception { }
    class Diccionario<Item>
    {
         /*************by:
                        Gabriel Mendez Reyes -***************/
       
        class KeyItemPair
        {
            public int key { get; private set; }
            public Item item { get; private set; }
            public KeyItemPair(int key, Item item)
            {
                this.key = key;
                this.item = item;
            }
            /*
            public override bool Equals(Object obj)
            {
                if (obj == null || GetType() != obj.GetType()) return false;
                KeyItemPair r = (KeyItemPair)obj;
                // Use Equals to compare instance variables.
                return key == r.key;
            }    
            */
        }

        LinkedList<KeyItemPair>[] buckets;

        public int size { get; private set; }

        public Diccionario()
        {
            const int INTIAL_CAPACITY = 3;
            buckets = new LinkedList<KeyItemPair>[INTIAL_CAPACITY];
            for (int i = 0; i < INTIAL_CAPACITY; i++)
                buckets[i] = new LinkedList<KeyItemPair>();
            size = 0;
        }

        private int GetIndex(int key)
        {
            return Math.Abs(key) % buckets.Length;
        }

        private int GetIndex(int key, int cap)
        {
            return Math.Abs(key) % cap;
        }
        /*Resize when the dictionary is full*/
        private void ResizeAndReindex(int newCapacity)
        {
            Console.WriteLine("Resize a " + newCapacity);
            var newBuckets = new LinkedList<KeyItemPair>[newCapacity];
            for (int i = 0; i < newCapacity; i++)
                newBuckets[i] = new LinkedList<KeyItemPair>();
            for (int i = 0; i < buckets.Length; i++)
            {
                foreach (KeyItemPair pareja in buckets[i])
                {
                    int newPos = GetIndex(pareja.key, newCapacity);
                    newBuckets[newPos].AddLast(pareja);
                }
            }
            buckets = newBuckets;
        }
        /*Add key with the value to the Dictionary*/
        public void Add(int key, Item value)
        {
            if (size >= buckets.Length)
            {
                const int FACTOR_CRECIMIENTO = 2;
                ResizeAndReindex(buckets.Length * FACTOR_CRECIMIENTO);
            }


            int pos = GetIndex(key);
            //  Console.WriteLine("Posicion de key {0} es {1}", key, pos);

            KeyItemPair pareja = new KeyItemPair(key, value);

            if (Find(buckets[pos], key) != null)
                throw new DuplicaKey();

            buckets[pos].AddLast(pareja);
            size++;
        }
        private KeyItemPair Find(LinkedList<KeyItemPair> lista, int key)
        {
            foreach (KeyItemPair pareja in lista)
            {
                if (pareja.key == key)
                    return pareja;
            }
            return null;
        }
        /*Find the value of key of the Dictionary*/
        public Item Find(int key)
        {
            int pos = GetIndex(key);
            KeyItemPair pareja = Find(buckets[pos], key);

            if (pareja == null)
                return default(Item);
            else
                return pareja.item;
        }
        /*Remove key from Dictionary*/
        public Item Remove(int key)
        {
            int pos = GetIndex(key);
            LinkedListNode<KeyItemPair> cur = buckets[pos].First;
            while (cur != null)
            {
                if (cur.Value.key == key)
                {
                    Item ret = cur.Value.item;
                    buckets[pos].Remove(cur);
                    size--;
                    return ret;
                }
                cur = cur.Next;
            }
            return default(Item);
        }
    }

    class Estudiante
    {
        public int id { get; private set; }
        public string name { get; set; }
        public Estudiante(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public override string ToString()
        {
            return "id: " + this.id
                   + "  name: " + this.name;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Diccionario<Estudiante> d = new Diccionario<Estudiante>();
            Estudiante maria = new Estudiante(100, "Maria");
            Estudiante maria2 = new Estudiante(100, "Otra Maria");

            d.Add(maria.id, maria);
            try
            {
                d.Add(maria2.id, maria2);
            }
            catch (DuplicaKey ex)
            {
                Console.WriteLine("Bien! id de maria2 esta duplicado");
            }

            d.Add(12, new Estudiante(12, "Ramon"));
            d.Add(-100, new Estudiante(-100, "Maria a la inversa"));

            d.Add(123, new Estudiante(123, "XYZ"));

            Console.WriteLine("Size: " + d.size);

            Console.WriteLine(" Find(100): " + d.Find(100));
            Console.WriteLine(" Find(444): " + d.Find(444));

            Console.WriteLine(" Remove(-100) : " + d.Remove(-100));
            Console.WriteLine(" Find(-100): " + d.Find(-100));
            Console.WriteLine(" Remove(-100) : " + d.Remove(-100));

            Random rnd = new Random();
            Diccionario<string> d2 = new Diccionario<string>();
            for (int t = 1; t <= 10000; t++)
            {
                int key = rnd.Next();
                try
                {
                    d2.Add(key, "Valor " + key);
                }
                catch (DuplicaKey ex)
                {

                }
            }
            Console.ReadLine();
            /*
            Resultado esperado:
                Bien! id de maria2 esta duplicado
                Resize a 6
                Size: 4
                 Find(100): id: 100  name: Maria
                 Find(444):
                 Remove(-100) : id: -100  name: Maria a la inversa
                 Find(-100):
                 Remove(-100) :
                Resize a 6
                Resize a 12
                Resize a 24
                Resize a 48
                Resize a 96
                Resize a 192
                Resize a 384
                Resize a 768
                Resize a 1536
                Resize a 3072
                Resize a 6144
                Resize a 12288
            */
        }
    }
}
