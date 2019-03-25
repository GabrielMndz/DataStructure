public class Main {

    public static void main(String[] args) throws NoSuchElementException {        


        MinPriorityQueue PQ = new MinPriorityQueue(25);

        PQ.enqueue(5);
        PQ.enqueue(-3);
        PQ.enqueue(23);
        PQ.enqueue(27);
        PQ.enqueue(24);
        PQ.enqueue(-3);
        PQ.enqueue(-3);
        PQ.enqueue(11);
        PQ.enqueue(42);
        PQ.enqueue(6);
        PQ.enqueue(8);
        PQ.enqueue(2);
        PQ.enqueue(10);

        System.out.println(PQ.peek());
        System.out.println(PQ.size);
        System.out.println(PQ.dequeue());
        System.out.println(PQ.size);

        PQ.replace(8, 21);
        PQ.replace(2,-2);
        System.out.println(PQ.dequeue());
        PQ.replace(1,50);
        System.out.println(PQ.peek());
        PQ.replace(1, -15);
        System.out.println(PQ.peek());
        PQ.replace(1, 25);
        PQ.replace(1,8);

    /*
    Output esperado:
        .peek() = 42
        .size() = 13
        .dequeue() = 42
        .size() = 12
        .dequeue() = 27
        .peek() = 50
        .peek() = 21
    */       

    }
}
