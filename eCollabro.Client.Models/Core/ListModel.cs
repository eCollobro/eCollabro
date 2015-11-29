// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>

namespace eCollabro.Client.Models.Core
{
    /// <summary>
    /// ListModel - Class to return list object as per JQuery Data Table binding
    /// </summary>
    /// <typeparam name="TList"></typeparam>
    public class ListModel<TList>
    {
        public TList data { get; set; }

        public int recordsTotal { get; set; }

       
        public int recordsFiltered {get;set;}
        
        public int draw{get;set;}
        
        public int length{get;set;}
        
        public int start {get;set;}


    }
}
