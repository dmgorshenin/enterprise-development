using DispatchService.Model;

namespace DispatchService.Api.Services
{
    public class DriversService
    {
        private List<Driver> _drivers = [];
        private int Id = 1;

        public List<Driver> GetAll()
        {
            return _drivers;
        }

        public Driver GetById(int id)
        {
            return _drivers.First(d => d.Id == id);
        }

        public Driver Create(Driver driver)
        {
            driver.Id = Id++;
            _drivers.Add(driver);
            return _drivers.First(d => d.Id == driver.Id);
        }

        public bool Update(Driver driver)
        {
            var temp_driver = _drivers.Find(d => d.Id == driver.Id);
            if (temp_driver == null) throw new Exception("Driver not found");
            else
            {
                temp_driver.Id = driver.Id;
                temp_driver.Address = driver.Address;
                temp_driver.PhoneNumber = driver.PhoneNumber;
                temp_driver.DriverLicense = driver.DriverLicense;
                temp_driver.Passport = driver.Passport;
                temp_driver.FullName = driver.FullName;
                return true;
            }
        }

        public bool Delete(int id)
        {
            var temp_driver = _drivers.Find(d => d.Id == id);
            if (temp_driver == null) throw new Exception("Driver not found");
            else
            {
                bool res = _drivers.Remove(temp_driver);
                return res;
            }
        }
    }
}
