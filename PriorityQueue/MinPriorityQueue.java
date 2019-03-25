class NoSuchElementException extends Exception {}

public class MinPriorityQueue < Item extends Comparable >
{
    Item[] heap;                 // arreglo que representa el Min Heap
    int size;                    // cantidad de datos en el Priority Queue

    public MinPriorityQueue(int cap)
    {
        heap = (Item[]) new Comparable[cap+1];  // reservamos un espacio adicional
                                                // para el dummy en la posicion 0
        size = 0;
    }

    /**
     * devuelve la cantidad de datos grabados en el Priority Queue
     **/
    public int size()
    {
        return size;
    }


    /**
     * devuelve el elemento mas prioritario del Min Priority Queue
     **/
    public Item peek() throws NoSuchElementException
    {
        if (size == 0)
            throw new NoSuchElementException();
        return heap[1];    // root siempre esta en la posicion 1
    }


    /**
     * Enforza que se cumplan las propiedades del Min Binary Heap navegando el
     * arbol desde la posicion pos hasta llegar a la raiz
     **/
    private void upHeap(int pos) throws NoSuchElementException
    {
        if (pos <= 0 || pos > size)
            throw new NoSuchElementException();

        int cur = pos;
        while (cur != 1) // si llega a uno esta en la raiz.
        {
            // [TODO] rompe el loop si llegamos a la raiz

            int par = cur/2;   // [TODO] indice del padre

            // [TODO] Comparamos los elementos en las posiciones cur y par
            //        Si la condicion del heap se cumple para esos elementos,
            //            no hay mas que hacer y detenemos el loop

            if (heap[par].compareTo(heap[cur]) > 0)
                break;

            // [TODO] Intercambiamos los elementos en los indices cur y par
            Item p = heap[par];
            Item c = heap[cur];
            heap[par] = c;
            heap[cur] = p;

            // [TODO] Sube en el arbol hacia el padre
            cur = par;
        }
    }

    /**
     * Enforza que se cumplan las propiedades del Min Binary Heap navegando el
     * arbol desde la posicion pos hasta llegar a una hoja
     **/
    public void downHeap(int pos) throws NoSuchElementException
    {
        if (pos <= 0 || pos > size)
            throw new NoSuchElementException();

        int cur = pos;
        while (true) {
            int left = 2* cur;     // [TODO] indice del hijo izquierdo
            int right = 2* cur + 1;    // [TODO] indice del hijo derecho

            // [TODO] Si no tiene hijos (es una hoja), detenemos el loop

            if (left > size && right > size)
                break;

            int min_idx;   // indice (left o right) del hijo mas prioritario

            // [TODO] Compara el hijo izquierdo versus el derecho y determina cual
            // es el mas prioritario.  Graba su posicion en min_idx.  Ojo: el hijo
            // derecho puede ser que NO exista, en cuyo caso el mas prioritario es
            // el izquierdo
            if (heap[right] == null){
                min_idx = left;
            }else
            if (heap[left].compareTo(heap[right]) > 0){
                min_idx = left;
            }else
                min_idx = right;


            // [TODO] Revisar si la propiedad de heap se cumple para los elementos
            // en los indices cur y min_idx.  Si no nay violacion de la propiedad
            // de heap, terminamos el loop
            if (heap[cur].compareTo(heap[min_idx]) > 0)
                break;

            // [TODO] Intercambiamos los elementos en los indices cur y min_idx
            Item c = heap[cur];
            Item hijo = heap[min_idx];

            heap[min_idx] = c;
            heap[cur] = hijo;

            // [TODO] Navega hacia el hijo mas prioritario
            cur = min_idx;
        }
    }

    /**
     * Agrega item en el Priority Queue
     **/
    public void enqueue(Item item) throws NoSuchElementException {
        // Usar el metodo upHeap para corregir la condicion de
        //              Min Heap


        if (size == 0){
            heap[1] = item;
            size = 1;
            return;
        }
        int cur = size + 1;
        int par = cur/2;

        heap[cur] = item;
        size++;

        if (heap[cur].compareTo(heap[par]) > 0){
            upHeap(cur);
        }


    }

    /**
     * Extrae y devuelve el elemento con mayor prioridad del Min Priority Queue
     **/
    public Item dequeue() throws NoSuchElementException {
        // Usar el metodo downHeap para corregir la condicion
        //              de Min Heap


        Item ret = heap[1];

        heap[1] = heap[size];
        downHeap(1);

        heap[size] = null;
        size--;

        return ret;
    }


    /**
     * Reemplaza el elemento en la posicion pos por el parametro item
     **/
    public void replace(int pos, Item item) throws NoSuchElementException
    {
        if (pos <= 0 || pos > size)
            throw new NoSuchElementException();

        int par = pos / 2;
        int left = 2* pos;
        int right = 2* pos + 1;

        heap[pos] = item;

        if (par == 0){ // replace el root
            if (heap[pos].compareTo(heap[left]) < 0 || heap[pos].compareTo(heap[right]) < 0 ){
                downHeap(pos);
            }
            return;
        }

        if (heap[pos].compareTo(heap[par]) > 0){
            upHeap(pos);
        }else
            downHeap(pos);
    }
}