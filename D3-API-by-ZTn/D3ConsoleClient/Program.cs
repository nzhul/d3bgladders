using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTn.BNet.D3;
using ZTn.BNet.BattleNet;
using ZTn.BNet.D3.Careers;
using ZTn.BNet.D3.Heroes;

namespace D3ConsoleClient
{
    public class Program
    {
        private const string ApiKey = "zrxxcy3qzp8jcbgrce2es4yq52ew2k7r";
        static void Main(string[] args)
        {
            D3Api.ApiKey = ApiKey;

            var battleTag = new BattleTag("nzhul#2124");

            var career = Career.CreateFromBattleTag(battleTag);
            var hero = Hero.CreateFromHeroId(battleTag, career.Heroes[1].Id);

            var gosho = 1;

            Console.WriteLine("----------END-----------");
        }
    }
}
