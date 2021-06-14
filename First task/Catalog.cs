using System;
using System.Collections.Generic;

namespace First_task
{
    class Catalog : IComparable<Catalog>
    {
        public string Name { get; private set; } //название
        private SortedSet<Catalog> internalCatalogs; //Отсортированный список каталогов
        private SortedSet<Goods> internalGoods; //Отсортированный список товаров
        public Catalog(string name = "root", SortedSet<Catalog> contentCatalogs = null, SortedSet<Goods> contentGoods = null)
        {
            Name = name;
            if (contentCatalogs == null) internalCatalogs = new SortedSet<Catalog>();
            else internalCatalogs = contentCatalogs;

            if (contentGoods == null) internalGoods = new SortedSet<Goods>();
            else internalGoods = contentGoods;
        }
        public void AddCatalog(Catalog catalog)
        {
            internalCatalogs.Add(catalog);
        }
        public void AddGoods(Goods goods)
        {
            internalGoods.Add(goods);
        }
        public void DeleteCatalog(string catalogName)
        {
            Catalog catalog = FindCatalog(catalogName);
            if (catalog != null) internalCatalogs.Remove(catalog);
        }
        public void DeleteGoods(string goodsName)
        {
            Goods goods = FindGoods(goodsName);
            if (goods != null) internalGoods.Remove(goods);
        }
        public void ChangeGoods(string goodsName, int count)
        {
            Goods goods = FindGoods(goodsName);
            if (goods != null) goods.ChangeCount(count);
        }
        public Catalog FindCatalog(string name)
        {
            if (internalCatalogs != null)
                foreach (var item in internalCatalogs)
                    if (item.Name == name)
                    {
                        return item;
                    }
            return null;
        }
        public Goods FindGoods(string name)
        {
            if (internalGoods != null)
                foreach (var item in internalGoods)
                    if (item.Name == name)
                    {
                        return item;
                    }
            return null;
        }
        public int[] Info()
        {
            int[] data = new int[2] { 0, 0 };
            int[] info;
            if (internalGoods != null)
                foreach (var item in internalGoods)
                {
                    info = item.Info();
                    data[0] += info[0];
                    data[1] += info[1] * info[0];
                }
            if (internalCatalogs != null)
                foreach (var item in internalCatalogs)
                {
                    info = item.Info();
                    data[0] += info[0];
                    data[1] += info[1];
                }
            return data;
        }
        public void PrintAllCatalogsContent(int degree = 0)
        {
            int[] data;
            if (internalCatalogs != null)
                foreach (var item in internalCatalogs)
                {
                    data = item.Info();
                    for (int i = 0; i < degree; ++i)
                        Console.Write("  ");
                    Console.WriteLine("[" + item.Name + "]" + " содержит " + data[0].ToString() + " товаров на общую сумму " + data[1].ToString());
                    Catalog podcat = null;
                    podcat = FindCatalog(item.Name);
                    if (podcat != null)
                        podcat.PrintAllCatalogsContent(degree + 1);
                }
            if (degree == 0)
            {
                data = Info();
                Console.WriteLine("Вне какого-либо каталога содержится " + data[0].ToString() + " товаров на общую сумму " + data[1].ToString());
            }
        }
        public void PrintCatalogContent()
        {
            int[] data;
            Console.WriteLine("Вы находитесь в " + Name);
            Console.WriteLine("Catalog name\tGoods count\tGoods cost");
            if (internalCatalogs != null)
                foreach (var item in internalCatalogs)
                {
                    data = item.Info();
                    if (item.Name.Length <= 7) Console.WriteLine(item.Name + '\t' + '\t' + data[0].ToString() + '\t' + '\t' + data[1].ToString());
                    else Console.WriteLine(item.Name + '\t' + data[0].ToString() + '\t' + '\t' + data[1].ToString());
                }

            Console.WriteLine();
            Console.WriteLine("Goods name\tCount\t\tCost");
            if (internalGoods != null)
                foreach (var item in internalGoods)
                {
                    data = item.Info();
                    if (item.Name.Length <= 7) Console.WriteLine(item.Name + '\t' + '\t' + data[0].ToString() + '\t' + '\t' + data[1].ToString());
                    else Console.WriteLine(item.Name + '\t' + data[0].ToString() + '\t' + '\t' + data[1].ToString());
                }

            Console.WriteLine();
        }

        public int CompareTo(Catalog catalog)
        {
            return Name.CompareTo(catalog.Name);
        }
    }
}