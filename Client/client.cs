﻿using Trade.Cfg;
using Trade.Data;
using Trade.Mixin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Interace.Mixin;

namespace Trade
{
    public class client
    {
        public Basic basics(string code)
        {
            return basics()
                .FirstOrDefault(p=>string.Equals(p.code, code, StringComparison.InvariantCultureIgnoreCase));
        }

        public Basics basics(IEnumerable<string> codes)
        {
            var set = basics();

            var q = from f in set
                    join c in codes on f.code equals c
                    select f;

            return new Basics(q.ToArray());
        }

        public Basics basics()
        {
            var file = Configuration.data.basics.file("basics.csv");
            return new Basics(file.ReadCsv<Basic>(Configuration.encoding.gbk));
        }

        public kdata kdata(string code, string ktype)
        {
            var file = Configuration.data.kdata.file(ktype + "/" + code + ".csv");
            var p = file.ReadCsv<kdatapoint>(Configuration.encoding.gbk);
            return new kdata(code, p);
        }

        public IEnumerable<kdata> kdataall(string ktype, string sector = null)
        {
            var codes = Codes(sector).ToArray();
            var results = codes
                .AsParallel()
                .Select(code => kdata(code, ktype))
                .Where(p => p != null)
                .ToArray();
            return results;
        }

        public IEnumerable<string> Codes(string sector = Sector.any)
        {
            return basics().Where(p => InSector(p, sector)).Select(p => p.code).Distinct().ToArray();
        }

