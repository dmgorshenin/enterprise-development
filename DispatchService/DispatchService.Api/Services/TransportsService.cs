using DispatchService.Model;

namespace DispatchService.Api.Services
{
    public class TransportsService
    {
        private List<Transport> _transports = [];
        private int Id = 1;

        public List<Transport> GetAll()
        {
            return _transports;
        }

        public Transport GetById(int id)
        {
            return _transports.First(d => d.Id == id);
        }

        public Transport Create(Transport transport)
        {
            transport.Id = Id++;
            _transports.Add(transport);
            return _transports.First(d => d.Id == transport.Id);
        }

        public bool Update(Transport transport)
        {
            var temp_transport = _transports.Find(d => d.Id == transport.Id);
            if (temp_transport == null) throw new Exception("Transport not found");
            else
            {
                temp_transport.Id = transport.Id;
                temp_transport.IsLowFloor = transport.IsLowFloor;
                temp_transport.YearOfManufacture = transport.YearOfManufacture;
                temp_transport.MaxCapacity = transport.MaxCapacity;
                temp_transport.LicensePlate = transport.LicensePlate;
                temp_transport.ModelName = transport.ModelName;
                temp_transport.Type = transport.Type;
                return true;
            }
        }

        public bool Delete(int id)
        {
            var temp_transport = _transports.Find(d => d.Id == id);
            if (temp_transport == null) throw new Exception("Transport not found");
            else
            {
                bool res = _transports.Remove(temp_transport);
                return res;
            }
        }
    }
}