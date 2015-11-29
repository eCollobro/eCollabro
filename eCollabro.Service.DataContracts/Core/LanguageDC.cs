// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References 

using System.Runtime.Serialization;

#endregion
namespace eCollabro.Service.DataContracts.Core
{
    /// <summary>
    /// LanguageDC
    /// </summary>
    [DataContract]
    public class LanguageDC
    {
        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string LanguageCode { get; set; }

        [DataMember]
        public string Language { get; set; }
    }
}
