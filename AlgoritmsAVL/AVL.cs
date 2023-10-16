using System;

class AVLNode
{
    public int key;
    public AVLNode left;
    public AVLNode right;
    public int height;

    public AVLNode(int key)
    {
        this.key = key;
        this.left = null;
        this.right = null;
        this.height = 1;
    }
}

class AVLTree
{
    AVLNode root;

    public AVLTree()
    {
        this.root = null;
    }

    public void Insert(int key)
    {
        root = InsertHelper(root, key);
    }

    private AVLNode InsertHelper(AVLNode root, int key)
    {
        if (root == null)
        {
            return new AVLNode(key);
        }

        if (key < root.key)
        {
            root.left = InsertHelper(root.left, key);
        }
        else if (key > root.key)
        {
            root.right = InsertHelper(root.right, key);
        }

        root.height = 1 + Math.Max(GetHeight(root.left), GetHeight(root.right));

        int balanceFactor = GetBalance(root);

        if (balanceFactor > 1)
        {
            if (key < root.left.key)
            {
                return RightRotate(root);
            }
            else
            {
                root.left = LeftRotate(root.left);
                return RightRotate(root);
            }
        }

        if (balanceFactor < -1)
        {
            if (key > root.right.key)
            {
                return LeftRotate(root);
            }
            else
            {
                root.right = RightRotate(root.right);
                return LeftRotate(root);
            }
        }

        return root;
    }

    public AVLNode Delete(AVLNode root, int key)
    {
        if (root == null)
        {
            return root;
        }
        else if (key < root.key)
        {
            root.left = Delete(root.left, key);
        }
        else if (key > root.key)
        {
            root.right = Delete(root.right, key);
        }
        else
        {
            if (root.left == null)
            {
                AVLNode temp = root.right;
                root = null;
                return temp;
            }
            else if (root.right == null)
            {
                AVLNode temp = root.left;
                root = null;
                return temp;
            }

            AVLNode successor = GetSuccessor(root.right);
            root.key = successor.key;
            root.right = Delete(root.right, successor.key);
        }

        if (root == null)
        {
            return root;
        }

        root.height = 1 + Math.Max(GetHeight(root.left), GetHeight(root.right));

        int balanceFactor = GetBalance(root);

        if (balanceFactor > 1)
        {
            if (GetBalance(root.left) >= 0)
            {
                return RightRotate(root);
            }
            else
            {
                root.left = LeftRotate(root.left);
                return RightRotate(root);
            }
        }

        if (balanceFactor < -1)
        {
            if (GetBalance(root.right) <= 0)
            {
                return LeftRotate(root);
            }
            else
            {
                root.right = RightRotate(root.right);
                return LeftRotate(root);
            }
        }

        return root;
    }

    private AVLNode GetSuccessor(AVLNode node)
    {
        while (node.left != null)
        {
            node = node.left;
        }
        return node;
    }

    private int GetHeight(AVLNode node)
    {
        if (node == null)
        {
            return 0;
        }
        return node.height;
    }

    private int GetBalance(AVLNode node)
    {
        if (node == null)
        {
            return 0;
        }
        return GetHeight(node.left) - GetHeight(node.right);
    }

    private AVLNode RightRotate(AVLNode y)
    {
        AVLNode x = y.left;
        AVLNode T2 = x.right;

        x.right = y;
        y.left = T2;

        y.height = 1 + Math.Max(GetHeight(y.left), GetHeight(y.right));
        x.height = 1 + Math.Max(GetHeight(x.left), GetHeight(x.right));

        return x;
    }

    private AVLNode LeftRotate(AVLNode x)
    {
        AVLNode y = x.right;
        AVLNode T2 = y.left;

        y.left = x;
        x.right = T2;

        x.height = 1 + Math.Max(GetHeight(x.left), GetHeight(x.right));
        y.height = 1 + Math.Max(GetHeight(y.left), GetHeight(y.right));

        return y;
    }
    public void Visualize()
    {
        VisualizeHelper(root, "", true);
    }

    private void VisualizeHelper(AVLNode root, string indent, bool last)
    {
        if (root != null)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("└── ");
                indent += "    ";
            }
            else
            {
                Console.Write("├── ");
                indent += "│   ";
            }
            Console.WriteLine(root.key);
            VisualizeHelper(root.left, indent, false);
            VisualizeHelper(root.right, indent, true);
        }
    }

    public void InOrderTraversal()
    {
        InOrderTraversalHelper(root);
        Console.WriteLine();
    }

    private void InOrderTraversalHelper(AVLNode node)
    {
        if (node != null)
        {
            InOrderTraversalHelper(node.left);
            Console.Write($"{node.key} ");
            InOrderTraversalHelper(node.right);
        }
    }
}

class avl_run
{
    public static void Main(string[] args)
    {
        AVLTree avlTree = new AVLTree();

        int[] anArrayNodes = {
            17, 6, 5, 20, 19, 18, 11, 14, 12, 13, 2, 4, 10
        };

        for (int i = 0; i < anArrayNodes.Length; i++)
        {
            avlTree.Insert(anArrayNodes[i]);
        }

        avlTree.Visualize();

        Console.Write("In-order Traversal: ");
        avlTree.InOrderTraversal();
    }
}
