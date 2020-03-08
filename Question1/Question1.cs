using System;
using System.Linq;
using System.Collections.Generic;

namespace Question1
{
    class Question1
    {
        static List<Country> Countries = Country.GetCountries();
        static void Main(string[] args)
        {
            //1.1
            OrderByCountryNameLinQ();
            //OrderByCountryNameMethod();

            //1.2
            //OrderByNumberOfResourceLinQ();
            //OrderByNumberOfResourceMethod();

            //1.3
            //ShareBorderWithArgentinaLinQ();
            //ShareBorderWithArgentinaMethod();

            //1.4
            //FindCountryWithPopulationLinQ();
            //FindCountryWithPopulationMethod();

            //1.5
            //CountryWithHighestPopulationLinQ();
            //CountryWithHighestPopulationMethod();

            //1.6
            //AllReligionInSouthAmericaMethod();

            Console.ReadKey();
        }

        #region 1.1 List the names of the countries in alphabetical order [0.5 mark]
        private static void OrderByCountryNameLinQ()
        {
            var countries = from c in Countries
                            orderby c.Name
                            select c;

            foreach (var c in countries)
                Console.WriteLine(c.Name);
        }

        private static void OrderByCountryNameMethod()
        {
            var countries = Countries.OrderBy(c => c.Name);

            foreach (var c in countries)
                Console.WriteLine(c.Name);
        }
        #endregion

        #region 1.2 List the names of the countries in descending order of number of resources[0.5 mark]
        private static void OrderByNumberOfResourceLinQ()
        {
            var countries = from c in Countries
                            orderby c.Resources.Count() descending
                            select c;

            foreach (var c in countries)
                Console.WriteLine(c.Name + "(" + c.Resources.Count() + ")");
        }
        private static void OrderByNumberOfResourceMethod()
        {
            var countries = Countries.OrderByDescending(c => c.Resources.Count());

            foreach (var c in countries)
                Console.WriteLine(c.Name + "(" + c.Resources.Count() + ")");
        }
        #endregion

        #region 1.3 List the names of the countries that shares a border with Argentina[0.5 mark]
        private static void ShareBorderWithArgentinaLinQ()
        {
            var countries = from c in Countries
                            where c.Borders.Contains("Argentina")
                            select c;
            foreach (var c in countries)
                Console.WriteLine(c.Name);
        }
        private static void ShareBorderWithArgentinaMethod()
        {
            var countries = Countries.Where(c => c.Borders.Contains("Argentina"));
            foreach (var c in countries)
                Console.WriteLine(c.Name);
        }
        #endregion

        #region 1.4 List the names of the countries that has more than 10,000,000 population[0.5 mark]
        private static void FindCountryWithPopulationLinQ()
        {
            var countries = from c in Countries
                            where c.Population > 10_000_000
                            select c;
            foreach (var c in countries)
                Console.WriteLine($"{c.Name} ({c.Population:N0})");
        }
        private static void FindCountryWithPopulationMethod()
        {
            var countries = Countries.Where(c => c.Population > 10_000_000);
            foreach (var c in countries)
                Console.WriteLine($"{c.Name} ({c.Population:N0})");
        }
        #endregion

        #region 1.5 List the country with highest population[1 mark]
        private static void CountryWithHighestPopulationLinQ()
        {
            var countries = from c in Countries
                            where c.Population == (from c1 in Countries
                                                   select c1.Population).Max()
                            select c; // complexity is O(n^2) ?

            //var maxPopulation = (from c in Countries
            //                     select c.Population).Max();
            //var countries = from c in Countries
            //                where c.Population == maxPopulation
            //                select c; // complexity is O(n) ?
            foreach (var c in countries)
                Console.WriteLine($"{c.Name} ({c.Population:N0})");
        }
        private static void CountryWithHighestPopulationMethod()
        {
            var countries = Countries.Where(c
                => c.Population == Countries.Select(c1 => c1.Population).Max()); // complexity is O(n^2) ?

            //var maxPopulation = Countries.Select(c1 => c1.Population).Max();
            //var countries = Countries.Where(c => c.Population == maxPopulation); // complexity is O(n) ?
            foreach (var c in countries)
                Console.WriteLine($"{c.Name} ({c.Population:N0})");
        }
        #endregion

        #region 1.6 List all the religion in south America in dictionary order[1 mark]
        private static void AllReligionInSouthAmericaLinQ()
        {
            // ?
        }
        private static void AllReligionInSouthAmericaMethod()
        {
            //solution1
            var religions = Countries.SelectMany(c => c.Religions)
                .Distinct()
                .OrderBy(r => r); ;

            // solution2
            IEnumerable<string> religions1 = new List<string>();
            religions = Countries.Select(c => c.Religions)
                .Aggregate(religions1, (current, list) => current.Concat(list))
                .Distinct()
                .OrderBy(r => r);

            foreach (var r in religions)
                Console.WriteLine(r);
        }
        #endregion
    }
}
