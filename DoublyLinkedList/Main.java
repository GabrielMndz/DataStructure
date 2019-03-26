public class Main {

    public static void main(String[] args) {

        DLinkedList<String> L2 = new DLinkedList<>();
        L2.append("A");
        L2.append("B");
        L2.append("C");
        L2.append("D");
        L2.append("E");
        L2.append("F");
        L2.append("G");
        L2.append("H");

        DLinkedList<String> L1 = new DLinkedList<>();
        L1.append("I");
        L1.append("J");
        L1.append("K");
        L1.append("L");
        L1.append("M");

        System.out.println("Listas originales");
        System.out.println("L1: " + L1);
        System.out.println("L2: " + L2);
        System.out.println();

        L1.prepend(L2);
        System.out.println("Despues de L1.prepend(L2)");
        System.out.println("L1: " + L1);
        System.out.println("L2: " + L2);
        System.out.println();

        DLinkedList<String> L3 = L1.subList(3, 7);
        System.out.println("Despues de L1.subList(3, 7)");
        System.out.println("L1: " + L1);
        System.out.println("L3: " + L3);
        System.out.println();

/*
Salida esperada:
Listas originales
L1: size = 5  elementos = ( I J K L M )
L2: size = 8  elementos = ( A B C D E F G H )

Despues de L1.prepend(L2)
L1: size = 13  elementos = ( A B C D E F G H I J K L M )
L2: size = 0  elementos = (  )

Despues de L1.subList(3, 7)
L1: size = 8  elementos = ( A B C I J K L M )
L3: size = 5  elementos = ( D E F G H )
*/

    }
}
