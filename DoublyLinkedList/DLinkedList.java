// Doubly Linked List, definido con Generics

public class DLinkedList <Value> {
    /**
     * Nodo del Linked List
     */
    class DLinkNode<Value>
    {
        Value value;
        DLinkNode<Value> prev, next;
        DLinkNode(Value value)
        {
            this.value = value;
        }
    }

    private DLinkNode<Value> head, tail;   // head y tail apuntan a dummy nodes
    private int size;                // cantidad de elementos en el Linked List

    public DLinkedList()
    {
        head = new DLinkNode<Value>(null);   // dummy head
        tail = new DLinkNode<Value>(null);   // dummy tail
        head.next = tail;
        tail.prev = head;
        size = 0;
    }

    public int size()
    {
        return size;
    }


    /**
     * Agrega un elemento al final del doubly linked list
     */
    public void append(Value value)
    {
        DLinkNode<Value> newNode = new DLinkNode<>(value);
        newNode.next = tail;
        newNode.prev = tail.prev;
        newNode.prev.next = newNode;
        newNode.next.prev = newNode;
        size++;
    }


    /**
     * Convertir el Linked List a un String para ser impreso en la consola
     */
    @Override
    public String toString()
    {
        StringBuffer sb = new StringBuffer();
        sb.append("size = ");
        sb.append(size);
        sb.append("  elementos = ( ");
        DLinkNode<Value> cur = head.next;
        while (cur != tail)
        {
            if (cur != head.next)
                sb.append(' ');
            sb.append( cur.value );
            cur = cur.next;
        }
        sb.append(" )");
        return sb.toString();
    }


    /**
     * Insertar la lista L al inicio de la lista 'this'
     * Luego de la operacion, la lista L debe quedar vacia.
     * Ejemplo: si la lista L1 tiene A, B, C y la lista L2 tiene D, E,
     * L1.prepend(L2)
     */
    void prepend(DLinkedList<Value> L2)
    {
        L2.tail.prev.next = head.next;
        head.next.prev = L2.tail.prev;
        head = L2.head;
        L2.tail = tail;


        L2.head = new DLinkNode<Value>(null);
        L2.tail = new DLinkNode<Value>(null);
        L2.head.next = L2.tail;
        L2.tail.prev = L2.head;

        size += L2.size;
        L2.size = 0;
    }


    /**
     * Extraer los elementos desde la posicion 'start' hasta la posicion 'end' y
     * devolverlo en un nuevo DLinkedList.  Los elementos extraidos desaparecen
     * de la lista 'this'.
     * Ejemplo: si la lista L contiene, en orden, A B C D E F G H I J K L M,
     * L.subList(3, 7) devuelve la lista D E F G H y deja la lista con los
     * valores A B C I J K L M
     */
    public DLinkedList<Value> subList(int start, int end)
    {
            if (start < 0 || start >= size || end < start || end >= size)
                throw new IndexOutOfBoundsException();

            DLinkedList<Value> myFirstPart = new DLinkedList<>();
            DLinkedList<Value> mySeconPart = new DLinkedList<>();
            DLinkedList<Value> mySubList = new DLinkedList<>();
            head = head.next;
            for (int i = 0; i < start ; i++){
                myFirstPart.append(head.value);
                head = head.next;
            }
            for (int i = start; i <= end; i++){
                mySubList.append(head.value);
                head = head.next;
            }
            for (int i = end; i < size-1 ; i++){
                mySeconPart.append(head.value);
                head = head.next;
            }
            //L1.prepend(L2);
            mySeconPart.prepend(myFirstPart);

            mySeconPart.head = mySeconPart.head.next;

            head = new DLinkNode<Value>(null);
            tail = new DLinkNode<Value>(null);
            head.next = tail;
            tail.prev = head;
            size = 0;

            for (int i= 0 ; i< mySeconPart.size; i++){
                DLinkNode<Value> newNode = new DLinkNode<>(mySeconPart.head.value);
                newNode.next = tail;
                newNode.prev = tail.prev;
                newNode.prev.next = newNode;
                newNode.next.prev = newNode;
                size++;
                mySeconPart.head = mySeconPart.head.next;
            }
            return mySubList;
    }
}
