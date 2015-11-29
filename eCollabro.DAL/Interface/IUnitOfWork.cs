// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.BAL.Entities.Models;
using System;
using System.Collections.Generic;

#endregion

namespace eCollabro.DAL.Interface
{
    public interface IUnitOfWork : IUnitOfWorkForService
    {
        void Save();
        void Dispose(bool disposing);
        IExtendedRepository ExtendedRepository();
        void ExecuteScripts(List<string> scripts);
    }

    // To be used in services e.g. ICustomerService, does not expose Save()
    // or the ability to Commit unit of work
    public interface IUnitOfWorkForService: IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    }
}