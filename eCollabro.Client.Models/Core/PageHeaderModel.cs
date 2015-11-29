// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using System.Collections.Generic;

#endregion

namespace eCollabro.Client.Models.Core
{
    /// <summary>
    /// pageButtons
    /// </summary>
    public class PageButton
    {
        public string Id { get; set; }

        public string ButtonText { get; set; }

        public string Icon { get; set; }

        public string Method { set; get; }

        public string CssClass {get;set;}

        public bool HideOnFooter { get; set; }

        public bool HideOnHeader { get; set; } 
    }

    /// <summary>
    /// PageHeaderModel
    /// </summary>
    public class PageHeaderModel
    {
        public string ViewId { get; set; }

        public string PageTitle { get; set; }

        public List<PageButton> PageButtons { get; set; }

        public bool ValidateForm { set; get; }

     }

    /// <summary>
    /// PageFooterModel
    /// </summary>
    public class PageFooterModel
    {
        public List<PageButton> PageButtons { get; set; }
    }
}
