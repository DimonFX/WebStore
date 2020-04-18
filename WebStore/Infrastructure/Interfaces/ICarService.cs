using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Infrastructure.Interfaces
{
    public interface ICarService
    {
        /// <summary>
        /// Получение списка машин
        /// </summary>
        /// <returns></returns>
        IEnumerable<CarViewModel> GetAll();

        /// <summary>
        /// Получение машины по id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        CarViewModel GetById(int id);

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        void Commit();

        /// <summary>
        /// Добавить новую
        /// </summary>
        /// <param name="model"></param>
        void AddNew(CarViewModel model);

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
