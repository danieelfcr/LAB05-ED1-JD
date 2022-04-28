using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class TwoThreeTree<T> : ITwoThree<T>
    {
        public Node<T> Root;
        public int NodeCount;
        public Func<T, T, int> Comparer;
        public List<T> ElementList;

        public TwoThreeTree(Func<T,T, int> Comparer)
        {
            this.Comparer = Comparer;
            Root = null;
            ElementList = new List<T>();
            NodeCount = 0;
        }

      

        public Node<T> Insert(Node<T> node, T value)
        {
            if (node == null)
            {
                //the node doesn't exists, a new node is created to store the value to insert
                Node<T> NewNode = new Node<T>(Comparer);
                NewNode.AddValue(value);
                ElementList.Add(value);
                return NewNode;
            }
            else if (node.IsLeaf())
            {
                //the insertion can be applied only in leaf nodes
                if (node.IsAvailable())
                {
                    node.AddValue(value);
                    ElementList.Add(value);
                }
                else
                {
                    //there's no more space in the node, a split is required
                    node.AddValue(value);
                    ElementList.Add(value);

                    return Split(ref node, node.middletValue);
                    
                    //return Root;
                }
            }
            else
            {
                //a leaf to store the value must be found recursively
                if (Comparer(value, node.LeftValue) == -1)
                {
                    //searching in the left child of the node
                    
                    var aux = Insert(node.LeftChild, value);
                    if (aux != node)
                        node.LeftChild = aux; 
                }
                else if (Comparer(value, node.LeftValue) == 1 && (node.RightValue != null && Comparer(value, node.RightValue) == -1 ))
                {
                    //searching in the middle child of the node
                    var aux = Insert(node.MiddleChild, value);
                    if (aux != node)
                        node.MiddleChild = aux;
                }
                else if (node.RightValue == null || Comparer(value, node.RightValue) == 1)
                {
                    //searching in the right child of the node
                    var aux = Insert(node.RightChild, value);
                    if (aux != node)
                        node.RightChild = aux;
                }
                
            }

            return node;
        }

       

        public Node<T> Split(ref Node<T> nodeToSplit, T valueToInsert)
        {
            Node<T> newParent = null;

            if (nodeToSplit == Root)
            {

                
                Node<T> left = new Node<T>(Comparer, ref nodeToSplit.LeftChild, nodeToSplit.LeftValue, -1);
                Node<T> right = new Node<T>(Comparer, ref nodeToSplit.RightChild, nodeToSplit.RightValue, 1);

                newParent = new Node<T>(Comparer, nodeToSplit.middletValue, default(T), left, right);
                left.Parent = newParent;
                right.Parent = newParent;
                
                return newParent;
            }
            else
            {
                if (nodeToSplit.Parent.IsAvailable())
                {
                    nodeToSplit.Parent.AddValue(nodeToSplit.middletValue);
                    nodeToSplit.middletValue = default(T);

                    Node<T> leftCopy = new Node<T>(Comparer, ref nodeToSplit.LeftChild, nodeToSplit.LeftValue, -1);
                    Node<T> rightCopy = new Node<T>(Comparer, ref nodeToSplit.RightChild, nodeToSplit.RightValue, 1);

                    

                    nodeToSplit.Parent.DeleteChild(nodeToSplit);
                    nodeToSplit.Parent.AddChild(ref leftCopy);
                    nodeToSplit.Parent.AddChild(ref rightCopy);
                    leftCopy.Parent = nodeToSplit.Parent;
                    rightCopy.Parent = nodeToSplit.Parent;

                    return nodeToSplit.Parent;
                }
                else
                {
                    Node<T> leftCopy = new Node<T>(Comparer, ref nodeToSplit.LeftChild, nodeToSplit.LeftValue, -1);
                    Node<T> rightCopy = new Node<T>(Comparer, ref nodeToSplit.RightChild, nodeToSplit.RightValue, 1);


                    nodeToSplit.Parent.AddValue(valueToInsert);
                    var node = Split(ref nodeToSplit.Parent, valueToInsert);

                    node.AddChild(ref leftCopy);
                    node.AddChild(ref rightCopy);
                    leftCopy.Parent = node;
                    rightCopy.Parent = node;
                }
            }

            return newParent;
            
        }

        public T Search(Node<T> node, T value)
        {
            if (node == null)
            {
                return default;
            }
            if (node.ContainsValue(value) != 0)
            {
                if (node.ContainsValue(value) == -1)
                    return node.LeftValue;
                else return node.RightValue;
            }
            else
            {
                if (Comparer(value, node.LeftValue) == -1)
                {
                    Search(node.LeftChild, value);
                }
                else if (Comparer(value, node.LeftValue) == 1 && Comparer(value, node.RightValue) == -1)
                {
                    Search(node.MiddleChild, value);
                }
                else if (Comparer(value, node.RightValue) == 1)
                {
                    Search(node.RightChild, value);
                }
            }


            return default;
        }

        public void Edit(Node<T> node, T value)
        {
            throw new NotImplementedException();
        }


        /*
        public void Split(ref Node<T> NodeToSplit)
        {
            if (NodeToSplit == Root)
            {
                //creation of a 2-node with the middle value
                Node<T> newParent = new Node<T>(Comparer);
                newParent.AddValue(NodeToSplit.middletValue);
                NodeToSplit.middletValue = default(T);

                Node<T> LeftCopy = new Node<T>(Comparer, ref NodeToSplit.LeftChild, NodeToSplit.LeftValue);
                Node<T> RightCopy = new Node<T>(Comparer, ref NodeToSplit.RightChild, NodeToSplit.RightValue);

                LeftCopy.Parent = newParent;
                RightCopy.Parent = newParent;

                // child pointers modifications
                newParent.LeftChild = LeftCopy;
                newParent.RightChild = RightCopy;

                newParent.RedistributeChildren();

                NodeToSplit = null;
                Root = newParent;
            }
            else
            {
                if (NodeToSplit.Parent.IsAvailable())
                {
                    NodeToSplit.Parent.AddValue(NodeToSplit.middletValue);
                    NodeToSplit.middletValue = default(T);

                    Node<T> LeftCopy = new Node<T>(Comparer, ref NodeToSplit.LeftChild, NodeToSplit.LeftValue);
                    Node<T> RightCopy = new Node<T>(Comparer, ref NodeToSplit.RightChild, NodeToSplit.RightValue);

                    LeftCopy.Parent = NodeToSplit.Parent;
                    RightCopy.Parent = NodeToSplit.Parent;

                    NodeToSplit.Parent.LeftChild = LeftCopy;
                    NodeToSplit.Parent.RightChild = RightCopy;

                    NodeToSplit.Parent.RedistributeChildren();
                    NodeToSplit = null;
                }
                else
                {

                }
            }
        }*/

        /*
        public void Split(ref Node<T> NodeToSplit, T ValueToInsert)
        {
            if (NodeToSplit.Parent == null && NodeToSplit == Root)
            {
                //base case: the node to split is the root, a new node is created
                Node<T> NewParent = new Node<T>(Comparer);
                NewParent.LeftValue = NodeToSplit.middletValue;

                //creation of two new nodes to divide the original node
                Node<T> Right = new Node<T>(Comparer);
                Node<T> Left = new Node<T>(Comparer);

                Left.LeftValue = NodeToSplit.LeftValue;
                Right.LeftValue = NodeToSplit.RightValue;

                NewParent.LeftChild = Left;
                NewParent.RightChild = Right;
                Left.Parent = NewParent;
                Right.Parent = NewParent;

                Root = NewParent;
            }
            else if (NodeToSplit.IsAvailable())
            {
                NodeToSplit.AddValue(ValueToInsert);

                Node<T> Right = new Node<T>(Comparer);
                Node<T> Left = new Node<T>(Comparer);

                Left.LeftValue = NodeToSplit.LeftValue;
                Right.LeftValue = NodeToSplit.RightValue;

                NodeToSplit.LeftChild = Left;
                NodeToSplit.RightChild = Right;
                Left.Parent = NodeToSplit;
                Right.Parent = NodeToSplit;
            }
            else
            {
                Split(ref NodeToSplit.Parent, NodeToSplit.middletValue);
            }
        }*/

    }
}
