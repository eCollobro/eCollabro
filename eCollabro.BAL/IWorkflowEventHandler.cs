// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.BAL.Entities.Models;

#endregion

namespace eCollabro.BAL
{
    /// <summary>
    /// IWorkflowEventHandler
    /// </summary>
    public interface IWorkflowEventHandler
    {
        void TaskUpdated(UserTask beforeUpdate,UserTask afterUpdate);
    }
}
