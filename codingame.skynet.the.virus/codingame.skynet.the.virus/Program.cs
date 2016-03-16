namespace codingame.skynet.the.virus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Player
    {
        static void Main(string[] args)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int NODES = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
            int L = int.Parse(inputs[1]); // the number of links
            int E = int.Parse(inputs[2]); // the number of exit gateways
            var links = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < L; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
                int N2 = int.Parse(inputs[1]);
                links.Add(new KeyValuePair<int, int>(int.Parse(inputs[0]), int.Parse(inputs[1])));
            }

            var gatewayIndexes = new List<int>();
            for (int i = 0; i < E; i++)
            {
                gatewayIndexes.Add(int.Parse(Console.ReadLine())); // the index of a gateway node
            }

            var skynetTheVirus = new SkynetVirus(NODES, links, gatewayIndexes);

            // game loop
            while (true)
            {
                int index = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn

                Console.WriteLine(skynetTheVirus.Severe(index)); // Example: 0 1 are the indices of the nodes you wish to sever the link between
            }
        }

        public class SkynetVirus
        {
            private readonly List<Node> _nodes = new List<Node>();
            private readonly List<Link> _links = new List<Link>();

            public SkynetVirus(
                int nodesCount,
                IEnumerable<KeyValuePair<int, int>> links, 
                ICollection<int> gatewayIndexes)
            {
                for (var node = 0; node < nodesCount; node++)
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

            public Link Severe(int indexOfAgent)
            {
                var linksNonSevereds = _links.Where(l => l.ContainsGateway() && !l.IsSevered);
                var link = linksNonSevereds.FirstOrDefault(l => l.HasIndex(indexOfAgent));
                if (link == null)
                {
                    link = linksNonSevereds.First();
                }

                link.IsSevered = true;
                return link;
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

            public override string ToString()
            {
                return Index.ToString();
            }
        }

        public class Link
        {
            private readonly List<Node> _nodes = new List<Node>();

            public Link(Node index1, Node index2)
            {
                _nodes.AddRange(new[] { index1, index2 });
            }

            public bool IsSevered { get; set; }

            public override string ToString()
            {
                return string.Join(" ", _nodes);
            }

            public bool ContainsGateway()
            {
                return _nodes.Any(n => n.IsGateway);
            }

            public bool HasIndex(int index)
            {
                return _nodes.Any(n => n.Index == index);
            }
        }
    }
}
