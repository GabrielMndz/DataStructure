class EmptyListException extends Exception { }

/**
 * Singly Linked List
    /*************by:
                      Gabriel Mendez Reyes -***************/

 */
public class SLinkedList {
    
    // Nodo del Linked List
    class SLinkNode
    {
        int value;                   // grabaremos valores de numeros enteros
        SLinkNode next;
        SLinkNode(int value)
        {
            this.value = value;
        }
    }

    private SLinkNode head, tail, prev;
    private int size;                // cantidad de elementos en el Linked List

    public int size()
    {
        return size;
    }
    public SLinkedList(){
        head = tail = null;
        size = 0;
    }

    /**
     * Agrega un elemento al final del singly linked list
     */
    public void addBack(int value) // addLast
    {
        if (size == 0){
            SLinkNode newNode = new SLinkNode(value);
            head = tail = newNode;
        }
        if (size == 1){
            SLinkNode newNode = new SLinkNode(value);
            head.next = newNode;
            tail = newNode;
        }
        if (size > 1){
            SLinkNode newNode = new SLinkNode(value);
            tail.next = newNode;
            tail= newNode;

        }
        size++;
    }

    /**
     * Convierte la lista a un String (para imprimir)
     */
    @Override
    public String toString()
    {
        StringBuffer sb = new StringBuffer();
        sb.append("size = ");
        sb.append(size);
        sb.append("  elementos = ( ");
        SLinkNode cur = head;
        while (cur != null)
        {
            if (cur != head)
                sb.append(' ');
            sb.append( cur.value );
            cur = cur.next;
        }
        sb.append(" )");
        return sb.toString();
    }


    /**
     * Localizar y eliminar el primer valor de la lista que coincide con el
     * parametro value.  En adicion, devuelve la posicion (contando desde 0)
     * donde se encontraba el elemento borrado .
     *
     * Si el valor no se encuentra en la lista, levanta la excepcion
     * NoSuchElementException.
     */
    int removeFirst() throws EmptyListException
    {
        // IMPLEMENTACION REMOVEFIRST SIN RECIBIR VALUE AS PARAMETER.
        if (size == 0)
            throw  new EmptyListException();
        
        if (head == tail){
            int ret = head.value;
            head = tail = null;
            return ret;
        }
        int ret = head.value;
        head = head.next;
        size--;

        return ret;
    }


    /**
     * Localizar y eliminar el ultimo valor de la lista que coincide con el
     * parametro value  En adicion, devuelve la posicion (contando desde 0)
     * donde se encontraba el elemento borrado.
     *
     * Si el valor no se encuentra en la lista, levanta la excepcion
     * NoSuchElementException.
     */
    int removeLast() throws EmptyListException
    {
        if (head == null)
            return 0;
        
        if (head == tail)            
            return removeFirst();

        prev = null;
        SLinkNode current = head;

        while (current != tail){
            prev = current;
            current = current.next;

        }
        prev.next = tail;
        tail = prev;
        size--;
        int value = current.value;      
        
        return value;
    }

}