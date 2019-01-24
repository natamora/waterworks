using Microsoft.AspNetCore.Mvc;
using System.Linq;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using Waterworks.Models;
using System.Collections.Generic;
using Waterworks.Data;
using Waterworks.Models.View;
using Waterworks.Models.Db.Waterworks;

namespace Waterworks.Controllers
{
    public class MapController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public MapController(ApplicationDbContext context)
        {
            dbContext = context;
        }
        public IActionResult Index()
        {
            var pointsList = dbContext.Obiekt.ToList();
            List<PointFeature> pointList = new List<PointFeature>();
            foreach (Obiekt o in pointsList)
            {
                pointList.Add(new PointFeature(o.Id, o.Geometria));
            }
            return View("~/Views/Map/Index.cshtml", pointList);
        }
    }
}