using Metro.Framework;
using Metro.Model;

namespace Metro
{
    public class RouteViewModel : NotificationObject
    {
        public RouteViewModel(Route route)
        {
            this.Route = route;
        }

        public Route Route { get; set; }
        public Station Station1 { get { return Route.Station1; } }
        public Station Station2 { get { return Route.Station2; } }
        public int RouteLineId
        {
            get
            {
                return Station1.LineId == Station2.LineId ? Station1.LineId : 0;
            }
        }

        private bool isHighlighted;
        public bool IsHighlighted
        {
            get { return isHighlighted; }
            set { SetProperty(ref isHighlighted, value, () => IsHighlighted); }
        }
    }
}
