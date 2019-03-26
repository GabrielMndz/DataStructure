using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLinkedList
{
    class EmptyListException : System.Exception { }
    class CLinkedList
    {
         /*************by:
                        Gabriel Mendez Reyes -***************/
       

        // Nodo del Circular Linked List, identico al nodo de un Doubly Linked List
        class CLinkNode
        {
            public int value;
            public CLinkNode prev, next;
            public CLinkNode(int val)
            {
                value = val;
            }
        }

        CLinkNode curent;    // nodo actual en el Linked List
        public int size { get; private set; }        // cantidad de elementos en el Linked List


        /**
         *  Devuelve la cantidad de elementos grabados en la lista
         **/
        public int getSize()
        {
            return size;
        }
        /**
     * Devuelve el valor grabado en el nodo actual
     * @return        el valor en el nodo actual
     **/
        public int get()
        {
            if (size == 0)
                throw new EmptyListException();
            int ret = curent.value;
            curent = curent.next;
            return ret;
        }

        /**
 *  Agrega el valor val a la lista
 *  Side effects: El nodo actual se mueve al nuevo nodo creado
 *  @param  val  valor a agregar a la lista
 */

        public void add(int val)
        {
            if (size == 0)
            {
                CLinkNode newNode = new CLinkNode(val);
                newNode.next = newNode;
                newNode.prev = newNode;
                curent = newNode;
            }
            if (size == 1)
            {
                CLinkNode newNode = new CLinkNode(val);
                newNode.prev = curent;
                newNode.next = curent;
                curent.next = newNode;
                curent.prev = newNode;

            }
            if (size > 1)
            {
                CLinkNode newNode = new CLinkNode(val);
                newNode.prev = curent.prev;
                newNode.next = curent;
                curent.prev.next = newNode;
                curent.prev = newNode;
                

            }

            size++;
        }

        /**
         * Localiza el valor en la lista, reposicionando el nodo actual al nodo
         * que contiene dicho valor.  Si la lista contiene multiples valores
         * duplicados, puedes elegir cualquiera.
         * @param  val  valor a buscar en la lista
         * @return      true si lo encuentra; de lo contrario, devuelve false
         */
        public bool locate(int val)
        {
            for (int i = 0; i < size; i++)
            {
                if (curent.next.value == val)
                {
                    curent = curent.next;
                    return true;
                }

                curent = curent.next;
            }
            return false;
        }

        /**
 * Mueve el nodo actual 'cur' steps pasos.
 *    Si steps es positivo, se mueve hacia adelante
 *    Si steps es negativo, mueve hacia atras
 *    Si steps == 0, se queda donde esta
 * @param   la cantidad de pasos a moverse
 */
        public void moveTo(int steps)
        {
            if (steps == 0) { }

            if (steps > 0)
            {
                for (int i = 0; i < steps; i++)
                {
                    curent = curent.next;
                }
            }

            if (steps < 0)
            {
                int ret = steps * -1;
                for (int i = 0; i < ret; i++)
                {
                    curent = curent.prev;
                }
            }
        }

        /**
 *  Elimina el nodo acual y devuelve el valor grabado en dicho nodo
 *  Side effects: el nodo actual debe moverse a uno de los nodos adyacentes
 *                (si existe) del nodo borrado
 *  @return       el valor grabado en la posicion actual
 */
        public int remove()
        {
            if (size == 0)
                throw new EmptyListException();

            int ret = curent.value;

            if (size == 1)
            {
                curent = null;
                size = 0;
                return ret;
            }

            CLinkNode prev = curent.prev;
            CLinkNode next = curent.next;

            prev.next = next;
            next.prev = prev;
            size--;

            curent = next;
                   
            return ret;
        }

        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("size = ");
            sb.Append(size);
            sb.Append("  elementos = (");
            CLinkNode cur = curent;

            for (int i = 0; i < size; i++)
            {
                sb.Append(" ");
                sb.Append(cur.value);
                cur = cur.next;
            }

            sb.Append(" )");
            return sb.ToString();
        }
        
        public class Result
        {
            public int[] execution_order;
            public int[] survivors;
        }
        // TODO: usando los metodos definidos en el Circurlar Linked List,
        // implementa una solucion al problema de Flavio Josefo
        //
        // Debes simular el juego con los parametros N, K y S:
        // * Tenemos N personas paradas alrededor de un circulo, donde N es la
        //   cantidad de personas al inicio del juego.  Para identificacion,
        //   cada persona es numerada desde 1 a N, en orden de la manecilla
        //   del reloj.
        // * En el inicio, la persona #1 es ejecutada.
        // * A partir de ahi, ejecutan a cada K persona (es decir, brincamos K-1
        //   personas).
        // * La simulacion se repite hasta que queden S sobrevivivientes,
        //   quienes decidir no seguir jugando este macabro juego.
        //
        // Devuelve un objeto de la clase Result, donde:
        //  a) el atributo execution_order contiene el orden de ejecucion de las
        //     personas aniquilidas en el proceso
        //  b) el atributo survisors contiene una lista de los sobrevivientes
        //     (en cualquier orden)
        //
        // Ejemplo: Para N=41, K=3 y S=2:
        // execution_orden = {
        //    1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34, 37, 40, 3, 8, 12,
        //    17, 21, 26, 30, 35, 39, 5, 11, 18, 24, 32, 38, 6, 15, 27, 36,
        //    9, 23, 41, 20, 2, 33
        // }
        // survivors = { 14, 29 }
        //
        // Asume que los parametors N, K, S tienen valores validos
        //
        public static Result josephus(int N, int K, int S)
        {
            Result myResult = new Result();
            CLinkedList myCircularList = new CLinkedList();


            int cantidadVictimas = N - S;
            myResult.execution_order = new int[cantidadVictimas];
            myResult.survivors = new int[S];

            for (int i = 1; i <= N; i++)
            {
                myCircularList.add(i);
            }

            myResult.execution_order[0] = myCircularList.remove();

            for (int i = 1; i < myResult.execution_order.Length; i++)
            {
                myCircularList.moveTo(K - 1);
                myResult.execution_order[i] = myCircularList.remove();
            }

            for (int i = 0; i < myResult.survivors.Length; i++)
            {
                myResult.survivors[i] = myCircularList.get();
            }

            return myResult;

        }
        
        class Program
        {
            static void Main(string[] args)
            {
                /*
                CLinkedList myCircularList = new CLinkedList();
                int N = 20;
                for (int i = 1; i <= N; i++)
                {
                    myCircularList.add(i);
                }
                myCircularList.remove();
                myCircularList.moveTo(2);
                myCircularList.remove();
                */

                Result res1 = josephus(20, 3, 3);
                Console.WriteLine("Execution order: " +
                                   ToString(res1.execution_order));
                Console.WriteLine("Survivors: " +
                                   ToString(res1.survivors));

                Result res2 = josephus(41, 3, 2);
                Console.WriteLine("Execution order: " +
                                   ToString(res2.execution_order));
                Console.WriteLine("Survivors: " +
                                   ToString(res2.survivors));
                Console.WriteLine();

                // En el siguiente caso todos mueren
                Result res3 = josephus(3, 1, 0);
                Console.WriteLine("Execution order: " +
                                   ToString(res3.execution_order));
                Console.WriteLine("Survivors: " +
                                   ToString(res3.survivors));
                Console.WriteLine();


                Result res4 = josephus(7, 3, 1);
                Console.WriteLine("Execution order: " +
                                   ToString(res4.execution_order));
                Console.WriteLine("Survivors: " +
                                   ToString(res4.survivors));
                Console.WriteLine();

                Console.ReadLine();
                /*
               Resultado esperado:
                Execution order: 1 4 7 10 13 16 19 3 8 12 17 2 9 15 5 14 6
                Survivors: 11 18 20

                Execution order: 
                1 4 7 10 13 16 19 22 25 28 31 34 37 40 3 8 12 17 21 26 30 35 39 5 11 18 24 32 38 6 15 27 36 9 23 41 20 2 33
                Survivors: 14 29

                Execution order: 1 2 3
                Survivors:

                Execution order: 1 4 7 5 3 6
                Survivors: 2


               */
            }


            public static string ToString(int[] arr)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (i > 0)
                        sb.Append(' ');
                    sb.Append(arr[i]);
                }
                return sb.ToString();
            }
        }

        
    }
}
