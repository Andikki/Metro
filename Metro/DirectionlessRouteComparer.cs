using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metro.Model;

namespace Metro
{
    /// <summary>
    /// Считает маршруты (Станция1 - Станция2) и (Станция2 - Станция1) одинаковыми.
    /// </summary>
    class DirectionlessRouteComparer : IEqualityComparer<Route>
    {
        public bool Equals(Route x, Route y)
        {
            return x.Station1.Id == y.Station1.Id && x.Station2.Id == y.Station2.Id
                || x.Station1.Id == y.Station2.Id && x.Station2.Id == y.Station1.Id;
        }

        public int GetHashCode(Route obj)
        {
            return obj.Station1.Id ^ obj.Station2.Id;
        }
    }
}
