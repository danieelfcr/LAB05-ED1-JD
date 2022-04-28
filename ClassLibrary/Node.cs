using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Node<T>
    {
        public T LeftValue;
        public T middletValue;
        public T RightValue;

        public Node<T> LeftChild;
        public Node<T> MiddleChild;
        public Node<T> RightChild;

        public Node<T> Parent;
        public Func<T, T, int> Comparer;


        //Constructor of an empty node
        public Node(Func<T, T, int> Comparer)
        {
            this.Comparer = Comparer;
            LeftChild = null;
            MiddleChild = null;
            RightChild = null;
        }

        //constructor of a 3-node without childs
        public Node(Func<T, T, int> Comparer, T LeftValue, T RightValue)
        {
            this.Comparer = Comparer;
            this.LeftValue = LeftValue;
            this.RightValue = RightValue;
            LeftChild = null;
            MiddleChild = null;
            RightChild = null;
        }

        //Constructor of a 3-node with left and middle childs
        public Node(Func<T, T, int> Comparer, T LeftValue, T RightValue, Node<T> left, Node<T> right)
        {
            this.Comparer = Comparer;
            this.LeftValue = LeftValue;
            this.RightValue = RightValue;
            LeftChild = left;
            RightChild = right;
            //RightChild = null;
        }


        public Node(Func<T, T, int> Comparer, ref Node<T> child, T value, int position)
        {
            this.Comparer = Comparer;

            
            if (child != null)
            {
                child.Parent = this;
                AddChild(ref child);
            }
               

            /*
            if (position == -1) //creates a copy of left subtree
            {
                

                if (child != null)
                {
                    child.Parent = this;
                    LeftChild = child;
                }


                
            }
            else  //creates a copy of right subtree
            {

                if (child != null)
                {
                    child.Parent = this;
                    RightChild = child;
                }


                
            }*/

            LeftValue = value;

        }

        /*

        public Node(Func<T, T, int> Comparer, ref Node<T> Left, ref Node<T> Right, T RValue)
        {
            this.Comparer = Comparer;
           
            if (Right != null)
            {
                Right.Parent = this;
                RightChild = Right;
            }
                
            
            RightValue = RValue;
        }*/
            
            

        public bool IsLeaf()
        {
            return LeftChild == null && MiddleChild == null && RightChild == null;
        }

        public bool is2Node()
        {
            return RightValue == null;
        }

        public bool is3Node()
        {
            return RightValue != null;
        }


        public bool IsAvailable()
        {
            return LeftValue == null || RightValue == null;
        }
        
        public void AddChild(ref Node<T> newChild)
        {
            if (newChild != null)
            {

                if (is2Node())
                {
                    //the node only has two options for childrens, left and right
                    if (LeftChild == null)
                    {
                        LeftChild = newChild;
                    }
                    else if (RightChild == null)
                    {
                        if (Comparer(newChild.LeftValue, LeftValue) == 1)
                        {
                            RightChild = newChild;
                        }
                        else
                        {
                            RightChild = LeftChild;
                            LeftChild = newChild;
                        }
                               
                    }
                }
                else if (is3Node())
                {
                    //the node has the three options for childresn, left, middle and right
                    if (MiddleChild == null)
                    {
                        if (Comparer(newChild.LeftValue, LeftValue) == 1 && Comparer(newChild.LeftValue, RightValue) == -1)
                        {
                            MiddleChild = newChild;
                        }
                        else if (Comparer(newChild.LeftValue, LeftValue) == -1)
                        {
                            MiddleChild = LeftChild;
                            LeftChild = newChild;
                        }
                        else if (Comparer(newChild.LeftValue, RightValue) == 1)
                        {
                            MiddleChild = RightChild;
                            RightChild = newChild;
                        }
                    }
                }


                /*
                if (LeftChild == null)
                {
                    LeftChild = newChild;
                }
                else if (RightChild == null && MiddleChild == null)
                {
                    if (Comparer(newChild.LeftValue, LeftValue) == 1)
                    {
                        RightChild = newChild;
                    }
                    else
                    {
                        RightChild = LeftChild;
                        LeftChild = newChild;
                    }
                }
                else if (RightChild != null &&) */
            }

            /*
            if (middletValue != null &&  Comparer(LeftChild.LeftValue,RightChild.LeftValue) == -1 && Comparer(LeftChild.LeftValue, MiddleChild.LeftValue) == 1)
            {
                var aux = LeftChild;
                LeftChild = MiddleChild;
                MiddleChild = aux;
            }
            else if (middletValue != null &&  Comparer(LeftChild.LeftValue, MiddleChild.LeftValue) == -1)
            {
                if (Comparer(MiddleChild.LeftValue, RightChild.LeftValue) == 1)
                {
                    var aux = MiddleChild;
                    MiddleChild = RightChild;
                    RightChild = aux;
                }
            }
            else if (Comparer(LeftChild.LeftValue, RightChild.LeftValue) == 1)
            {
                var aux = LeftChild;
                LeftChild = RightChild;
                RightChild = aux;
            }*/
        }

        public void DeleteChild(Node<T> child)
        {
            if (LeftChild == child)
                LeftChild = null;
            else if (MiddleChild != null && MiddleChild == child)
                MiddleChild = null;
            else
                RightChild = null;
        }
        public void AddValue(T value)
        {
            //Search the place where the value must be stored

            if (RightValue == null && LeftValue == null)
            {
                LeftValue = value;
            }
            else if (RightValue == null || LeftValue == null)
            {
               
                if (Comparer(value, LeftValue) == 1)
                {
                    RightValue = value;
                }
                else if (Comparer(value, LeftValue) == -1)
                {
                    RightValue = LeftValue;
                    LeftValue = value;
                }
            }
            else
            {
                //middle value used to split the node
                if (Comparer(value, LeftValue) == 1 && Comparer(value, RightValue) == -1)
                {
                    middletValue = value;
                }
                else if (Comparer(value, LeftValue) == -1)
                {
                    middletValue = LeftValue;
                    LeftValue = value;
                }
                else if (Comparer(value, RightValue) == 1)
                {
                    middletValue = RightValue;
                    RightValue = value;
                }
            }
            
        }
    }
}
