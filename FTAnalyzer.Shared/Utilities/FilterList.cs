﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

// http://www.timvw.be/2009/11/06/presenting-filterlist/

// example usage
//
//void textBoxFilter_KeyUp(object sender, KeyEventArgs e)
//{
// var filterChars = this.textBoxFilter.Text.ToLower();
// this.Filter(filterChars);
//}

//void Filter(string filterChars)
//{
// var persons = (FilterList<person>)this.dataGridView1.DataSource;
// persons.Filter(p => p.Firstname.ToLower().Contains(filterChars));
//}

namespace FTAnalyzer.Utilities
{
    public class FilterList<T> : SortableBindingList<T>
    {
        readonly List<T> allItems = new();

        public FilterList()
        {
        }

        public FilterList(IEnumerable<T> elements)
            : base(elements) => allItems.AddRange(elements);

        public void Filter(Predicate<T> filter)
        {
            if (filter is null)
                throw new ArgumentNullException(nameof(filter));

            ApplyFilter(filter);
            if (IsSortedCore)
            {
                ApplySortCore(SortPropertyCore, SortDirectionCore);
            }
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected virtual void ApplyFilter(Predicate<T> filter)
        {
            var wantedItems = this.allItems.FindAll(filter);
            
            Items.Clear();
            foreach (var item in wantedItems)
            {
                Items.Add(item);
            }
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            allItems.Add(this[index]);
        }

        protected override void RemoveItem(int index)
        {
            allItems.Remove(this[index]);
            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            allItems.Clear();
        }

        protected override void SetItem(int index, T item)
        {
            allItems[allItems.IndexOf(this[index])] = item;
            base.SetItem(index, item);
        }
    }
}
