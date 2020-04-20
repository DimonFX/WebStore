using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryCarService: ICarService
    {
        List<CarViewModel> _cars;
        public InMemoryCarService()
        {
            _cars = new List<CarViewModel>
            {
                new CarViewModel
                {
                    Id = 1,
                    BodyType = "Хэтчбек",
                    Color = "Красный",
                    Drive= "Передний",
                    EngineVolume = 1.6,
                    Mark = "Nissan",
                    CarModel = "Almera",
                    Transmission = "МКПП",
                    YearRelease = 2018
                },
                new CarViewModel
                {
                    Id = 2,
                    BodyType = "Х",
                    Color = "Черный",
                    Drive= "Задний",
                    EngineVolume = 1.6,
                    Mark = "ВАЗ 2107",
                    CarModel = "LADA",
                    Transmission = "МКПП",
                    YearRelease = 2004
                }
            };
        }
        public void AddNew(CarViewModel model)
        {
            if (_cars.Count > 0) model.Id = _cars.Max(e => e.Id);
            model.Id += 1;
            _cars.Add(model);
        }

        public void Commit()
        {
            //throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var car = GetById(id);

            if (car is null)
                return;

            _cars.Remove(car);
        }

        public IEnumerable<CarViewModel> GetAll()
        {
            return _cars;
        }

        public CarViewModel GetById(int id)
        {
            return _cars.FirstOrDefault(x => x.Id == id);
        }
    }
}
