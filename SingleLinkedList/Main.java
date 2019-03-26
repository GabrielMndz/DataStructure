public class Main {

    public static void main(String[] args) throws EmptyListException {


        SLinkedList L = new SLinkedList();
        L.addBack(2);
        L.addBack(7);
        L.addBack(1);
        L.addBack(8);
        L.addBack(2);
        L.addBack(8);
        L.addBack(1);
        L.addBack(8);
        L.addBack(2);
        L.addBack(8);
        L.addBack(4);
        L.addBack(5);
        L.addBack(9);
        System.out.println("Lista original");
        System.out.println("L: " + L);
        System.out.println();

        int value = L.removeFirst();
        System.out.println("Despues de removeFirst()");
        System.out.println("L: " + L);
        System.out.println("El primer elemento fue eliminado con el valor " + value);
        System.out.println();

        value = L.removeLast();
        System.out.println("Despues de removeLast()");
        System.out.println("L: " + L);
        System.out.println("El ultimo elemento fue eliminado con el valor " + value);
        System.out.println();

        /*
        Resultado esperado:
        Lista original
        L: size = 13  elementos = ( 2 7 1 8 2 8 1 8 2 8 4 5 9 )

        Despues de removeFirst()
        L: size = 12  elementos = ( 7 1 8 2 8 1 8 2 8 4 5 9 )
        El primer elemento fue eliminado con el valor 2

        Despues de removeLast()
        L: size = 11  elementos = ( 7 1 8 2 8 1 8 2 8 4 5 9 )
        El ultimo elemento fue eliminado con el valor 9

        */


    }
}
