using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    class DuplicateKeyException : Exception { }
    class EmptyTree : Exception { }
    class NotSuccessorOrNotDeccessor : Exception { }
    class PositionOutOfRangeException : Exception { }
    class OrderedDictionary<K, V> where K : IComparable<K>
    {

        class TreeNode
        {
            public K key;
            public V value;
            public TreeNode left, right, parent;
            public int subtreeSize = 1;
            public TreeNode(K key, V value)
            {
                this.key = key;
                this.value = value;
            }
        }
        private TreeNode root;
        public int size { get; private set; }

        private int SubtreeSize(TreeNode x)
        {
            if (x == null) return 0;
            else return x.subtreeSize;
        }

        public int Size()
        {
            return SubtreeSize(root);
        }
        
        /***********************/
        public void Add(K key, V value)
        {
            if (size == 0)
            {
                root = new TreeNode(key, value);
                size = 1;
                return;
            }

            TreeNode cur = root, prev = null;
            while (cur != null)
            {
                if (key.CompareTo(cur.key) == 0)
                    throw new DuplicateKeyException();
                prev = cur;
                if (key.CompareTo(cur.key) < 0)
                    cur = cur.left;
                else
                    cur = cur.right;
            }
            TreeNode newNode = new TreeNode(key, value);
            newNode.parent = prev;
            if (key.CompareTo(prev.key) < 0)
                prev.left = newNode;
            else
                prev.right = newNode;
            ++size;
            cur = prev;
            while (cur != null)
            {
                //  cur.subtreeSize++;
                cur.subtreeSize = SubtreeSize(cur.left) +
                                  SubtreeSize(cur.right) + 1;
                cur = cur.parent;
            }
        }

        /***********************/
        private TreeNode FindNode(K key)
        {
            TreeNode cur = root;
            while (cur != null)
            {
                if (key.CompareTo(cur.key) == 0)
                    return cur;
                else if (key.CompareTo(cur.key) < 0)
                    cur = cur.left;
                else
                    cur = cur.right;
            }
            return null;
        }
        public V Find(K key)
        {
            TreeNode x = FindNode(key);
            if (x == null)
                throw new KeyNotFoundException();
            return x.value;
        }

        /***********************/
        private TreeNode MinNode(TreeNode cur)
        {
            if (cur == null)
                return null;
            while (cur.left != null)
                cur = cur.left;
            return cur;
        }

        public K Min()
        {
            TreeNode x = MinNode(root);
            if (x == null)
                throw new EmptyTree();
            return x.key;
        }

        /***********************/
        private TreeNode MaxNode(TreeNode x)
        {
            if (x == null)
                return null;
            while (x != null)
            {
                x = x.right;
            }
            return x;
        }
        public K Max()
        {
            TreeNode cur = MaxNode(root);
            if (cur == null)
                throw new EmptyTree();

            return cur.key;
        }

        /***********************/
        public K Successor(K key)
        {
            TreeNode cur = root, best = null;
            while (cur != null)
            {
                if (key.CompareTo(cur.key) == 0)
                    cur = cur.right;

                else if (key.CompareTo(cur.key) < 0)
                {
                    if (best == null || best.key.CompareTo(cur.key) > 0)
                        best = cur;
                    cur = cur.left;
                }
                else
                    cur = cur.right;
            }
            if (best == null)  // no hay sucesor
                throw new NotSuccessorOrNotDeccessor();
            return best.key;
        }

        /***********************/
        public K Predeccessor(K key)
        {
            TreeNode cur = root, best = null;

            while (cur != null)
            {
                if (key.CompareTo(cur.key) == 0)
                {
                    cur = cur.left;
                }

                else if (key.CompareTo(cur.key) > 0)
                {
                    best = cur;
                    cur = cur.right;

                }
                else
                    cur = cur.left;
            }
            if (best == null)  // no hay predeccesor
                throw new NotSuccessorOrNotDeccessor();
            return best.key;
        }

        /***********************/
        public int Rank(int key)
        {
            int cantMenores = 0;

            TreeNode cur = root;
            while (cur != null)
            {
                if (key.CompareTo(cur.key) == 0)
                {
                    cantMenores += SubtreeSize(cur.left);
                    break;
                }
                else if (key.CompareTo(cur.key) < 0)
                {
                    cur = cur.left;
                }
                else
                {
                    cantMenores += SubtreeSize(cur.left) + 1;
                    cur = cur.right;
                }
            }

            return cantMenores;
        }

        /***********************/
        public K Select(int pos)
        {
            if (pos < 0 || pos >= size)
                throw new PositionOutOfRangeException();

            int cantMenores = pos;

            TreeNode cur = root;
            while (cur != null)
            {
                if ((cantMenores - SubtreeSize(cur.left)) < 0)
                    cur = cur.left;
                else if ((cantMenores - SubtreeSize(cur.left)) > 0)
                {
                    cantMenores -= SubtreeSize(cur.left) + 1;
                    cur = cur.right;
                }
                else
                {
                    // (cantMenores - SubtreeSize(cur.left)) == 0
                    return cur.key;
                }
            }

            throw new Exception("WTF");
        }

        /***********************/
        public V Remove(K key)
        {
            TreeNode x = FindNode(key);
            if (x == null)
                throw new KeyNotFoundException();
            V ret = RemoveNode(x);
            size--;
            return ret;
        }

        private V RemoveNode(TreeNode x)
        {

            V ret = x.value;
            TreeNode p = x.parent;

            if (x.left == null && x.right == null)
            {
                // no tiene hijos
                if (p == null)
                {
                    // x == root
                    root = null;
                    return ret;
                }
                //  if (x.key < p.key)
                if (x == p.left)
                    p.left = null;
                else
                    p.right = null;
            }

            else if (x.left != null && x.right == null)
            {
                // tiene 1 hijo (izquierdo)
                TreeNode y = x.left;

                if (p == null)
                {
                    root = y;
                }
                else if (x == p.left)
                {
                    p.left = y;
                }
                else
                {
                    p.right = y;
                }
                y.parent = p;

            }
            else if (x.left == null && x.right != null)
            {
                // tiene 1 hijo (derecho)
                TreeNode y = x.right;
                if (p == null)
                {
                    root = y;
                }
                else if (x == p.left)
                {
                    p.left = y;
                }
                else
                {
                    p.right = y;
                }
                y.parent = p;

            }
            else
            {
                // tiene 2 hijos
                TreeNode s = MinNode(x.right);
                x.key = s.key;
                x.value = s.value;
                RemoveNode(s);
            }

            TreeNode cur = p;
            while (cur != null)
            {
                cur.subtreeSize = SubtreeSize(cur.left) +
                                  SubtreeSize(cur.right) + 1;
                cur = cur.parent;
            }

            return ret;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            OrderedDictionary<int, string> d = new OrderedDictionary<int, string>();
            d.Add(3, "tres");
            d.Add(2, "dos");
            d.Add(10, "diez");
            d.Add(5, "cinco");
            d.Add(8, "ocho");
            d.Add(11, "once");
            Console.WriteLine(d.Find(3));
            Console.WriteLine(d.Min());
            Console.WriteLine(d.Successor(3));
            Console.WriteLine(d.size + " vs " + d.Size());
            Console.WriteLine(d.Rank(10));
            Console.WriteLine(d.Rank(6));
            for (int p = 0; p < d.size; p++)
                Console.WriteLine("Select(" + p + ") = " + d.Select(p));
            Console.WriteLine(d.Remove(10));
            Console.WriteLine(d.Remove(3));
            Console.WriteLine(d.Remove(11));
            Console.WriteLine(d.Remove(2));
            Console.WriteLine(d.size + " vs " + d.Size());
            Console.WriteLine(d.Remove(5));
            Console.WriteLine(d.Remove(8));
            Console.WriteLine(d.size + " vs " + d.Size());

            Console.WriteLine("Termino");
            Console.WriteLine(d.size);

            var d2 = new OrderedDictionary<DateTime, string>();
            d2.Add(new DateTime(), "xxx");

            Console.ReadKey();
        }
    }
}
