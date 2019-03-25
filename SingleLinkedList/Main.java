/*
 *
 * Puntos ejercicio: 8 + 1 extra
 *
 * En este ejercicio, implementaremos algunas operaciones sobre un Singly
 * Linked List.  Las operaciones estan marcadas con el tag TODO.  En estas
 * operaciones, solo se permite cambiar el campo next de SLinkNode.
 *
 * No se permite:
 *   1) Crear arreglos
 *   2) Alterar el value en SLinkNode
 *
 * Instrucciones adicionales:
 *   a) No borren los comments que existen en este codigo
 *   b) No se permite usar las estructuras de datos en java.util.*
 *   c) Se permite agregar campos adicionales en la declaracion de las clases,
 *      pero no usar dummy nodes
 *   d) Se permite agregar parametros adicionales a los metodos
 *
 */
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
