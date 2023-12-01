public static class Puzzle20
{
    public class Node {
        public Node(long value) {
            this.value = value;
            this.next = this;
            this.prev = this;
        }
        public long value;
        public Node next;
        public Node prev;
    }

    public static int Part1(string[] lines)
    {
        Node[] numbers = lines.Select(l=>new Node(int.Parse(l))).ToArray();
        for (int n=1; n<numbers.Length-1; n++) {
            numbers[n].prev = numbers[n-1];
            numbers[n].next = numbers[n+1];
        }
        numbers[0].next = numbers[1];
        numbers[0].prev = numbers[numbers.Length-1];
        numbers[numbers.Length-1].next = numbers[0];
        numbers[numbers.Length-1].prev = numbers[numbers.Length-2];
        
        Node zero = numbers[0];
        foreach (Node n in numbers) {
            if (n.value == 0) {
                zero = n;
                continue;
            }
            n.next.prev = n.prev;
            n.prev.next = n.next;
            if (n.value<0) {
                Node prevNode = n.prev;
                for (int i=0; i>n.value; i--) {
                    prevNode = prevNode.prev;
                }
                Node nextNode = prevNode.next;
                prevNode.next = n;
                nextNode.prev = n;
                n.next = nextNode;
                n.prev = prevNode;
            }
            else {
                Node nextNode = n.next;
                for (int i=0; i<n.value; i++) {
                    nextNode = nextNode.next;
                }
                Node prevNode = nextNode.prev;
                prevNode.next = n;
                nextNode.prev = n;
                n.next = nextNode;
                n.prev = prevNode;
            }
        }
        int sum=0;
        for (int i=1; i<3001; i++) {
            zero = zero.next;
            if (i%1000 == 0) sum+=(int)zero.value;
        }
        return sum;
    }

    public static long Part2(string[] lines)
    {
        Node[] numbers = lines.Select(l=>new Node(long.Parse(l)*811589153)).ToArray();
        for (int n=1; n<numbers.Length-1; n++) {
            numbers[n].prev = numbers[n-1];
            numbers[n].next = numbers[n+1];
        }
        int numCount = numbers.Length;
        numbers[0].next = numbers[1];
        numbers[0].prev = numbers[numCount-1];
        numbers[numCount-1].next = numbers[0];
        numbers[numCount-1].prev = numbers[numCount-2];
        
        Node zero = numbers[0];
        for (int m=0; m<10; m++) {
            foreach (Node n in numbers) {
                if (n.value == 0) {
                    zero = n;
                    continue;
                }
                n.next.prev = n.prev;
                n.prev.next = n.next;
                if (n.value<0) {
                    Node prevNode = n.prev;
                    long shift = (n.value*-1) % (numCount-1);
                    for (long i=0; i<shift; i++) {
                        prevNode = prevNode.prev;
                    }
                    Node nextNode = prevNode.next;
                    prevNode.next = n;
                    nextNode.prev = n;
                    n.next = nextNode;
                    n.prev = prevNode;
                }
                else {
                    Node nextNode = n.next;
                    long shift = n.value % (numCount-1);
                    for (long i=0; i<shift; i++) {
                        nextNode = nextNode.next;
                    }
                    Node prevNode = nextNode.prev;
                    prevNode.next = n;
                    nextNode.prev = n;
                    n.next = nextNode;
                    n.prev = prevNode;
                }
            }
        }
        long sum=0;
        for (int i=1; i<3001; i++) {
            zero = zero.next;
            if (i%1000 == 0) sum+=zero.value;
        }
        return sum;
    }
}