        private bool InSector(Basic f, string sector)
        {
            var code = f.code;
            if (string.IsNullOrEmpty(sector)) return true;

            switch (sector)
            {
                case "any": return true;
                case Sector.上证: return code.StartsWith("60");
                case Sector.深证: return code.StartsWith("30") || code.StartsWith("00");
                case Sector.创业板: return code.StartsWith("30");
                case Sector.中小板: return code.StartsWith("00");
                case Sector.板块指数: return code.StartsWith("88");
                case Sector.板块指数除外: return !code.StartsWith("88");
                case Sector.证金持股:
                    {
                        var codes = new string[]
                        {
                            "300386"    ,
                            "300277"    ,
                            "600410"    ,
                            "000680"    ,
                            "000049"    ,
                            "300321"    ,
                            "002637"    ,
                            "300368"    ,
                            "002709"    ,
                            "300125"    ,
                            "601311"    ,
                            "002692"    ,
                            "300034"    ,
                            "300185"    ,
                            "600389"    ,
                            "002661"    ,
                            "002351"    ,
                            "300140"    ,
                            "300033"    ,
                            "000060"    ,
                            "300018"    ,
                            "600657"    ,
                            "600588"    ,
                            "600601"    ,
                            "600528"    ,
                            "600362"    ,
                            "600198"    ,
                            "300246"    ,
                            "002273"    ,
                            "300264"    ,
                            "600640"    ,
                            "300250"    ,
                            "300333"    ,
                            "600176"    ,
                            "002153"    ,
                            "300211"    ,
                            "000852"    ,
                            "002405"    ,
                            "300240"    ,
                            "600614"    ,
                            "002192"    ,
                            "601965"    ,
                            "600086"    ,
                            "000629"    ,
                            "300341"    ,
                            "000090"    ,
                            "000823"    ,
                            "600037"    ,
                            "600058"    ,
                            "002352"    ,
                            "002122"    ,
                            "600120"    ,
                            "600340"    ,
                            "002588"    ,
                            "002168"    ,
                            "601216"    ,
                            "601992"    ,
                            "603005"    ,
                            "600570"    ,
                            "000780"    ,
                            "000631"    ,
                            "603555"    ,
                            "600880"    ,
                            "600171"    ,
                            "300178"    ,
                            "600289"    ,
                            "600415"    ,
                            "000962"    ,
                            "600809"    ,
                            "300187"    ,
                            "002466"    ,
                            "600200"    ,
                            "600435"    ,
                            "600219"    ,
                            "000528"    ,
                            "000877"    ,
                            "601808"    ,
                            "600175"    ,
                            "600109"    ,
                            "000030"    ,
                            "600787"    ,
                            "603699"    ,
                            "002189"    ,
                            "600776"    ,
                            "600031"    ,
                            "000999"    ,
                            "300030"    ,
                            "600481"    ,
                            "600895"    ,
                            "600826"    ,
                            "600637"    ,
                            "601179"    ,
                            "002253"    ,
                            "600085"    ,
                            "000997"    ,
                            "000088"    ,
                            "600801"    ,
                            "000006"    ,
                            "300416"    ,
                            "000762"    ,
                            "002617"    ,
                            "600166"    ,
                            "600009"    ,
                            "600651"    ,
                            "600761"    ,
                            "300253"    ,
                            "600759"    ,
                            "000572"    ,
                            "000988"    ,
                            "600863"    ,
                            "600839"    ,
                            "600563"    ,
                            "600600"    ,
                            "600831"    ,
                            "600039"    ,
                            "600750"    ,
                            "600694"    ,
                            "601158"    ,
                            "600422"    ,
                            "600805"    ,
                            "600261"    ,
                            "002601"    ,
                            "601009"    ,
                            "000900"    ,
                            "600586"    ,
                            "002064"    ,
                            "000816"    ,
                            "000652"    ,
                            "600373"    ,
                            "600060"    ,
                            "600112"    ,
                            "601001"    ,
                            "600983"    ,
                            "000513"    ,
                            "000581"    ,
                            "600720"    ,
                            "600511"    ,
                            "600089"    ,
                            "600352"    ,
                            "600705"    ,
                            "002365"    ,
                            "002665"    ,
                            "601208"    ,
                            "002073"    ,
                            "000027"    ,
                            "002022"    ,
                            "300039"    ,
                            "002146"    ,
                            "600271"    ,
                            "600199"    ,
                            "600519"    ,
                            "000848"    ,
                            "600808"    ,
                            "002646"    ,
                            "300059"    ,
                            "000919"    ,
                            "002029"    ,
                            "601336"    ,
                            "600584"    ,
                            "600743"    ,
                            "000156"    ,
                            "000028"    ,
                            "000728"    ,
                            "600348"    ,
                            "002375"    ,
                            "000540"    ,
                            "600535"    ,
                            "600251"    ,
                            "000625"    ,
                            "600416"    ,
                            "000788"    ,
                            "600436"    ,
                            "600468"    ,
                            "002463"    ,
                            "002706"    ,
                            "600332"    ,
                            "600572"    ,
                            "600594"    ,
                            "601908"    ,
                            "601969"    ,
                            "600006"    ,
                            "002030"    ,
                            "600770"    ,
                            "000776"    ,
                            "000616"    ,
                            "000667"    ,
                            "600196"    ,
                            "002236"    ,
                            "600703"    ,
                            "600177"    ,
                            "000543"    ,
                            "600958"    ,
                            "600064"    ,
                            "600997"    ,
                            "000869"    ,
                            "600429"    ,
                            "600409"    ,
                            "600008"    ,
                            "000099"    ,
                            "600884"    ,
                            "600489"    ,
                            "600518"    ,
                            "600522"    ,
                            "600812"    ,
                            "601699"    ,
                            "600208"    ,
                            "600690"    ,
                            "002682"    ,
                            "000592"    ,
                            "601688"    ,
                            "601098"    ,
                            "603188"    ,
                            "601666"    ,
                            "601718"    ,
                            "601888"    ,
                            "002736"    ,
                            "600642"    ,
                            "601117"    ,
                            "600741"    ,
                            "600325"    ,
                            "601607"    ,
                            "601678"    ,
                            "600059"    ,
                            "002041"    ,
                            "600587"    ,
                            "600583"    ,
                            "002294"    ,
                            "002048"    ,
                            "000552"    ,
                            "601901"    ,
                            "601788"    ,
                            "601333"    ,
                            "600816"    ,
                            "600062"    ,
                            "601929"    ,
                            "600825"    ,
                            "000157"    ,
                            "000951"    ,
                            "601126"    ,
                            "600276"    ,
                            "600309"    ,
                            "600201"    ,
                            "002081"    ,
                            "600376"    ,
                            "600507"    ,
                            "000568"    ,
                            "600597"    ,
                            "000069"    ,
                            "601369"    ,
                            "600823"    ,
                            "600755"    ,
                            "000333"    ,
                            "000031"    ,
                            "600664"    ,
                            "000046"    ,
                            "002028"    ,
                            "300024"    ,
                            "600387"    ,
                            "000525"    ,
                            "600079"    ,
                            "000422"    ,
                            "000511"    ,
                            "000538"    ,
                            "000563"    ,
                            "000719"    ,
                            "000729"    ,
                            "000829"    ,
                            "000876"    ,
                            "000883"    ,
                            "000897"    ,
                            "000975"    ,
                            "002128"    ,
                            "002396"    ,
                            "002440"    ,
                            "002482"    ,
                            "002608"    ,
                            "300120"    ,
                            "600021"    ,
                            "600027"    ,
                            "600132"    ,
                            "600158"    ,
                            "600256"    ,
                            "600337"    ,
                            "600425"    ,
                            "600509"    ,
                            "600537"    ,
                            "600578"    ,
                            "600636"    ,
                            "600673"    ,
                            "600675"    ,
                            "600717"    ,
                            "600747"    ,
                            "600780"    ,
                            "600859"    ,
                            "600999"    ,
                            "601005"    ,
                            "601101"    ,
                            "601377"    ,
                            "601958"    ,
                            "300072"    ,
                            "601601"    ,
                            "000895"    ,
                            "300115"    ,
                            "300124"    ,
                            "000758"    ,
                            "600585"    ,
                            "601555"    ,
                            "300267"    ,
                            "600185"    ,
                            "600864"    ,
                            "000930"    ,
                            "601231"    ,
                            "600655"    ,
                            "600881"    ,
                            "600406"    ,
                            "600252"    ,
                            "000338"    ,
                            "000898"    ,
                            "000600"    ,
                            "000612"    ,
                            "000961"    ,
                            "000783"    ,
                            "600546"    ,
                            "000921"    ,
                            "000768"    ,
                            "600195"    ,
                            "000669"    ,
                            "000858"    ,
                            "600004"    ,
                            "600887"    ,
                            "600369"    ,
                            "600029"    ,
                            "600596"    ,
                            "600111"    ,
                            "601000"    ,
                            "600068"    ,
                            "600298"    ,
                            "600875"    ,
                            "600426"    ,
                            "000400"    ,
                            "600685"    ,
                            "000830"    ,
                            "000777"    ,
                            "600143"    ,
                            "600141"    ,
                            "002066"    ,
                            "002205"    ,
                            "300305"    ,
                            "000656"    ,
                            "600335"    ,
                            "002450"    ,
                            "600456"    ,
                            "600138"    ,
                            "000063"    ,
                            "002269"    ,
                            "601099"    ,
                            "600300"    ,
                            "000825"    ,
                            "600125"    ,
                            "000623"    ,
                            "600150"    ,
                            "002266"    ,
                            "300216"    ,
                            "002038"    ,
                            "600557"    ,
                            "600660"    ,
                            "002242"    ,
                            "600737"    ,
                            "002166"    ,
                            "000726"    ,
                            "600170"    ,
                            "601226"    ,
                            "000539"    ,
                            "600220"    ,
                            "002475"    ,
                            "000661"    ,
                            "600674"    ,
                            "600811"    ,
                            "000931"    ,
                            "000039"    ,
                            "000423"    ,
                            "000939"    ,
                            "000402"    ,
                            "002277"    ,
                            "601106"    ,
                            "002634"    ,
                            "002251"    ,
                            "000690"    ,
                            "300451"    ,
                            "600317"    ,
                            "600320"    ,
                            "000550"    ,
                            "000651"    ,
                            "600259"    ,
                            "600026"    ,
                            "002092"    ,
                            "002012"    ,
                            "600169"    ,
                            "600428"    ,
                            "002204"
                        };
                        return codes.Contains(code);
                    }
                default:
                    return f.industry == sector;
            }
        }
    }
}
