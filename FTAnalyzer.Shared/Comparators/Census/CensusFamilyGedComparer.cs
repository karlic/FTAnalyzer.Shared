﻿namespace FTAnalyzer
{
    class CensusFamilyGedComparer : Comparer<CensusIndividual>
    {
        public override int Compare(CensusIndividual? x, CensusIndividual? y)
        {
            int r = string.Compare(x.FamilyID, y.FamilyID, StringComparison.Ordinal);
            if (r == 0)
            {
                r = x.Position - y.Position;
            }
            return r;
        }
    }
}
