using LAB1.Packages;
using LAB1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace LAB1.Builders
{
    public class PackageBuilder<T> where T : IPackage, new()
    {
        HammingCodeService _hammingCodeService = new HammingCodeService();

        public List<T> PackMessage(string message, string sourcePort)
        {
 
            List<T> list = new List<T>();
            while (message.Length > 26)
            {
                T package = new T();
                package.Pack(ByteStaff(message.Substring(0, 26)), sourcePort);
                list.Add(package);
                message = message.Remove(0, 26);
            }

            T FinalPackage = new T();
            FinalPackage.Pack(ByteStaff(message), sourcePort);
            list.Add(FinalPackage);


            return list;
            
        }

        public List<T> UnPackMessage(string message)
        {
            var list = new List<T>();         
            foreach(var package in DeStaff(message))
            {
                T pack = new T();
                pack.UnPack(package);
                if (_hammingCodeService.CheckFrame(pack.GetData())) pack.SetData(_hammingCodeService.CorrectFrame(pack.GetData()));
                list.Add(pack);
            }

            return list;
        }

        private string ByteStaff(string message)
        {
            for(int i = 0; i < message.Length; i++)
            {
                if (message[i] == '\"') 
                {
                    message = message.Remove(i, 1);
                    message = message.Insert(i, IPackage.EscapeByte.ToString());
                    i++;
                }
            }

            return message;
        }

        private List<string> DeStaff(string message)
        {
            var list = message.Split(IPackage.Flag).ToList();
            list = list
                .Select(pack =>
                {
                    pack = pack.Contains(IPackage.EscapeByte) ? pack.Replace(IPackage.EscapeByte, IPackage.Flag) : pack;
                    return pack;
                })
                .ToList();
            
            
            return list;
        }
    }
}
