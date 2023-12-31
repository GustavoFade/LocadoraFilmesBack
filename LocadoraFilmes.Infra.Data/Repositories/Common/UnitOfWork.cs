﻿using LocadoraFilmes.Domain.Abstractions.Common;
using LocadoraFilmes.Infra.Data.Context;
using System;
using System.Threading.Tasks;

namespace LocadoraFilmes.Infra.Data.Repositories.Common
{
    //criado o UnitOfWork para concentrarmos em uma única transação na request
    //se houver alguma falha, nada vai ser salvado no banco
    //se houver sucesso, chamaremos o CommitTransactionAsync e será salvo no banco
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitTransactionAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
