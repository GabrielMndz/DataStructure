using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trie
{
    class DuplicateKeyException : Exception { }
    class KeyNotFoundException : Exception { }
    class Trie<V>
    {

        class TrieNode
        {
            public V value;
            public bool alive;
            public TrieNode[] children = new TrieNode[10];
            public TrieNode(V value = default(V))
            {
                this.value = value;
            }
        }

        TrieNode root = new TrieNode();

        public void Add(int key, V value)
        {
            TrieNode cur = root;
            int x = key;
            while (x > 0)
            {
                int d = x % 10;
                x /= 10;

                TrieNode nxt = cur.children[d];
                if (nxt == null)
                {
                    cur.children[d] = new TrieNode();
                    nxt = cur.children[d];
                }
                cur = nxt;
            }
            if (cur.alive)
                throw new DuplicateKeyException();
            cur.value = value;
            cur.alive = true;
        }

        public V Find(int key)
        {
            TrieNode cur = root;
            int x = key;
            while (x != 0)
            {
                int d = Math.Abs(x % 10);
                x /= 10;
                TrieNode nxt = cur.children[d];
                if (nxt == null)
                    throw new KeyNotFoundException();
                cur = nxt;
            }
            if (cur.alive)
                return cur.value;
            else
                throw new KeyNotFoundException();
        }

        public V Remove(int key)
        {
            TrieNode cur = root;
            int x = key;
            while (x != 0)
            {
                int d = Math.Abs(x % 10);
                x /= 10;
                TrieNode nxt = cur.children[d];
                if (nxt == null)
                    throw new KeyNotFoundException();
                cur = nxt;
            }
            if (cur.alive)
            {
                V ret = cur.value;
                cur.alive = false;
                cur.value = default(V);
                return ret;
            }
            else
                throw new KeyNotFoundException();

        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            var t = new Trie<string>();
            t.Add(1, "uno");
            t.Add(123, "ciento veinte y tres");
            t.Add(2, "dos");
            t.Add(23, "veinte y tres");

            Console.WriteLine(t.Find(123));
            t.Remove(123);
            try
            {
                Console.WriteLine(t.Find(123));
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine("123 no existe como key");
            }

            t.Add(-123, "negativo ciento veinte y tres");
            Console.WriteLine(t.Find(-123));
            Console.ReadKey();
            
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
