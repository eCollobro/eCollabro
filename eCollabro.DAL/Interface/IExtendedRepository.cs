// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion 
namespace eCollabro.DAL.Interface
{
    public partial interface IExtendedRepository
    {
    }

    // Just to show - If DAL for modules are written separately their extended repository can be declared in module it self as partial classes
    public partial interface IExtendedRepository
    {
        ISecurityRepository SecurityRepository { get; }
    }


    public partial interface IExtendedRepository
    {
        IContentRepository ContentRepository { get; }
    }
}
