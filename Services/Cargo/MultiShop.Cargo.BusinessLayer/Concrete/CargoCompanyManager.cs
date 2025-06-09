using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
    public class CargoCompanyManager : ICargoCompanyService
    {

        private readonly ICargoCompanyDal cargoCompanyDal;
        public CargoCompanyManager(ICargoCompanyDal cargoCompanyDal)
        {
            this.cargoCompanyDal = cargoCompanyDal;
        }

        public async Task TDeleteAsync(int id)
        {
            await cargoCompanyDal.DeleteAsync(id);
        }

        public async Task<List<CargoCompany>> TGetAllAsync()
        {
            return await cargoCompanyDal.GetAllAsync();
        }

        public async Task<CargoCompany> TGetByIdAsync(int id)
        {
            return await cargoCompanyDal.GetByIdAsync(id);
        }

        public async Task TInsertAsync(CargoCompany entity)
        {
            await cargoCompanyDal.InsertAsync(entity);
        }

        public async Task TUpdateAsync(CargoCompany entity)
        {
            await cargoCompanyDal.UpdateAsync(entity);
        }
    }
}
