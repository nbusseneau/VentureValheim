﻿using System.Collections.Generic;
using VentureValheim.Progression;
using Xunit;

namespace VentureValheim.ProgressionTests
{
    public class APITests
    {
        public class TestProgressionAPI : ProgressionAPI, IProgressionAPI
        {
            public TestProgressionAPI(IProgressionAPI api) : base()
            {

            }
        }

        [Fact]
        public void StringToSet_All()
        {
            string string1 = "killedTroll";
            string string2 = "killedTroll,killedBear,killed_Jesus";
            string string3 = " killedTroll , killedBear   , killed_Jesus ";

            var set1 = new HashSet<string>();
            set1.Add("killedTroll");
            var set2 = new HashSet<string>();
            set2.Add("killedTroll");
            set2.Add("killedBear");
            set2.Add("killed_Jesus");

            Assert.Single(set1);
            Assert.Equal(set1, ProgressionAPI.Instance.StringToSet(string1));
            Assert.Equal(3, set2.Count);
            Assert.Equal(set2, ProgressionAPI.Instance.StringToSet(string2));
            Assert.Equal(3, set2.Count);
            Assert.Equal(set2, ProgressionAPI.Instance.StringToSet(string3));
        }

        [Fact]
        public void StringToDictionary_All()
        {
            string string1 = "Boar";
            string string2 = "Boar,defeated_eikthyr,Wolf,defeated_dragon,Lox,defeated_goblinking";
            string string3 = " Boar, defeated_eikthyr  ,  Wolf, defeated_dragon,   Lox, defeated_goblinking ";

            var dict1 = new Dictionary<string, string>();
            var dict2 = new Dictionary<string, string>();
            dict2.Add("Boar", "defeated_eikthyr");
            dict2.Add("Wolf", "defeated_dragon");
            dict2.Add("Lox", "defeated_goblinking");

            Assert.Equal(dict1, ProgressionAPI.Instance.StringToDictionary(string1));
            Assert.Equal(3, dict2.Count);
            Assert.Equal(dict2, ProgressionAPI.Instance.StringToDictionary(string2));
            Assert.Equal(3, dict2.Count);
            Assert.Equal(dict2, ProgressionAPI.Instance.StringToDictionary(string3));
        }

        [Theory]
        [InlineData(105, 5, true, 105)]
        [InlineData(104, 5, true, 105)]
        [InlineData(103, 5, true, 105)]
        [InlineData(102, 5, true, 105)]
        [InlineData(101, 5, true, 105)]
        [InlineData(100, 5, true, 100)]
        [InlineData(105, 5, false, 105)]
        [InlineData(104, 5, false, 100)]
        [InlineData(103, 5, false, 100)]
        [InlineData(102, 5, false, 100)]
        [InlineData(101, 5, false, 100)]
        [InlineData(100, 5, false, 100)]
        public void PrettifyNumber_All(int num, int roundTo, bool roundUp, int expected)
        {
            Assert.Equal(expected, ProgressionAPI.Instance.PrettifyNumber(num, roundTo, roundUp));
        }

        [Fact]
        public void Enum_All()
        {
            ItemCategory ic2 = (ItemCategory)(-2);

            ItemCategory ic = (ItemCategory)(-1);
            Assert.Equal(ItemCategory.Undefined, ic);
        }
    }
}