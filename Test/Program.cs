using libplctag;
using libplctag.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var BufferNotEmpty_St = new Tag<BoolPlcMapper, bool>()
            {
                Name = "SendBuffer.BufferNotEmpty",
                Gateway = "192.168.201.167",
                Path = "1,2",
                PlcType = PlcType.ControlLogix,
                Protocol = Protocol.ab_eip,
                Timeout = TimeSpan.FromSeconds(5)
            };
            BufferNotEmpty_St.Read();
            Console.WriteLine(BufferNotEmpty_St.Value);
            Console.ReadKey();

        }
    }



     
}
