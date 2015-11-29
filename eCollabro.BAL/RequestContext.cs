// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.ServiceModel;
using Microsoft.Practices.Unity;
using eCollabro.DAL.Interface;
using System.Web;
using eCollabro.BAL.Entities.Models;
using eCollabro.Utilities;
using System.Collections;

#endregion 

namespace eCollabro.BAL
{
        /// <summary>
        /// RepositoryManagerContext
        /// </summary>
        public class RequestContext : IExtension<OperationContext>,IDisposable
        {

            #region Data Member

            private Hashtable _contextObjects;
            private bool _disposed;
            private UserContext _userContext;

            // to be used for single threaded application - Windows Application
            private static RequestContext _requestContext;

            #endregion

            #region Constructor 

            public RequestContext()
            {
                _contextObjects = new Hashtable();
            }

            #endregion 

            #region Methods

            /// <summary>
            /// GetInstance
            /// </summary>
            /// <returns></returns>
            public static RequestContext Current
            {
                get
                {
                    RequestContext requestContext = null;
                    if (OperationContext.Current != null) // WCF call
                    {
                        requestContext = OperationContext.Current.Extensions.Find<RequestContext>();
                        if (requestContext == null)
                        {
                            requestContext = new RequestContext();
                            OperationContext.Current.Extensions.Add(requestContext);
                        }
                    }
                    else if (HttpContext.Current != null) // Web Call
                    {
                        requestContext = HttpContext.Current.Items["RequestContext"] as RequestContext;
                        if (requestContext == null)
                        {
                            requestContext = new RequestContext();
                            HttpContext.Current.Items.Add("RepositoryManagerContext", requestContext);
                        }
                    }
                    else // Single thread Application Call
                    {
                        if (_requestContext == null)
                        {
                            _requestContext = new RequestContext();
                        }
                        requestContext = _requestContext;
                    }
                    return requestContext;
                }
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!_disposed && disposing)
                {
                    
                }
                _disposed = true;
            }

            public void Add<T>(String key, T contextObject) where T : class
            {
                if (_contextObjects.ContainsKey(key))
                {
                    throw new Exception("Object already added by the same key to context.");
                }
                else
                {
                    _contextObjects.Add(key, contextObject);
                }
            }

            public T Get<T>(String key) where T : class
            {
                if (_contextObjects.ContainsKey(key))
                {
                    return _contextObjects[key] as T;
                }
                else
                {
                    return null;
                }
            }


            public void Attach(OperationContext owner)
            {
                // do nothing
            }

            public void Detach(OperationContext owner)
            {
                // do nothing
            }

         
     
            #endregion

        }

}
