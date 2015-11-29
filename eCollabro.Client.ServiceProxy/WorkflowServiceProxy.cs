﻿// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.ServiceModel;
using eCollabro.Service.ServiceContracts;
using eCollabro.Client.ServiceProxy.Interface;

#endregion

namespace eCollabro.Client.ServiceProxy
{
    /// <summary>
    /// WorkflowServiceProxy
    /// </summary>
    public class WorkflowServiceProxy : BaseBusinessProxy<IWorkflowService>, IWorkflowProxy
    {
        private string _endpointServiceConfigurationName;

        /// <summary>
        /// EndpointServiceConfigurationName
        /// </summary>
        public override string EndpointServiceConfigurationName
        {
            get
            {
                if (string.IsNullOrEmpty(_endpointServiceConfigurationName))
                    _endpointServiceConfigurationName = "BasicHttpBinding_IWorkflowService";
                return _endpointServiceConfigurationName;
            }
            set
            {
                _endpointServiceConfigurationName = value;
            }
        }
    }

}
