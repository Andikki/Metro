using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metro.Model;

namespace Metro.DAL
{
    public class MetroLayout
    {
        private Dictionary<int, Station> stations = new Dictionary<int, Station>();
        private List<Route> routes = new List<Route>();
        public MetroLayout()
        {
            AddStation(1, "ОЧАКОВСКОЕ", 1, 1, 0, 8);
            AddStation(2, "БАЛТИКА", 1, 2, 0, 21);
            AddStation(3, "ОХОТА", 1, 3, 19, 29, true);
            AddStation(4, "ЖИГУЛИ", 1, 4, 50, 42, true);
            AddStation(5, "БОЧКА", 1, 5, 81, 54);
            AddStation(6, "ХАМОВНИКИ", 1, 6, 100, 63);
            AddStation(7, "АФАНАСИЙ", 1, 7, 100, 92);
            AddStation(8, "ЛОВЕНБРОЙ", 2, 1, 31, 4);
            AddStation(9, "ВАРШТАЙНЕР", 2, 2, 31, 17);
            AddStation(10, "ГЕССЕР", 2, 3, 31, 29);
            AddStation(11, "БЕКС", 2, 4, 31, 54, true);
            AddStation(12, "ПАУЛАНЕР", 2, 5, 50, 71, true);
            AddStation(13, "КРОМБАХЕР", 2, 6, 38, 88);
            AddStation(14, "АМСТЕЛ", 2, 7, 38, 100);
            AddStation(15, "ХУГАРДЕН", 4, 1, 100, 4);
            AddStation(16, "БАВАРИЯ", 4, 2, 100, 17);
            AddStation(17, "КАРЛСБЕРГ", 4, 3, 81, 29);
            AddStation(18, "ЛЕФФЕ", 4, 4, 50, 50);
            AddStation(19, "СТЕЛЛА", 4, 5, 31, 63, true);
            AddStation(20, "ХАЙНЕКЕН", 4, 6, 6, 75);
            AddStation(21, "ТУБОРГ", 4, 7, 6, 92);
            AddStation(22, "СТАРОПРАМЕН", 3, 1, 69, 0);
            AddStation(23, "КРУШОВИЦЕ", 3, 2, 69, 8);
            AddStation(24, "КОЗЕЛ", 3, 3, 69, 29, true);
            AddStation(25, "ВЕЛЬВЕТ", 3, 4, 69, 54, true);
            AddStation(26, "БУДВАЙЗЕР", 3, 5, 63, 71);
            AddStation(27, "УРКВЕЛЛ", 3, 6, 69, 83);
            AddStation(28, "ГАМБРИНУС", 3, 7, 69, 100);

            AddTwoWayRoute(1, 2, 7);
            AddTwoWayRoute(2, 3, 8);
            AddTwoWayRoute(3, 4, 2);
            AddTwoWayRoute(4, 5, 1);
            AddTwoWayRoute(5, 6, 3);
            AddTwoWayRoute(6, 7, 4);
            AddTwoWayRoute(8, 9, 5);
            AddTwoWayRoute(9, 10, 7);
            AddTwoWayRoute(10, 11, 2);
            AddTwoWayRoute(11, 12, 4);
            AddTwoWayRoute(12, 13, 3);
            AddTwoWayRoute(13, 14, 9);
            AddTwoWayRoute(15, 16, 5);
            AddTwoWayRoute(16, 17, 4);
            AddTwoWayRoute(17, 18, 5);
            AddTwoWayRoute(18, 19, 3);
            AddTwoWayRoute(19, 20, 5);
            AddTwoWayRoute(20, 21, 8);
            AddTwoWayRoute(22, 23, 7);
            AddTwoWayRoute(23, 24, 3);
            AddTwoWayRoute(24, 25, 4);
            AddTwoWayRoute(25, 26, 3);
            AddTwoWayRoute(26, 27, 3);
            AddTwoWayRoute(27, 28, 8);
            AddTwoWayRoute(3, 10, 4);
            AddTwoWayRoute(4, 18, 3);
            AddTwoWayRoute(5, 25, 1);
            AddTwoWayRoute(11, 19, 5);
            AddTwoWayRoute(12, 26, 3);
            AddTwoWayRoute(17, 24, 4);
        }

        public Station[] GetStations()
        {
            return stations.Values.OrderBy(station => station.LineId).ThenBy(station => station.OrderNumber)
                .ToArray();
        }

        public Route[] GetRoutes()
        {
            return routes.ToArray();
        }

        private void AddStation(int id, string name, int lineId, int orderNumber, int x, int y, bool showLeftOfDot = false)
        {
            stations.Add(id, new Station()
                {
                    Id = id,
                    Name = name,
                    LineId = lineId,
                    X = x,
                    Y = y,
                    ShowLeftOfDot = showLeftOfDot
                });
        }

        private void AddTwoWayRoute(int station1, int station2, int time)
        {
            AddRoute(station1, station2, time);
            AddRoute(station2, station1, time);
        }
        private void AddRoute(int station1, int station2, int time)
        {
            routes.Add(new Route()
                {
                    Station1 = stations[station1],
                    Station2 = stations[station2],
                    Time = time
                });
        }
    }
}
