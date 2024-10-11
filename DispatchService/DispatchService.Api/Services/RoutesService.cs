namespace DispatchService.Api.Services
{
    public class RoutesService
    {
        private List<Model.Route> _routes = [];
        private int Id = 1;

        public List<Model.Route> GetAll()
        {
            return _routes;
        }

        public Model.Route GetById(int id)
        {
            return _routes.First(d => d.Id == id);
        }

        public Model.Route Create(Model.Route driver)
        {
            driver.Id = Id++;
            _routes.Add(driver);
            return _routes.First(d => d.Id == driver.Id);
        }

        public bool Update(Model.Route driver)
        {
            var temp_driver = _routes.Find(d => d.Id == driver.Id);
            if (temp_driver == null) throw new Exception("Route not found");
            else
            {
                temp_driver.Id = driver.Id;
                temp_driver.RouteNumber = driver.RouteNumber;
                temp_driver.AssignedDriver = driver.AssignedDriver;
                temp_driver.AssignedTransport = driver.AssignedTransport;
                temp_driver.StartTime = driver.StartTime;
                temp_driver.EndTime = driver.EndTime;
                return true;
            }
        }

        public bool Delete(int id)
        {
            var temp_driver = _routes.Find(d => d.Id == id);
            if (temp_driver == null) throw new Exception("Route not found");
            else
            {
                bool res = _routes.Remove(temp_driver);
                return res;
            }
        }
    }
}
