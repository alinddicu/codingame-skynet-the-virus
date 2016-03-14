namespace codingame.skynet.the.virus.test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NFluent;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [TestClass]
    public class SkynetTheVirusTests
    {
        [TestMethod]
        public void TwoPaths()
        {
            //4 4 1
            //0 1
            //0 2
            //1 3
            //2 3
            //3

            var links = new List<KeyValuePair<int, int>> 
            { 
                new KeyValuePair<int, int>(0, 1), 
                new KeyValuePair<int, int>(0, 2), 
                new KeyValuePair<int, int>(1, 3), 
                new KeyValuePair<int, int>(2, 3) 
            };
            var gateways = new int[] { 3 };
            var virus = new codingame.skynet.the.virus.Player.SkynetTheVirus(4, links, gateways);

            Check.That(virus.Severe(0).ToString()).IsEqualTo("1 3");
            Check.That(virus.Severe(2).ToString()).IsEqualTo("2 3");
        }

        [TestMethod]
        public void ThreePaths()
        {
            var links = new List<KeyValuePair<int, int>> 
            { 
                new KeyValuePair<int, int>(0, 1), 
                new KeyValuePair<int, int>(0, 2), 
                new KeyValuePair<int, int>(0, 3), 
                new KeyValuePair<int, int>(1, 4), 
                new KeyValuePair<int, int>(2, 4),
                new KeyValuePair<int, int>(3, 4) 
            };
            var gateways = new int[] { 4 };
            var virus = new codingame.skynet.the.virus.Player.SkynetTheVirus(5, links, gateways);

            Check.That(virus.Severe(0).ToString()).IsEqualTo("1 4");
            Check.That(virus.Severe(3).ToString()).IsEqualTo("3 4");
        }
    }
}
