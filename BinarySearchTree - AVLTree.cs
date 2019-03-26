using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace AVLTree
{
    class DuplicateKeyException : Exception { }
    class InvalidRotation : Exception { }
    class NotSuccessorFound : Exception { }
    class NotAVLTree
    {
        /*************by:
                        Gabriel Mendez Reyes -***************/

        class TreeNode : IComparable
        {
            StringBuilder sb = new StringBuilder();
            public int key;
            public TreeNode left, right, parent;
            public int height, depth, heightBalanceFactor;  // NOTA: puedes agregar estos atributos si lo deseas
            public TreeNode(int key)
            {
                this.key = key;
            }

            public int CompareTo(object obj)
            {
                TreeNode that = (TreeNode)obj;
                return that.depth.CompareTo(this.depth);
            }

            // Convertir el subtree cuya raiz es este nodo a un string (para
            // verificar que su codigo de conversion funciona)
            override public string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append('(');
                sb.Append(key);
                if (left != null || right != null)
                {
                    sb.Append('-');
                    sb.Append(left == null ? "()" : left.ToString());
                    sb.Append('-');
                    sb.Append(right == null ? "()" : right.ToString());
                }
                sb.Append(')');
                return sb.ToString();
            }
        }


        private TreeNode root;
        public int size { get; private set; }  // Cantidad de keys grabados en el arbol
        private int getHeight(TreeNode x) // Complexity: O(1)
        {
            if (x == null)
                return -1;

            else
                return x.height;
        }

        /** 
         *  Agrega key al arbol
         **/
        public void Add(int key)
        {
            if (size == 0)
            {
                root = new TreeNode(key);
                size = 1;
                return;
            }

            TreeNode cur = root, prev = null;
            while (cur != null)
            {
                if (key == cur.key)
                    throw new DuplicateKeyException();
                prev = cur;
                if (key < cur.key)
                    cur = cur.left;
                else
                    cur = cur.right;
            }
            TreeNode newNode = new TreeNode(key);
            newNode.parent = prev;
            if (key < prev.key)
                prev.left = newNode;
            else
                prev.right = newNode;
            ++size;
        }

        /** 
         *  Busca y devuelve el nodo que contiene key; devuelve null si no se 
         *  encuentra
         **/
        private TreeNode FindNode(int key)
        {
            TreeNode cur = root;
            while (cur != null)
            {
                if (key == cur.key)
                    return cur;
                else if (key < cur.key)
                    cur = cur.left;
                else
                    cur = cur.right;
            }
            return null;
        }

        /** 
         *  Devuelve true si key existe en el arbol
         **/
        public bool Exists(int key)
        {
            TreeNode x = FindNode(key);
            return x != null;
        }

        /** 
         *  Borra key del arbol; devuelve false si el key NO se encuentra en el
         *  arbol
         **/
        public bool Remove(int key)
        {
            // TODO: Algoritmo implementado:
            //       1) Buscar el nodo X que contiene key
            //       2) Si X es un leaf o tiene un solo hijo, borralo como fue
            //          descrito para Hibbard's Delete
             //       3) Si X tiene 2 hijos
            //            3a) Ubica el sucesor de X, llamemoslo Y
            //            3b) Rota el nodo Y hasta que se convierta en padre de X
            //            3c) En este punto X no tendra hijo derecho y lo puedes
            //                borrar como en el caso de que tiene un solo hijo


            if (!Exists(key))
                return false;

            TreeNode myNode = FindNode(key);

            if (myNode == null)
                throw new KeyNotFoundException();

            bool remove = RemoveNode(myNode);
            size--;

            return remove;
        }
        private bool RemoveNode( TreeNode myNode )
        {
            TreeNode myParent = myNode.parent;

            if (myNode.right == null && myNode.left == null)
            {
                // no hijo
                if (myParent == null)
                {
                    root = null;
                }
                else if (myNode == myParent.left)
                    myParent.left = null;
                else
                    myParent.right = null;

                
            }
            if (myNode.left != null && myNode.right == null)
            {
                // tiene 1 hijo (izquierdo)
                TreeNode leftChild = myNode.left;

                if (myParent == null)
                {
                    root = leftChild;
                }
                else if (myNode == myParent.left)
                {
                    myParent.left = leftChild;
                }
                else
                {
                    myParent.right = leftChild;
                }

                leftChild.parent = myParent;
                
            }
            if (myNode.left == null && myNode.right != null)
            {
                // tiene 1 hijo (derecho)
                TreeNode rightChild = myNode.right;
                if (myParent == null)
                {
                    root = rightChild;
                }
                else if (myNode == myParent.left)
                {
                    myParent.left = rightChild;
                }
                else
                {
                    myParent.right = rightChild;
                }
                rightChild.parent = myParent;
                
            }
            if (myNode.left != null && myNode.right != null)
            {
                // tiene 2 hijos
                TreeNode Y = Successor(myNode.key);
                TreeNode myParentY = Y.parent;

                while (myNode.parent != Y)
                {
                    if(myParentY.left == Y)
                    {
                        RotateRight(myParentY);
                    }else
                    RotateLeft(myParentY);

                    myParentY = Y.parent;
                }
                RemoveNode(myNode);
                                
            }
            return true;

        }
        private TreeNode Successor(int key)
        {
            TreeNode cur = root, best = null;
            while (cur != null)
            {
                if (key == cur.key)
                    cur = cur.right;

                else if (key < cur.key)
                {
                    if (best == null || best.key > cur.key)
                        best = cur;
                    cur = cur.left;
                }
                else
                    cur = cur.right;
            }
            if (best == null)  // no hay sucesor
                throw new NotSuccessorFound();
            return best;
        }

        /** 
         *  rotateLeft(P) rota el nodo P hacia la izquierda.
         **/
        private void RotateLeft(TreeNode P) // O(1)
        {
            // NOTA: Puedes alterar este metodo
            if (P == null || P.right == null)
                throw new InvalidRotation();

            TreeNode par = P.parent;
            TreeNode Q = P.right;
            TreeNode B = Q.left;
            if (B != null)
                B.parent = P;
            P.right = B;
            P.parent = Q;
            Q.left = P;
            Q.parent = par;
            if (par != null)
            {
                if (P == par.left)
                    par.left = Q;
                else
                    par.right = Q;
            }
            else
                root = Q;
            P.height = HeightNode(P);

            if (B != null)
            {
                B.height = HeightNode(B);
            }
            if (Q != null)
            {
                Q.height = HeightNode(Q);
            }

            if (par != null)
            {
                par.height = HeightNode(par);
            }
        }


        /*** 
         *  rotateRight(Q) rota el nodo Q hacia la derecha.
         **/
        private void RotateRight(TreeNode Q) // O(1)
        {                                      
            // NOTA: Puedes alterar este metodo
            if (Q == null|| Q.left == null)
                throw new InvalidRotation();

            TreeNode par = Q.parent;
            TreeNode P = Q.left;
            TreeNode B = P.right;
            if (B != null)
                B.parent = Q;
            Q.left = B;
            Q.parent = P;
            P.right = Q;
            P.parent = par;
            if (par != null)
            {
                if (Q == par.left)
                    par.left = P;
                else
                    par.right = P;
            }
            else
                root = P;

            if (B != null)
            {
                B.height = HeightNode(B);
            }

            Q.height = HeightNode(Q);

            if (P != null)
            {
                P.height = HeightNode(P);
            }
            if (par != null)
            {
                par.height = HeightNode(par);
            }
        }

        /***
        Transformar el Binary Search Tree a AVL Tree
         **/
        private bool FixAVLPropertyOfNode(TreeNode x) // O(1)
        {
            // TODO: FixAVLProperty en el nodo x.
            //       NOTA: el algoritmo descrito NO funciona si el
            //       height balance factor del nodo x es < -2 o > +2, en cuyo caso
            //       este metodo simplemente devuelve false; de lo contrario,
            //       corrigelo y devuelve true

            if (x.heightBalanceFactor < -2 || x.heightBalanceFactor > 2)
                return false;

            if (x.heightBalanceFactor == 2)
            {
                if (x.right.heightBalanceFactor == -1) // codo
                {
                    RotateRight(x.right);
                }
                RotateLeft(x);
                
            }
            if (x.heightBalanceFactor == -2)
            {
                if (x.left.heightBalanceFactor == 1) //  codo
                {
                    RotateLeft(x.left);
                }
                RotateRight(x);
            }

            return true;
        }


        /**
         * Transformar el Binary Search Tree a AVL Tree
         **/
        public bool TransformToAVLTree()
        {
            // TODO: Algoritmo implementado:
            //       1) Computa el depth de cada nodo en el arbol
            //       2) Ordena los nodos por su depth
            //          Puedes consultar en Internet sobre como ordenar datos en C#
            //       3) Itera los nodos de mayor a menor depth
            //         3a) Calcula el height y balance factor de cada nodo
            //         3b) Si un nodo requiere correccion, llama al metodo
            //             FixAVLPropertyOfNode
            //       Si en alguno punto no fue posible corregirse, devuelve false
         
            
            List<TreeNode> myList = InOrderIteration(); // Complexity: O(N)

            bool SePudoArreglar = true;

            foreach (var cur in myList) // Complexity: O(N*H) 
            {
                cur.depth = depth(cur.key); // 1) Computa el depth de cada nodo en el arbol
            }
            myList.Sort(); // Complexity: O(N * LogN) //Sort myList por el depth <- : IComparable : // 2) Ordena los nodos por su depth

            for (int i = 0; i<myList.Count; i++) //  Complexity: O(N) // 3) Itera los nodos de mayor a menor depth
            {
                TreeNode temp = myList[i];

                temp.height = HeightNode(temp); //--------------------//----> 3a) Calcula el height y balance factor de cada nodo
                temp.heightBalanceFactor = HeightBalanceFactor(temp); //

                bool didFixed = FixAVLPropertyOfNode(temp);//----------> 3b) Si un nodo requiere correccion, llama al metodo
                                                           //             FixAVLPropertyOfNode
                if (!didFixed)
                    SePudoArreglar =  false;//------------->Si en algun punto no fue posible corregirse, devuelve false
                                            //if didFixes = false, significa que el heightBalanceFactor es menor que -2 o mayor que 2
                                            // Se convierte false y se sigue iterando cada nodo de mayor a menor profundidad,
                                            // hasta que no quede nodo por iterar y al final return el value que quedara en "SePudoArreglar"
                                            // PD: Por lo tanto si se da el caso que no se pueda corregir por violentar la regla,
                                            //----> el metodo "TransformToAVLTree" hara un return false.

                if (temp.left != null)  
                {
                    temp.left.heightBalanceFactor = HeightBalanceFactor(temp.left);

                }
                if (temp.right != null)
                {
                    temp.right.heightBalanceFactor = HeightBalanceFactor(temp.right);
                }

            }
            return SePudoArreglar;        

            //Complexity --> O(N*H + N*LogN) */            
        }

        private List<TreeNode> InOrderIteration() // Complexity: O(N) 
        {
            List<TreeNode> myList = new List<TreeNode>();
            RecInOrderIteration(myList, root);

            return myList;
        }

        private void RecInOrderIteration(List<TreeNode> L, TreeNode root) // Complexity: O(N) 
        {
            if (root == null)
                return;
            RecInOrderIteration(L, root.left);
            L.Add(root);
            RecInOrderIteration(L, root.right);
        }
        private int depth(int key) // Complexity: O(H)
        {
            int contador = 0;
            TreeNode cur = root;
            while (cur != null)
            {

                if (key == cur.key)
                {
                    break;
                }
                if (key < cur.key)
                {
                    cur = cur.left;
                    contador++;
                }
                if (key > cur.key)
                {
                    cur = cur.right;
                    contador++;
                }
            }
            return contador;
        }

        private void RecDepth (TreeNode cur) // Complexity: O(N) 
        {
            if (cur == null)
                return;

            if (cur.parent != null)
                cur.depth = cur.parent.depth + 1;

            RecDepth(cur.right);
            RecDepth(cur.left);

        }

        private int RecHeight(TreeNode cur) // Complexity: O(N) 
        {
            if (cur == null)
                return -1;

            int HeightRight = RecHeight(cur.right);
            int HeightLeft = RecHeight(cur.left);

            cur.height = Math.Max(RecHeight(cur.right), RecHeight(cur.left)) + 1;

            return cur.height;
        }



        private int HeightNode(TreeNode x) // Complexity: O(1)
        {
            return Math.Max( getHeight(x.left) , getHeight(x.right) ) + 1;
        }

        private int HeightBalanceFactor(TreeNode x) // Complexity: O(1)
        {
            return getHeight(x.right) - getHeight(x.left);
        }

        /**
         * Convertir el arbol a un string (para debugging)
         **/
        override public string ToString()
        {
            if (root == null) return "";
            return root.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            NotAVLTree T = new NotAVLTree();

            T.Add(90);
            T.Add(100);
            T.Add(20);
            T.Add(10);
            T.Add(70);
            T.Add(30);
            T.Add(40);
            T.Add(50);
            T.Add(80);

            Console.WriteLine(T);
            // (90-(20-(10)-(70-(30-()-(40-()-(50)))-(80)))-(100))

            T.TransformToAVLTree();

            Console.WriteLine(T);
            // (40-(20-(10)-(30))-(90-(70-(50)-(80))-(100)))
            // NOTA: No estoy seguro si este output esta correcto :-P

            T.Remove(40);
            Console.WriteLine(T.Exists(40));  // False

            Console.WriteLine(T);
            // (50-(20-(10)-(30))-(90-(70-()-(80))-(100)))
            Console.ReadKey();

            /**************************************/
            /*      // <- QUITAR /* PARA VER OTRA PRUEBA CON OTRO BST.
           T.Add(80);
           T.Add(10);
           T.Add(50);
           T.Add(30);
           T.Add(15);
           T.Add(5);
           T.Add(70);
           T.Add(100);
           T.Add(120);
           T.Add(42);
           T.Add(13);
           T.Add(500);
           T.Add(9);

           Console.WriteLine(T);
           //(80-(10-(5-()-(9))-(50-(30-(15-(13)-())-(42))-(70)))-(100-()-(120-()-(500))))
           T.TransformToAVLTree();

           Console.WriteLine(T);
           //(30-(10-(5-()-(9))-(15-(13)-()))-(80-(50-(42)-(70))-(120-(100)-(500))))

           T.Remove(9);
           Console.WriteLine(T);
           //(30-(10-(5)-(15-(13)-()))-(80-(50-(42)-(70))-(120-(100)-(500))))

           T.Remove(10);
           Console.WriteLine(T);
           //(30-(13-(5)-(15))-(80-(50-(42)-(70))-(120-(100)-(500))))

           T.Remove(30);
           Console.WriteLine(T);
           //(42-(13-(5)-(15))-(80-(50-()-(70))-(120-(100)-(500))))
           Console.WriteLine(T.Remove(8));
           /**************************************/
           /**************************************/
            /*      // <- QUITAR /* PARA VER OTRA PRUEBA CON OTRO BST.
            T.Add(100);
            T.Add(10);
            T.Add(1);
            T.Add(50);
            T.Add(80);
            T.Add(25);
            T.Add(150);
            T.Add(130);
            T.Add(200);
            T.Add(20);
            T.Add(15);
            T.Add(75);
            Console.WriteLine(T);
            //(100 - (10 - (1) - (50 - (25 - (20 - (15) - ()) - ()) - (80 - (75) - ()))) - (150 - (130) - (200)))

            T.TransformToAVLTree();

            Console.WriteLine(T);
            //(50 - (10 - (1) - (20 - (15) - (25))) - (100 - (80 - (75) - ()) - (150 - (130) - (200))))

            T.Remove(100);
            Console.WriteLine(T);
            //(50 - (10 - (1) - (20 - (15) - (25))) - (130 - (80 - (75) - ()) - (150 - () - (200)))) */

        }
    }
}
