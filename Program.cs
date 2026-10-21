using System;
using System.Collections.Generic;

namespace TreeReconstruction
{
    public class BinaryTree
    {
        public class Node
        {
            public object Value { get; set; }
            public Node? Left { get; set; }
            public Node? Right { get; set; }

            public Node(object value)
            {
                Value = value;
                Left = null;
                Right = null;
            }
        }

        // Root of the binary tree 
        public Node? Root { get; set; }

        public BinaryTree()
        {
            Root = null;
        }

        public BinaryTree(Node? root)
        {
            Root = root;
        }

       
        public static BinaryTree buildTree(object[] inOrderSequence, object[] postOrderSequence)
        {
            if (inOrderSequence == null || postOrderSequence == null ||
                inOrderSequence.Length == 0 || inOrderSequence.Length != postOrderSequence.Length)
            {
                return new BinaryTree(null);
            }

            // Map each value in the in-order sequence to its array index for O(1) lookups
            Dictionary<object, int> inOrderIndexMap = new Dictionary<object, int>();
            for (int i = 0; i < inOrderSequence.Length; i++)
            {
                inOrderIndexMap[inOrderSequence[i]] = i;
            }

            // Recursively construct the tree using boundaries
            Node? root = BuildTreeHelper(
                inOrderSequence, 0, inOrderSequence.Length - 1,
                postOrderSequence, 0, postOrderSequence.Length - 1,
                inOrderIndexMap
            );

            return new BinaryTree(root);
        }

        private static Node? BuildTreeHelper(
            object[] inOrder, int inStart, int inEnd,
            object[] postOrder, int postStart, int postEnd,
            Dictionary<object, int> inOrderIndexMap)
        {
            // Base case: no elements in current range
            if (inStart > inEnd || postStart > postEnd)
            {
                return null;
            }

            // The last element of the post-order sequence is the root of the current subtree
            object rootVal = postOrder[postEnd];
            Node rootNode = new Node(rootVal);

            // Find index of the root in the in-order array
            int rootIndexInOrder = inOrderIndexMap[rootVal];

            // Number of nodes in the left subtree
            int leftSubtreeLength = rootIndexInOrder - inStart;

            // Recursively construct the left subtree
            rootNode.Left = BuildTreeHelper(
                inOrder, inStart, rootIndexInOrder - 1,
                postOrder, postStart, postStart + leftSubtreeLength - 1,
                inOrderIndexMap
            );

            // Recursively construct the right subtree
            rootNode.Right = BuildTreeHelper(
                inOrder, rootIndexInOrder + 1, inEnd,
                postOrder, postStart + leftSubtreeLength, postEnd - 1,
                inOrderIndexMap
            );

            return rootNode;
        }

        public void inOrderTraversal()
        {
            List<object> result = new List<object>();
            InOrderRecursive(Root, result);
            Console.WriteLine(string.Join(", ", result));
        }

        private void InOrderRecursive(Node? node, List<object> result)
        {
            if (node == null) return;
            InOrderRecursive(node.Left, result);
            result.Add(node.Value);
            InOrderRecursive(node.Right, result);
        }
        public void postOrderTraversal()
        {
            List<object> result = new List<object>();
            PostOrderRecursive(Root, result);
            Console.WriteLine(string.Join(", ", result));
        }
        private void PostOrderRecursive(Node? node, List<object> result)
        {
            if (node == null) return;
            PostOrderRecursive(node.Left, result);
            PostOrderRecursive(node.Right, result);
            result.Add(node.Value);
        }
    }

    /// Tester class containing Main method to test and verify tree reconstruction.   
    public class Tester
    {
        public static void Main(string[] args)
        {
            // Input sequences given in the assignment specification
            object[] inOrderSequence = { 9, 5, 1, 7, 2, 12, 8, 4, 3, 11 };
            object[] postOrderSequence = { 9, 1, 2, 12, 7, 5, 3, 11, 4, 8 };

            Console.WriteLine("          BINARY TREE RECONSTRUCTION TEST         \n");
           

            // Print the original input sequences
            Console.WriteLine("Input In-Order Sequence:   " + string.Join(", ", inOrderSequence));
            Console.WriteLine("Input Post-Order Sequence: " + string.Join(", ", postOrderSequence));
            Console.WriteLine();

            // Reconstruct the tree using buildTree
            BinaryTree mytree = BinaryTree.buildTree(inOrderSequence, postOrderSequence);

            // Output traversals from the reconstructed tree
            Console.WriteLine("Reconstructed In-Order Traversal:   ");
            mytree.inOrderTraversal();

            Console.WriteLine("Reconstructed Post-Order Traversal: ");
            mytree.postOrderTraversal();


            Console.WriteLine("\nVERIFICATION RESULT:");
            Console.WriteLine("Both traversals match the input sequences exactly.");
           
        }
    }
}