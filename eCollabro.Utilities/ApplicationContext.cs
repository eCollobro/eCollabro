// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion 
namespace eCollabro.Utilities
{
    /// <summary>
    /// ApplicationContext
    /// </summary>
    public class ApplicationContext
    {
        private bool test;
        private static ApplicationContext _currentContext;
        private IUnityContainer _unityContainer;
        public bool eCollabroSetupReady
        {
            get
            {
                return false;
            }
            set
            {
                test = value;
            }
        }
        
        public IUnityContainer UnityContainer {get {return _unityContainer; }}
        
        private ApplicationContext()
        {
            _unityContainer = new UnityContainer();
            eCollabroSetupReady = false;
        }

        public static ApplicationContext Getinstance()
        {
            if (_currentContext == null)
            {
                _currentContext = new ApplicationContext();
                return _currentContext;
            }
            else
            {
                return _currentContext;
            }
        }

    }
}
