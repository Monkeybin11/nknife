﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using NKnife.Util;

namespace NKnife.Chinese
{
    /// <summary>
    ///     中国的城市
    /// </summary>
    public class Cities
    {
        private static readonly Random _Random = UtilRandom.Random;

        public static List<City> Data { get; }

        /// <summary>
        ///     随机生成指定数量的城市（省份+城市）
        /// </summary>
        public StringCollection GetRandomCityName(int count)
        {
            if (Data == null || Data.Any())
                return new StringCollection();
            var indexArray = new int[count];
            for (var i = 0; i < count; i++)
                indexArray[i] = _Random.Next(0, 34);
            var sc = new StringCollection();
            foreach (var i in indexArray)
            {
                var sheng = Data[i];
                CityItem city;
                if (sheng.city.Length > 1)
                    city = sheng.city[_Random.Next(0, sheng.city.Length - 1)];
                else
                    city = sheng.city[0];
                string area;
                if (city.area.Count > 1)
                    area = city.area[_Random.Next(0, city.area.Count - 1)];
                else
                    area = city.area[0];
                if (sheng.name == city.name)
                    sc.Add($"{city.name}{area}".Replace(" ", ""));
                else
                    sc.Add($"{sheng.name}{city.name}{area}".Replace(" ", ""));
            }

            return sc;
        }

        public class City
        {
            public string name { get; set; }
            public CityItem[] city { get; set; }
        }

        public class CityItem
        {
            public string name { get; set; }
            public List<string> area { get; set; }
        }
    }
}