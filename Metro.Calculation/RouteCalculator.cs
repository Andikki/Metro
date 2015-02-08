using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metro.Model;

namespace Metro.Calculation
{
    public class RouteCalculator
    {

        private IEnumerable<Station> stations;
        private IEnumerable<Route> routes; 
        private ILookup<int, Route> routesLookup; //по ID исходной станции
        private ILookup<int, Route> reversedRoutesLookup; //по ID конечной станции

        public RouteCalculator(IEnumerable<Station> stations, IEnumerable<Route> routes)
        {
            this.routes = routes;
            this.stations = stations;            
            this.routesLookup = routes.ToLookup(route => route.Station1.Id);
            this.reversedRoutesLookup = routes.ToLookup(route => route.Station2.Id);
        }

        public Route[] FindShortestRoute(Station departureStation, Station arrivalStation)
        {
            if (departureStation != null && arrivalStation != null)
            {
                return FindShortestRoute(departureStation.Id, arrivalStation.Id);
            }
            else
            {
                return new Route[0] { };
            }
        
        }

        public Route[] FindShortestRoute(int departureStation, int arrivalStation)
        {
            var stationsToVisit = new HashSet<int>(stations.Select(station => station.Id));
            Dictionary<int, int?> calculatedWayTimes = stationsToVisit
                .ToDictionary(station => station, station => (int?)null);
            calculatedWayTimes[departureStation] = 0;

            int currentStation = departureStation;
            int currentTime;
            //Станции обрабатываются в порядке удаления от исходной.
            while (true)
            {
                if (currentStation == arrivalStation)
                {
                    //Пункт назначения достигнут.
                    return TracebackRoute(departureStation, arrivalStation, calculatedWayTimes);
                }
                currentTime = calculatedWayTimes[currentStation].Value;
                //Смотрим, куда и за сколько можно доехать из текущей точки.                
                foreach (var outgoingRoute in routesLookup[currentStation].Where(route => stationsToVisit.Contains(route.Station2.Id)))
                {
                    var outgoingRouteTime = outgoingRoute.Time + currentTime;
                    var outgoingRouteDestinationId = outgoingRoute.Station2.Id;
                    //Если более быстрого пути туда раньше не находили, сохраняем это время.
                    if (calculatedWayTimes[outgoingRouteDestinationId] == null || calculatedWayTimes[outgoingRouteDestinationId] > outgoingRouteTime)
                    {
                        calculatedWayTimes[outgoingRouteDestinationId] = outgoingRouteTime;
                    }
                }
                //До текущей станции найден самый близкий путь, больше на неё пытаться ездить не надо.
                stationsToVisit.Remove(currentStation);

                if (!stationsToVisit.Where(station => calculatedWayTimes[station].HasValue).Any())
                {
                    //TODO: пути до станции назначения не существует
                    throw new NotImplementedException();
                }
                //Следующая ближайшая станция.
                currentStation = stationsToVisit
                    .Where(station => calculatedWayTimes[station].HasValue)
                    .OrderBy(station => calculatedWayTimes[station])
                    .First();
            }
        }

        //private Waypoint[] TracebackRoute(int station1, int station2, Dictionary<int, int?> calculatedWayTimes)
        //{
        //    var routeDetails = new List<Waypoint>();
        //    int waypoint = station2;
        //    int wayTime;
        //    while (waypoint != station1)
        //    {
        //        wayTime = calculatedWayTimes[waypoint].Value;
        //        routeDetails.Add(new Waypoint() { Station = stations[waypoint], Time = wayTime });
        //        //Не рассчитываем, что в обратную сторону времена такие же.
        //        waypoint = routes.SelectMany(route => route.Value, (source, route) => new { source = source.Key, route })
        //            .Where(route => route.route.Key == waypoint && calculatedWayTimes[route.source] == wayTime - route.route.Value)
        //            .First().source;
        //    }
        //    routeDetails.Add(new Waypoint() { Station = stations[station1], Time = 0 });
        //    return routeDetails.Reverse<Waypoint>().ToArray();
        //}

        private Route[] TracebackRoute(int station1, int station2, Dictionary<int, int?> calculatedWayTimes)
        {            
            var routeDetails = new List<Route>();
            int waypoint = station2;
            int wayTime;
            while (waypoint != station1)
            {
                wayTime = calculatedWayTimes[waypoint].Value;
                //Не рассчитываем, что в обратную сторону времена такие же.
                var previousRoute = reversedRoutesLookup[waypoint]
                    .Where(route => calculatedWayTimes[route.Station1.Id] == wayTime - route.Time)
                    .First();
                routeDetails.Add(previousRoute);
                waypoint = previousRoute.Station1.Id;
            }
            return routeDetails.Reverse<Route>().ToArray();
        }
    }
}
