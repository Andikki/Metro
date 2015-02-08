using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metro.Calculation;
using Metro.DAL;
using Metro.Framework;
using Metro.Model;

namespace Metro
{
    public class MainWindowViewModel : NotificationObject
    {
        public MainWindowViewModel()
        {
            var data = new MetroLayout();
            var stations = data.GetStations();
            var routes = data.GetRoutes();
            StationList = stations;
            RouteList = routes
                .Distinct(directionlessRouteComparer)
                .Select(route => new RouteViewModel(route)).ToArray();
            calculator = new RouteCalculator(stations, routes);
        }

        public Station[] StationList { get; private set; }
        public RouteViewModel[] RouteList { get; private set; }

        private Station departureStation;
        public Station DepartureStation
        {
            get { return departureStation; }
            set
            {
                SetProperty(ref departureStation, value, () => DepartureStation);
                CalculateRoute();
            }
        }
        private Station arrivalStation;
        public Station ArrivalStation
        {
            get { return arrivalStation; }
            set
            {
                SetProperty(ref arrivalStation, value, () => ArrivalStation);
                CalculateRoute();
            }
        }

        private DirectionlessRouteComparer directionlessRouteComparer = new DirectionlessRouteComparer();
        private RouteCalculator calculator;
        private string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value, () => Message); }
        }
        private List<WaypointViewModel> routeDetails;
        public List<WaypointViewModel> RouteDetails
        {
            get { return routeDetails; }
            set { SetProperty(ref routeDetails, value, () => RouteDetails); }
        }

        public void SelectStation(Station station)
        {
            if (DepartureStation == null) { DepartureStation = station; }
            else { ArrivalStation = station; }
        }

        public void CalculateRoute()
        {
            ClearRoutesHighlight();
            Message = null;
            var routes = calculator.FindShortestRoute(DepartureStation, ArrivalStation);
            var waypoints = new List<WaypointViewModel>();
            int time = 0;
            if (routes.Any())
            {
                waypoints.Add(new WaypointViewModel() { Station = DepartureStation, Time = 0 });
                foreach (var route in routes)
                {
                    time += route.Time;
                    waypoints.Add(new WaypointViewModel() { Station = route.Station2, Time = time });
                }
                HighlightRoutes(routes);
                Message = String.Format("Общее время в пути: {0}", time);
            }
            RouteDetails = waypoints;
        }

        private void ClearRoutesHighlight()
        {
            foreach (var route in RouteList)
            {
                route.IsHighlighted = false;
            }
        }

        private void HighlightRoutes(IEnumerable<Route> routes)
        {
            foreach (var routeVM in RouteList.Join(routes, routeVM => routeVM.Route, route => route,
                (routeVM, route) =>
                {
                    routeVM.IsHighlighted = true;
                    return routeVM;
                },
                directionlessRouteComparer))
            { }
        }
    }
}
