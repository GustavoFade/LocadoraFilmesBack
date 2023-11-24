using LocadoraFilmes.Domain.Abstractions.Common;
using LocadoraFilmes.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LocadoraFilmes.Infra.Data.Repositories.Common
{
    //criado um repository base para não ter que ficar criando e adicionando os logs para
    //cada repository, no caso, os logs de error já estão concentrados aqui
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity, TContext>
        where TEntity : Entity
        where TContext : DbContext
    {
        protected readonly ILogger<BaseRepository<TEntity, TContext>> Logger;
        protected TContext Context { get; }
        protected DbSet<TEntity> DbSet { get; }

        public BaseRepository(TContext context, ILogger<BaseRepository<TEntity, TContext>> logger)
        {
            Logger = logger;
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public virtual async Task<TEntity> SaveAsync(TEntity entity, CancellationToken cancellationToken)
        {
            try
            {
                var entitySaved = await DbSet.AddAsync(entity, cancellationToken);
                return entitySaved.Entity;
            }
            catch (Exception ex)
            {
                object[] infoError = new object[]
                {
                    new { nomeMetodo = nameof(SaveAsync) },
                    new { rotina = typeof(BaseRepository<,>).FullName },
                    new { exceptionInfo = ex },
                    new
                    {
                        parametros = new
                        {
                            TEntity = typeof(TEntity),
                        }
                    }
                };
                Logger.LogError("Erro ao salvar objeto no repositório", infoError);
                throw;
            }
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            try
            {
                DbSet.Update(entity);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                object[] infoError = new object[]
                {
                    new { nomeMetodo = nameof(UpdateAsync) },
                    new { rotina = typeof(BaseRepository<,>).FullName },
                    new { exceptionInfo = ex },
                    new
                    {
                        parametros = new
                        {
                            TEntity = typeof(TEntity),
                        }
                    }
                };
                Logger.LogError("Erro ao atualizar objeto no repositório", infoError);
                throw;

            }
        }

        public Task DeleteAsync(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Remove(entity);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                object[] infoError = new object[]
                {
                    new { nomeMetodo = nameof(DeleteAsync) },
                    new { rotina = typeof(BaseRepository<,>).FullName },
                    new { exceptionInfo = ex },
                    new
                    {
                        parametros = new
                        {
                            TEntity = typeof(TEntity),
                        }
                    }
                };
                Logger.LogError("Erro ao deletar o objeto no repositório", infoError);
                throw;
            }
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, bool onlyRead, CancellationToken cancellationToken)
        {
            try
            {
                if (onlyRead)
                {
                    return await DbSet.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);
                }
                return await DbSet.FirstOrDefaultAsync(predicate, cancellationToken);
            }
            catch (Exception ex)
            {

                object[] infoError = new object[]
                {
                    new { nomeMetodo = nameof(FindAsync) },
                    new { rotina = typeof(BaseRepository<,>).FullName },
                    new { exceptionInfo = ex },
                    new
                    {
                        parametros = new
                        {
                            TEntity = typeof(TEntity),
                        }
                    }
                };
                Logger.LogError("Erro ao localizar objeto no repositório", infoError);
                throw;
            }
        }

        public async Task<TEntity[]> FindAllAsync(bool onlyRead, CancellationToken cancellationToken)
        {
            try
            {
                if (onlyRead)
                {
                    await DbSet.AsNoTracking().ToArrayAsync(cancellationToken);
                }
                return await DbSet.ToArrayAsync(cancellationToken);
            }
            catch (Exception ex)
            {

                object[] infoError = new object[]
                {
                    new { nomeMetodo = nameof(FindAllAsync) },
                    new { rotina = typeof(BaseRepository<,>).FullName },
                    new { exceptionInfo = ex },
                    new
                    {
                        parametros = new
                        {
                            TEntity = typeof(TEntity),
                        }
                    }
                };
                Logger.LogError("Erro ao localizar os objetos no repositório", infoError);
                throw;
            }
        }
        public TContext GetContext()
        {
            return Context;
        }
    }

}
