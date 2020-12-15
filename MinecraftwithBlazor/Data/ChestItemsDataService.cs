using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace MinecraftwithBlazor.Data
{
    public class ChestItemsDataService
    {
        public ChestItems[] Itemlist { get; set; }

        public void Extraction(string data)
        {
            data = data.Substring(data.IndexOf("[")); // [ 以降のデータを得る
            data = Regex.Replace(data, @"[\[{\]\s]", ""); // [, ], { 記号を削除
            string[] split_data = data.Split("},"); // }, 記号で分割

            Itemlist = new ChestItems[split_data.Length];

            for (int i = 0; i < split_data.Length; i++)
            {
                Itemlist[i] = new ChestItems(); // 配列の宣言だけでなくインスタンスもつくる

                string[] str = split_data[i].Split(",");

                for (int j = 0; j < str.Length; j++)
                {
                    if (str[j].Contains("Slot:"))
                    {
                        Itemlist[i].ItemSlot = Regex.Replace(str[j], "[^0-9]", "");
                    }
                    else if (str[j].Contains("id:"))
                    {
                        str[j] = Regex.Replace(str[j], @"[^a-zA-Z:]", "");
                        Itemlist[i].ItemID = str[j].Substring(str[j].IndexOf("minecraft"));
                    }
                    else if (str[j].Contains("Count:"))
                    {
                        Itemlist[i].ItemCount = Regex.Replace(str[j], "[^0-9]", "");
                    }
                }
            }
        }

        public Task<ChestItems[]> AsyncExtraction()
        {
            return Task.FromResult(Enumerable.Range(0, Itemlist.Length).Select(index => new ChestItems
            {
                ItemCount = Itemlist[index].ItemCount,
                ItemID = Itemlist[index].ItemID,
                ItemSlot = Itemlist[index].ItemSlot
            }).ToArray());
        }

    }
}