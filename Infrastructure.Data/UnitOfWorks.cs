using Core;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork 
    {
        private PipeInspectionLogContext _context = new PipeInspectionLogContext();
        private GenericRepository<PipeLog> _pipeLogRepository;
        private GenericRepository<Diameter> _diameterRepository;
        private GenericRepository<Nomenclature> _nomenclatureRepository;



        public GenericRepository<PipeLog> PipeLogRepository
        {
            get
            {
                if (_pipeLogRepository == null)
                    _pipeLogRepository = new GenericRepository<PipeLog>(_context);
                return _pipeLogRepository;
            }
        }

        public GenericRepository<Diameter> DiameterRepository
        {
            get
            {
                if (_diameterRepository == null)
                    _diameterRepository = new GenericRepository<Diameter>(_context);
                return _diameterRepository;
            }
        }

        public GenericRepository<Nomenclature> NomenclatureRepository
        {
            get
            {
                if (_nomenclatureRepository == null)
                    _nomenclatureRepository = new GenericRepository<Nomenclature>(_context);
                return _nomenclatureRepository;
            }
        }

        

        //общие для всех репозиториев методы
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
