using CarRentalCompany.Data;
using CarRentalCompany.Models;
using CarRentalCompany.Models.Enums;
using CarRentalCompany.Services.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CarRentalCompany.Services
{
    public class VeiculoService
    {
        private readonly ICarRentalCompanyContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VeiculoService(ICarRentalCompanyContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public List<Veiculo> FindAll()
        {
            List<Veiculo> vehicles = _context.Veiculo.ToList();

            CheckVehicleRented(vehicles);

            return vehicles;
        }

        private void CheckVehicleRented(List<Veiculo> vehicles)
        {
            List<Locacao> rents = _context.Locacao.ToList();

            foreach (var vehicle in vehicles)
            {
                bool check = rents.Any(obj => obj.VeiculoId == vehicle.Id && obj.Status == LocacaoStatus.Ativa);

                if (check == true && vehicle.Alugado == false)
                {
                    Veiculo veiculo = FindById(vehicle.Id);
                    veiculo.Alugado = true;
                    veiculo.DataAtualizado = DateTime.Now;

                    _context.Veiculo
                        .Update(veiculo)
                        .Property(x => x.DataCadastro).IsModified = false;

                    _context.SaveChanges();
                }
            }
        }

        public void Insert(Veiculo veiculo)
        {
            if (veiculo.ImageFile != null)
            {
                var path = this.ImagePath(veiculo);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    veiculo.ImageFile.CopyTo(fileStream);
                }
            }

            veiculo.DataCadastro = DateTime.Now;

            _context.Veiculo.Add(veiculo);
            _context.SaveChanges();
        }

        public Veiculo FindById(int id)
        {
            var veiculo = _context.Veiculo.FirstOrDefault(obj => obj.Id == id);

            if (veiculo == null)
            {
                throw new UserException("Veículo não foi encontrado");
            }

            return veiculo;
        }

        public void Update(Veiculo veiculo)
        {
            var _veiculo = _context.Veiculo.AsNoTracking().FirstOrDefault(x => x.Id == veiculo.Id);

            if (_veiculo == null)
            {
                throw new UserException("Veículo não encontrado");
            }

            veiculo.Imagem = _veiculo.Imagem;

            if (veiculo.ImageFile != null)
            {
                this.ImageDelete(_veiculo);

                var path = this.ImagePath(veiculo);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    veiculo.ImageFile.CopyTo(fileStream);
                }
            }

            veiculo.DataAtualizado = DateTime.Now;

            _context.Veiculo
                .Update(veiculo)
                .Property(x => x.DataCadastro).IsModified = false;

            _context.SaveChanges();
        }

        public void Remove(int? id)
        {
            var veiculo = _context.Veiculo.Find(id);

            if (veiculo == null)
            {
                throw new UserException("Veículo não encontrado");
            }

            this.ImageDelete(veiculo);

            _context.Veiculo.Remove(veiculo);
            _context.SaveChanges();
        }

        private string ImagePath(Veiculo veiculo)
        {
            string wwwRootRath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(veiculo.ImageFile.FileName);
            string extension = Path.GetExtension(veiculo.ImageFile.FileName);

            veiculo.Imagem = fileName + DateTime.Now.ToString("yymmssfff") + extension;

            return Path.Combine(wwwRootRath + "/images", veiculo.Imagem);
        }

        private void ImageDelete(Veiculo veiculo)
        {
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", veiculo.Imagem);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        public int Somar(int num, int num2)
        {
            return num + num2;
        }

        public int Multiplicar(int num, int num2)
        {
            return num * num2;
        }
    }
}

