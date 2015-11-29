// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System.Runtime.Serialization;

#endregion
namespace eCollabro.Service.DataContracts.Content
{
    /// <summary>
    /// ContentCommentRequestDC
    /// </summary>
    [DataContract]
    public class ContentCommentRequestDC
    {
        [DataMember]
        public int ContextId { get; set; }

        [DataMember]
        public int ContextContentId { get; set; }
    }
}
