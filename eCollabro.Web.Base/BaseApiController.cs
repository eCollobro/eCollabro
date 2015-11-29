// <copyright company="eCollabro">
// Copyright (c) 2014 All Rights Reserved 
// Collaborative Framework and CMS - eCollabro.com
// </copyright>
// <author>Anand Singh</author>
#region References

using eCollabro.Client.Models.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.ModelBinding;

#endregion

namespace eCollabro.Web.Base
{

    /// <summary>
    /// BaseApiController
    /// </summary>
    public class BaseApiController : ApiController
    {
        #region Methods

        /// <summary>
        /// SetPagingParameters
        /// </summary>
        protected void SetPagingParameters(RequestContextParameter requestParameter)
        {
            var drawParameter = Request.GetQueryNameValuePairs().Where(qry => qry.Key.Equals("draw")).FirstOrDefault().Value;
            if (drawParameter != null)
            {
                requestParameter.Draw = Convert.ToInt32(drawParameter);
                requestParameter.PageNumber = Convert.ToInt32(Request.GetQueryNameValuePairs().Where(qry => qry.Key.Equals("start")).FirstOrDefault().Value);
                requestParameter.PageSize = Convert.ToInt32(Request.GetQueryNameValuePairs().Where(qry => qry.Key.Equals("length")).FirstOrDefault().Value);
                dynamic order = JValue.Parse(Request.GetQueryNameValuePairs().Where(qry => qry.Key.Equals("order")).FirstOrDefault().Value);
                int orderByColumnNumber = Convert.ToInt32(order.column.Value);
                var columns = Request.GetQueryNameValuePairs().Where(qry => qry.Key.Equals("columns")).ToList();

                dynamic column = JValue.Parse(columns[orderByColumnNumber].Value);

                requestParameter.OrderByColumn = column.data.Value;
                requestParameter.OrderByDirection = order.dir.Value;
            }
        }

        /// <summary>
        /// GetListResult
        /// </summary>
        /// <typeparam name="TDataObhject"></typeparam>
        /// <param name="dataObject"></param>
        /// <returns></returns>
        protected HttpResponseMessage GetListResult<TDataObhject>(TDataObhject dataObject,RequestContextParameter requestParameter,ResponseContextParameter responseParameter)
        {
            return Request.CreateResponse(HttpStatusCode.OK, new ListModel<TDataObhject> { data = dataObject, recordsTotal = responseParameter.NumberOfRecords, recordsFiltered = responseParameter.NumberOfRecords, draw = requestParameter.Draw, length = requestParameter.PageSize, start = requestParameter.PageNumber });
        }

        /// <summary>
        /// ModelErrorSummary
        /// </summary>
        protected string ModelErrorSummary
        {
            get
            {
                StringBuilder error = new StringBuilder();
                foreach (ModelState modelState in ModelState.Values)
                {
                    foreach (ModelError modelError in modelState.Errors)
                    {
                        error.Append(modelError.ErrorMessage + Environment.NewLine);
                    }
                }
                return error.ToString();
            }
        }

        #endregion
    }
}
