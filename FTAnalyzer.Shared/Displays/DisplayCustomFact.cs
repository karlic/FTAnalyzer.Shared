﻿using System;
using System.Collections.Generic;

namespace FTAnalyzer
{
    public class DisplayCustomFact : IDisplayCustomFact
    {
        public string CustomFactName { get; }
        public int IndividualCount { get; }
        public int FamilyCount { get; }
        public bool Ignore { get; set; }

        public DisplayCustomFact(string factname,int indcount, int famcount, bool ignore)
        {
            CustomFactName = factname;
            IndividualCount = indcount;
            FamilyCount = famcount;
            Ignore = ignore;
        }

        public int CompareTo(IDisplayCustomFact? that) => string.Compare(CustomFactName, that.CustomFactName, System.StringComparison.Ordinal);

        public IComparer<IDisplayCustomFact> GetComparer(string columnName, bool ascending)
        {
            return columnName switch
            {
                "CustomFactName" => CompareComparableProperty<IDisplayCustomFact>(f => f.CustomFactName, ascending),
                "IndividualCount" => CompareComparableProperty<IDisplayCustomFact>(f => f.IndividualCount, ascending),
                "FamilyCount" => CompareComparableProperty<IDisplayCustomFact>(f => f.FamilyCount, ascending),
                "Ignore" => CompareComparableProperty<IDisplayCustomFact>(f => f.Ignore, ascending),
                _ => CompareComparableProperty<IDisplayCustomFact>(f => f.CustomFactName, ascending),
            };
        }

        static Comparer<T> CompareComparableProperty<T>(Func<IDisplayCustomFact, IComparable> accessor, bool ascending)
        {
            return Comparer<T>.Create((x, y) =>
            {
                var c1 = accessor(x as IDisplayCustomFact);
                var c2 = accessor(y as IDisplayCustomFact);
                var result = c1.CompareTo(c2);
                return ascending ? result : -result;
            });
        }
    }
}

