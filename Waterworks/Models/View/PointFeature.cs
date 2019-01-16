using System;
using System.Collections.Generic;
using System.Linq;
using NetTopologySuite.Features;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using GeoAPI.Geometries;

namespace Waterworks.Models.View
{
    public class PointFeature
    {
        public int Id { get; set; }
        public string StringGeometry { get; set; }
        public PointFeature(int id, Point point)
        {
            Id = id;
            StringGeometry = WKTWriter.ToPoint(new Coordinate(point.X, point.Y));
        }
    }
}
