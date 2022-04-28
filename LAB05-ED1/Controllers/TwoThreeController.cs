using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using ClassLibrary;
using LAB05_ED1.Helpers;

namespace LAB05_ED1.Controllers
{
    public class TwoThreeController : Controller
    {
        private IWebHostEnvironment Environment;
        public TwoThreeController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        

        // GET: TwoThreeController
        public ActionResult Index()
        {
            if (Data.Instance.Searchlist.Count == 0)
                return View(Data.Instance.VehicleTree.ElementList.OrderBy(x => x.LicensePlate));
            else
            {
                List<Vehicle> aux = new List<Vehicle>();
                aux.Add(Data.Instance.Searchlist[0]);
                //var aux = Data.Instance.Searchlist[0];
                Data.Instance.Searchlist.Clear();
                return View(aux);
            }
                
        }

        


        [HttpPost]
        public ActionResult Index(IFormFile File)
        {
            if (File != null)
            {

                ViewBag.File = true;
                string path = Path.Combine(this.Environment.WebRootPath, "Uploads");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string FileName = Path.GetFileName(File.FileName);
                string filePath = Path.Combine(path, FileName);
                FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);



                string DataList = System.IO.File.ReadAllText(filePath);

                DataList.Trim();
                StreamReader read = new StreamReader(filePath);

                try
                {
                    string line = "";
                    while ((line = read.ReadLine()) != null)
                    {
                        string[] aux = line.Split(",");
                        Vehicle vehicle = new Vehicle
                        {
                            LicensePlate = aux[0],
                            Color = aux[1],
                            Owner = aux[2],
                            Latitude = Convert.ToDouble(aux[3]),
                            Longitude = Convert.ToDouble(aux[4])
                        };


                        Data.Instance.VehicleTree.Root = Data.Instance.VehicleTree.Insert(Data.Instance.VehicleTree.Root, vehicle);
                    }
                }
                catch
                {
                    return Content("There was an error with the file you're trying to import");
                }
            }

            return View();
        }

            // GET: TwoThreeController/Details/5
            public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TwoThreeController/Create
        public ActionResult Create()
        {
            return View(new Vehicle());
        }

        // POST: TwoThreeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                Vehicle vehicle = new Vehicle()
                {
                    LicensePlate = collection["LicensePlate"],
                    Color = collection["Color"],
                    Owner = collection["Owner"],
                    Latitude = Convert.ToDouble(collection["Latitude"]),
                    Longitude = Convert.ToDouble(collection["Longitude"]),
                };

                
                Data.Instance.VehicleTree.Root = Data.Instance.VehicleTree.Insert(Data.Instance.VehicleTree.Root, vehicle);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        public Action<Vehicle, Vehicle> EditData = (currentVehicle, EditedVehicle) =>
        {
            currentVehicle.Latitude = EditedVehicle.Latitude;
            currentVehicle.Longitude = EditedVehicle.Longitude;
        };

        // GET: TwoThreeController/Edit/5
        public ActionResult Edit(string id)
        {
            Vehicle aux = new Vehicle
            {
                LicensePlate = id
            };
            return View(Data.Instance.VehicleTree.Search(Data.Instance.VehicleTree.Root, aux));
        }

        [HttpPost]
        public ActionResult Search(string Search)
        {
            string licensePlate = Search;
            Vehicle vehicle = new Vehicle
            {
                LicensePlate = licensePlate
            };

            //List<Vehicle> SearchedVehicle = new List<Vehicle>();

            Data.Instance.Searchlist.Clear();
            Data.Instance.Searchlist.Add(Data.Instance.VehicleTree.Search(Data.Instance.VehicleTree.Root, vehicle));

            
            return RedirectToAction(nameof(Index));
        }




        // POST: TwoThreeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, IFormCollection collection)
        {
            try
            {
                Vehicle newData = new Vehicle
                {
                    LicensePlate = id,
                    Latitude = Convert.ToDouble(collection["Latitude"]),
                    Longitude = Convert.ToDouble(collection["Longitude"])

                };


                Data.Instance.VehicleTree.Edit(Data.Instance.VehicleTree.Root, newData,  EditData);
                Data.Instance.Searchlist.Clear();
                Data.Instance.Searchlist.Add(Data.Instance.VehicleTree.Search(Data.Instance.VehicleTree.Root, newData));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TwoThreeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TwoThreeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
