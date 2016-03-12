﻿namespace codingame.skynet.the.virus
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Text;
    using System.Collections;
    using System.Collections.Generic;

    /**
     * Auto-generated code below aims at helping you parse
     * the standard input according to the problem statement.
     **/
    class Player
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int NODES = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
            int L = int.Parse(inputs[1]); // the number of links
            int E = int.Parse(inputs[2]); // the number of exit gateways
            var links = new Dictionary<int, int>();
            for (int i = 0; i < L; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
                int N2 = int.Parse(inputs[1]);
                links.Add(int.Parse(inputs[0]), int.Parse(inputs[1]));
            }

            var gatewayIndexes = new List<int>();
            for (int i = 0; i < E; i++)
            {
                gatewayIndexes.Add(int.Parse(Console.ReadLine())); // the index of a gateway node
            }

            var skynetTheVirus = new SkynetTheVirus(NODES, links, gatewayIndexes);

            // game loop
            while (true)
            {
                int SI = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn

                // Write an action using Console.WriteLine()
                // To debug: Console.Error.WriteLine("Debug messages...");

                Console.WriteLine("0 1"); // Example: 0 1 are the indices of the nodes you wish to sever the link between
            }
        }

        public class SkynetTheVirus
        {
            private readonly List<Node> _nodes = new List<Node>();
            private readonly List<Link> _links = new List<Link>();

            public SkynetTheVirus(int nodes, IEnumerable<KeyValuePair<int, int>> links, List<int> gatewayIndexes)
            {
                for (var node = 0; node < nodes; node++)
                {
                    _nodes.Add(new Node(node, gatewayIndexes.Contains(node)));
                }

                foreach (var link in links)
                {
                    var node1 = _nodes.Single(n => n.Index == link.Key);
                    var node2 = _nodes.Single(n => n.Index == link.Value);
                    _links.Add(new Link(node1, node2));
                }
            }
        }

        public class Node
        {
            public Node(int index, bool isGateway)
            {
                Index = index;
                IsGateway = isGateway;
            }

            public int Index { get; private set; }

            public bool IsGateway { get; private set; }

            public override int ToString()
            {
                return Index;
            }

            public override bool Equals(object obj)
            {
                return Index == ((Node)obj).Index;
            }

            public static bool operator ==(Node left, Node right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(Node left, Node right)
            {
                return !Equals(left, right);
            }
        }

        public class Link
        {
            private readonly List<Node> _nodes = new List<Node>();

            public Link(Node index1, Node index2)
            {
                _nodes.AddRange(new[] { index1, index2 });
            }

            public override string ToString()
            {
                return string.Join(" ", _nodes);
            }
        }
    }
}
