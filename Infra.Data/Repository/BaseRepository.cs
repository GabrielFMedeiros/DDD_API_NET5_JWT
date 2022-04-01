using Domain.Interfaces;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class BaseRepository<Entity> : IBase<Entity>, IDisposable where Entity : class
    {
        private readonly DbContextOptions<DbContext> _options;
        public BaseRepository()
        {
            _options = new DbContextOptions<DbContext>();
        }
        public Task Add(Entity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Entity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Entity>> List()
        {
            throw new NotImplementedException();
        }

        public Task<Entity> SearchById(Entity entity)
        {
            throw new NotImplementedException();
        }

        public Task Updade(Entity entity)
        {
            throw new NotImplementedException();
        }

        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);


        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
        #endregion

    }
}
