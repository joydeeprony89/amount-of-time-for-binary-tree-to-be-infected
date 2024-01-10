namespace amount_of_time_for_binary_tree_to_be_infected
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            // https://leetcode.com/problems/amount-of-time-for-binary-tree-to-be-infected/description/
        }
    }

    
  //Definition for a binary tree node.
  public class TreeNode
    {
      public int val;
      public TreeNode left;
      public TreeNode right;
      public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
                 }
  }

    public class Solution
    {
        // Dictionary for the current node -> Parent mapping
        Dictionary<TreeNode, TreeNode> graph = new Dictionary<TreeNode, TreeNode>();
        // Visited hash
        HashSet<TreeNode> visited = new HashSet<TreeNode>();
        TreeNode infectedNode = null;
        public int AmountOfTime(TreeNode root, int start)
        {
            // lets create adjacency list, child->parent
            // during adjacency list creation we can mark the infected node
            // will start BFS from infected node(start) by pushing into Queue, using level order we should be able to find the answer 
            CreateGraph(root, null, start);
            var queue = new Queue<TreeNode>();
            queue.Enqueue(infectedNode);
            visited.Add(infectedNode);
            int height = 0;
            while (queue.Count > 0)
            {
                int size = queue.Count;
                while (size-- > 0)
                {
                    var infected = queue.Dequeue();
                    // at each level, for each infected node we can reach to left and right childs and also to parent node.
                    var parent = graph[infected];
                    // mark parent node of infected node as visited
                    if (parent != null && !visited.Contains(parent))
                    {
                        queue.Enqueue(parent);
                        visited.Add(parent);
                    }

                    // mark childs node of infected node as visited
                    if (infected.left != null && !visited.Contains(infected.left))
                    {
                        queue.Enqueue(infected.left);
                        visited.Add(infected.left);
                    }

                    if (infected.right != null && !visited.Contains(infected.right))
                    {
                        queue.Enqueue(infected.right);
                        visited.Add(infected.right);
                    }

                }
                height++;
            }

            return height - 1;
        }

        void CreateGraph(TreeNode current, TreeNode parent, int start)
        {
            if (current == null) return;
            if (current.val == start) infectedNode = current;
            if (!graph.ContainsKey(current)) graph.Add(current, parent);
            if (current.left != null) CreateGraph(current.left, current, start);
            if (current.right != null) CreateGraph(current.right, current, start);
        }
    }
}
