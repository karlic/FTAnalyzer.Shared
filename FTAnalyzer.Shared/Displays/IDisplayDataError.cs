﻿using FTAnalyzer.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayDataError
    {
#if __PC__
        [ColumnDetail("Icon", 50)]
        System.Drawing.Image Icon { get; }
#endif
        [ColumnDetail("Error Type", 200)]
        string ErrorType { get; }
        [ColumnDetail("Ref", 60)]
        string Reference { get; }
        [ColumnDetail("Name", 200)]
        string Name { get; }
        [ColumnDetail("Description", 500)]
        string Description { get; }
        [ColumnDetail("Born", 150)]
        FactDate Born { get; }
        [ColumnDetail("Died", 150)]
        FactDate Died { get; }
#if __PC__
        [ColumnDetail("Family", 50)]
        bool IsFamily { get; }
#elif __MACOS__ || __IOS__
        [ColumnDetail("Family", 50)]
        string IsFamily { get; }
#endif
        [ColumnDetail("Forenames", 100)]
        string Forenames { get; }
        [ColumnDetail("Surname", 75)]
        string Surname { get; }
    }
}