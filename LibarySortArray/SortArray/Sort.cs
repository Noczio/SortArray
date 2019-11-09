using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtLibraries
{  
        public enum TypeOrder
        {
            Ascending,
            Descending,    
        }

        public enum TypeSort
        {
            Bubble,
            ImprovedBubble,
            Insertion
        }

        public class Sort<T>
        {
            #region Atributes
            private Node first;
            private Node last;
            private int lenght;
            #endregion

            #region Constructor & destructor
            public Sort()
            {
                this.first = null;
                this.last = null;
                this.lenght = 0;
            }

            ~Sort()
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
             
            #endregion

            #region Methods

            private bool Compare_Info(Node _info_1, Node _info_2, string _cmptype)
            {

                switch (_cmptype)
                {
                    case "=":
                        return Comparer<T>.Default.Compare(_info_1.info, _info_2.info) == 0;

                    case "<":
                        return Comparer<T>.Default.Compare(_info_1.info, _info_2.info) < 0;

                    case ">":
                        return Comparer<T>.Default.Compare(_info_1.info, _info_2.info) > 0;

                    default:
                        return false;
                }

            }

            public void Get_Array(T[] _array)
            {
                Clean(); // Cleans de queue, then gets a new one
                for (int i = 0; i < _array.Length; i++)
                {
                    this.Push(_array[i]);
                }
            }

            public T [] Return_Array()
            {           
                T[] array = new T[1];
                if (!Empty())
                {
                    Array.Resize<T>(ref array, this.lenght);
                    for (int i = 1; i <= this.lenght; i++)
                    {
                        Node auxnode = this.Find(i);                    
                        array[i - 1] = auxnode.info;
                    }
                }                
                    return array;                    
            }
         
            public void Sort_Array(TypeSort _sort, TypeOrder _order)
            {
                if (!Empty())
                {
                    switch (_sort)
                    {
                        case TypeSort.Bubble:

                            for (int index_1 = 1; index_1 < this.lenght; index_1++)
                            {
                                Node auxnode_1 = this.Find(index_1);
                                for (int index_2 = index_1 + 1; index_2 <= this.lenght; index_2++)
                                {
                                    Node auxnode_2 = this.Find(index_2);

                                    switch (_order)
                                    {
                                        case TypeOrder.Ascending:
                                            if (Compare_Info(auxnode_1, auxnode_2, ">") == true) // Ascending order
                                            {
                                                //Exchange values between nodes
                                                T temp = auxnode_2.info;
                                                auxnode_2.info = auxnode_1.info;
                                                auxnode_1.info = temp;
                                            }
                                            break;

                                        case TypeOrder.Descending:
                                            if (Compare_Info(auxnode_1, auxnode_2, "<") == true) // Descending order
                                            {
                                                //Exchange values between nodes
                                                T temp = auxnode_2.info;
                                                auxnode_2.info = auxnode_1.info;
                                                auxnode_1.info = temp;
                                            }
                                            break;
                                    }
                                }
                            }

                            break;

                        case TypeSort.ImprovedBubble:

                            bool swapIBubble = true;

                            for (int i = this.lenght; i >= 1 && swapIBubble; --i)
                            {
                                swapIBubble = false;

                                for (int j = 1; j < i; ++j)
                                {
                                    Node auxnode_1 = this.Find(j);
                                    Node auxnode_2 = this.Find(j + 1);

                                    switch (_order)
                                    {
                                        case TypeOrder.Ascending:
                                            if (Compare_Info(auxnode_1, auxnode_2, ">") == true)
                                            {
                                                swapIBubble = true;
                                                T temp = auxnode_1.info;
                                                auxnode_1.info = auxnode_2.info;
                                                auxnode_2.info = temp;
                                            }
                                            break;
                                        case TypeOrder.Descending:
                                            if (Compare_Info(auxnode_1, auxnode_2, "<") == true)
                                            {
                                                swapIBubble = true;
                                                T temp = auxnode_1.info;
                                                auxnode_1.info = auxnode_2.info;
                                                auxnode_2.info = temp;
                                            }
                                            break;
                                    }


                                }

                            }

                            break;

                    case TypeSort.Insertion:                       

                        for (int i = 1; i <= this.lenght-1; i++)
                        {                                                                      
                            for (int j = i+1; j > 1; j--)
                           {
                                Node auxnode_1 = this.Find(j);
                                Node auxnode_2 = this.Find(j - 1);

                                switch (_order)
                                {
                                    case TypeOrder.Ascending:
                                        if (this.Compare_Info(auxnode_2,auxnode_1,">")==true)
                                        {
                                            T temp = auxnode_2.info;
                                            auxnode_2.info = auxnode_1.info;
                                            auxnode_1.info = temp;
                                        }
                                        break;

                                    case TypeOrder.Descending:
                                        if (this.Compare_Info(auxnode_2, auxnode_1, "<") == true)
                                        {
                                            T temp = auxnode_2.info;
                                            auxnode_2.info = auxnode_1.info;
                                            auxnode_1.info = temp;
                                        }
                                        break;
                                }
                                
                            }

                                                                               
                        }

                        break;

                    }
                }
                else { Console.WriteLine("Elements not found"); }

            }

            private Node Find(int _position)
            {
                Node auxnode = null;
                if (!Empty())
                {
                    auxnode = this.first;
                    for (int i = 1; i <= this.lenght; i++)
                    {
                        if (_position == i)
                        {
                            break;
                        }
                        auxnode = auxnode.next;
                    }
                }

                return auxnode;
            }

            private T Front()
            {

                Node auxnode = new Node();

                if (!Empty())
                {

                    auxnode = this.first;

                }
                else
                {
                    auxnode = null;
                }


                return auxnode.info;

            }

            private T Back()
            {
                Node auxnode = new Node();

                if (!Empty())
                {
                    if (this.lenght == 1)
                    {
                        auxnode = this.first;
                    }
                    else
                    {
                        auxnode = this.last;
                    }

                }
                else
                {
                    auxnode = null;
                }


                return auxnode.info;

            }

            private void Pop()
                {
                    if (!Empty())
                    {
                        //Disassociates last node from the stack
                        Node auxnode = this.first;
                        this.first = this.first.next;
                        auxnode.next = null;
                        //deleted node in console
                        //Console.Write(auxnode.info);
                        //frees memory
                        Release(auxnode);
                        this.lenght -= 1;
                    }

                }

            //add a node to the stack
            private void Push(T _value)
            {
                Node nw = new Node(_value);
                nw.info = _value;

                if (this.first == null)
                {
                    //stack is empty
                    nw.next = null;
                    this.first = nw;
                    this.last = nw;

                }
                else
                {
                    //there's an existing node
                    this.last.next = nw;
                    this.last = nw;
                }

                //Stack counter
                this.lenght += 1;
            }

            public void Clean()
            {
                while (this.lenght > 0)
                {
                    this.Pop();
                }
            }

            private void Release(Node node)
            {
                node = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();

            }

            public void Print()
            {
                if (!Empty())
                {
                    Node auxnode = this.first;
                    // Iterate the queue
                    while (auxnode != null)
                    {
                        Console.WriteLine(auxnode.info);
                        // Take the next node
                        auxnode = auxnode.next;
                    }
                }
                else
                {
                    Console.WriteLine("There's not elements");
                }
            }

            public int Lenght()
            {
                return this.lenght;
            }

            private bool Empty()
            {
                return (this.lenght == 0);
            }

        #endregion

            #region Inner class

        private class Node
            {
            
            #region Atributes
            private Node _next;
            public Node next
            {
                get { return _next; }
                set { _next = value; }
            }

            // T as private member data type.          
            private T _info;

            // T as return type of property.            
            public T info
            {
                get { return _info; }
                set { _info = value; }
            }
            #endregion

            #region Constructor & destructor
            public Node(T info)
            {
                    _next = null;
                    _info = info;
            }

            public Node()
            {

            }

            ~Node()
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

            #endregion
        }

            #endregion
    }

}


