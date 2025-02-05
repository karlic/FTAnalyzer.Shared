﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FTAnalyzer.Filters
{
    public static class FilterUtils
    {
        public static Predicate<T> TrueFilter<T>() => x => true;

        public static Predicate<T> FalseFilter<T>() => x => false;

        public static Predicate<T> OrFilter<T>(Predicate<T> p1, Predicate<T> p2) => x => p1(x) || p2(x);

        public static Predicate<T> OrFilter<T>(Predicate<T> p1, Predicate<T> p2, Predicate<T> p3) => x => p1(x) || p2(x) || p3(x);

        public static Predicate<T> OrFilter<T>(IEnumerable<Predicate<T>> ps) => x => ps.Any(p => p(x));

        public static Predicate<T> AndFilter<T>(Predicate<T> p1, Predicate<T> p2) => x => p1(x) && p2(x);

        public static Predicate<T> AndFilter<T>(Predicate<T> p1, Predicate<T> p2, Predicate<T> p3) => x => p1(x) && p2(x) && p3(x);

        public static Predicate<T> AndFilter<T>(IEnumerable<Predicate<T>> ps) => x => ps.All(p => p(x));

        public static Predicate<T> LocationFilter<T>(FactDate when, Func<FactDate, T, FactLocation> f, Func<FactLocation, string> g, string s) => StringFilter<T>(x => g(f(when, x)), s);

        public static Predicate<T> StringFilter<T>(Func<T, string> f, string s) => x => StringMatches(f(x), s);

        public static Predicate<T> IntFilter<T>(Func<T, int> f, int i) => x => f(x) == i;

        public static Predicate<T> FamilyRelationFilter<T>(Func<T, IEnumerable<int>> f, int i) => x => f(x).Contains(i);

        public static Predicate<T> DateFilter<T>(Func<T, FactDate> f, FactDate d) => x => f(x).Overlaps(d);

        public static Predicate<T> IncompleteDataFilter<T>(int level, Predicate<T> certificatePresent,
            Func<T, FactDate> filterDate, Func<FactDate, T, FactLocation> filterLocation)
        {
            return t =>
            {
                if (certificatePresent(t))
                    return false;
                FactDate fd = filterDate(t);
                if (fd is null || !fd.IsExact)
                    return true;
                FactLocation l = filterLocation(fd, t);
                return level switch
                {
                    FactLocation.COUNTRY => (l.Country.Length == 0),
                    FactLocation.REGION => (l.Region.Length == 0),
                    FactLocation.SUBREGION => (l.SubRegion.Length == 0),
                    FactLocation.ADDRESS => (l.Address.Length == 0),
                    FactLocation.PLACE => (l.Place.Length == 0),
                    _ => true,
                };
            };
        }

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> l, Predicate<T> p) => l.Where(new Func<T, bool>(x => p(x)));

        public static IEnumerable<T> Filter<T>(this IList<T> l, Predicate<T> p) => (l as IEnumerable<T>).Filter(p);

        static bool StringMatches(string s1, string s2) => s1 is not null && s1.Equals(s2, StringComparison.OrdinalIgnoreCase);

        public static T MostCommon<T>(this IEnumerable<T> list) => (from i in list
                                                                    group i by i into grp
                                                                    orderby grp.Count() descending
                                                                    select grp.Key).First();

        public static int CountUnique<T>(this IEnumerable<T> list) => (from x in list
                                                                       select x).Distinct().Count();
    }
}
