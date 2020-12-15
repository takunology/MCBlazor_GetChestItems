using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class ChestItemsData
    {
        public string Data { get; set; }
        public Items[] Itemlist { get; set; }

        public void Extraction(string data)
        {
            data = data.Substring(data.IndexOf("[")); // [ 以降のデータを得る

            Console.WriteLine();
            Console.WriteLine(data);

            data = Regex.Replace(data, @"[\[{\]\s]", ""); // [, ], { 記号を削除

            Console.WriteLine();
            Console.WriteLine(data);

            string[] split_data = data.Split("},"); // }, 記号で分割

            Console.WriteLine();
            int l = 0;
            foreach (var item in split_data)
            {
                Console.WriteLine($"{l} ===> {item}");
                l++;
            }

            Itemlist = new Items[split_data.Length];

            Console.WriteLine();

            for (int i = 0; i < split_data.Length; i++)
            {
                Itemlist[i] = new Items();

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

                Console.WriteLine($"アイテムスロット：{Itemlist[i].ItemSlot} アイテム名：{Itemlist[i].ItemID} アイテム数：{Itemlist[i].ItemCount}");
            }
            //Data = getdata.data[0].ItemID;
        }
    }
}
