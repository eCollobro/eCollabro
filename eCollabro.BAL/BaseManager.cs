// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using eCollabro.BAL.Entities.Models;
using eCollabro.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using eCollabro.Common;
using System.Resources;
using eCollabro.DAL.Interface;
using eCollabro.Resources;
using log4net;
using eCollabro.DAL;
#endregion

namespace eCollabro.BAL
{
    /// <summary>
    /// BaseManager
    /// </summary>
    public abstract class BaseManager
    {
        #region Data Member

        private IUnitOfWork _eCollabroDbContext = null;
        private IUnitOfWork _adpDbContext = null;
        protected ResourceManager _coreValidationResourceManager = null;
        protected readonly ILog log = null;

        #endregion 

        #region property

        /// <summary>
        /// eCollabroContext
        /// </summary>
        protected IUnitOfWork eCollabroDbContext
        {
            get
            {
                if (_eCollabroDbContext == null)
                {
                    RequestContext currentContext = RequestContext.Current;
                    _eCollabroDbContext = currentContext.Get<IUnitOfWork>("eCollabroDbContext");
                    if (_eCollabroDbContext == null)
                    {
                        _eCollabroDbContext = new UnitOfWork(DbConnectionParameter.eCollabro);
                        currentContext.Add<IUnitOfWork>("eCollabroDbContext", _eCollabroDbContext);
                    }
                }
                return _eCollabroDbContext;
            }
        }

        protected IUnitOfWork ADPDbContext
        {
            get
            {
                if (_adpDbContext == null)
                {
                    RequestContext currentContext = RequestContext.Current;
                    _adpDbContext = currentContext.Get<IUnitOfWork>("ADPDbContext");
                    if (_adpDbContext == null)
                    {
                        _adpDbContext = new UnitOfWork(DbConnectionParameter.ADP);
                        currentContext.Add<IUnitOfWork>("ADPDbContext", _adpDbContext);
                    }
                }
                return _adpDbContext;
            }
        }

        #endregion 

        #region Constructor 

        public BaseManager()
        {
            _coreValidationResourceManager = new ResourceManager(typeof(CoreValidationMessages));
            log = LogManager.GetLogger(this.GetType());

        }

        #endregion

        #region Methods

        /// <summary>
        /// GetContentResponse
        /// </summary>
        /// <typeparam name="TContent"></typeparam>
        /// <param name="content"></param>
        /// <param name="userPermissions"></param>
        /// <returns></returns>
        protected TContent GetContentResponse<TContent>(TContent content, List<PermissionEnum> userPermissions)
        {
            if (content == null)
            {
                if (!userPermissions.Contains(PermissionEnum.ViewInactiveContent))
                {
                    throw new BusinessException(string.Format(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.RecordNotExistOrInactive), "Requested content"), CoreValidationMessagesConstants.RecordNotExistOrInactive);
                }
                else
                {
                    throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.RecordNotFound), CoreValidationMessagesConstants.RecordNotFound);
                }
            }
            return content;
        }

        /// <summary>
        /// GetContentResponse
        /// </summary>
        /// <typeparam name="TContent"></typeparam>
        /// <param name="content"></param>
        /// <param name="userPermissions"></param>
        /// <returns></returns>
        protected TContent GetContentResponse<TContent>(TContent content)
        {
            if (content == null)
            {
                    throw new BusinessException(_coreValidationResourceManager.GetString(CoreValidationMessagesConstants.RecordNotFound), CoreValidationMessagesConstants.RecordNotFound);
            }
            return content;
        }



        /// <summary>
        /// UserContextDetails
        /// </summary>
        public UserContext UserContextDetails
        {
            get
            {
                if (RequestContext.Current != null)
                    return RequestContext.Current.Get<UserContext>("UserContext");
                else
                    return null;
            }
        }



        /// <summary>
        /// HandleError
        /// </summary>
        /// <param name="ex"></param>
        public bool HandleError(Exception ex)
        {
            bool handled = true;
            // add exceptions for which error need not to throw
            if (ex.GetType() != typeof(DBConcurrencyException) && ex.GetType() != typeof(BusinessException))
            {
                log.Error(ex.Message,ex);
            }
            handled = false;
            return handled;
        }

        /// <summary>
        /// Desrialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public T Desrialize<T>(string xmlData)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T returnObject = default(T);
            using (TextReader reader = new StringReader(xmlData))
            {
                returnObject = (T)xmlSerializer.Deserialize(reader);
            }
            return returnObject;
        }

        /// <summary>
        /// Serialize
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToSerialize"></param>
        /// <returns></returns>
        public string Serialize<T>(T objectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            string xmlResult = string.Empty;
            using (var stringWriter = new StringWriter())
            {
                var xmlWriter = XmlWriter.Create(stringWriter);
                xmlSerializer.Serialize(xmlWriter, objectToSerialize);
                xmlResult = stringWriter.ToString();
            }
            return xmlResult;
        }

        #endregion 
    }
}
