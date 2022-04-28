using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    interface ITwoThree<T>
    {

        public Node<T> Insert(Node<T> node, T value);
        public Node<T> Split(ref Node<T> nodeToSplit, T valueToInsert);

        public T Search(Node<T> node,  T value);

        public void Edit(Node<T> node,  T value, Action<T,T> Edit);

    }
}
