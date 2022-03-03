using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestSharp
{
    public static class RestSharpExtensions
    {
        #region with default error model
        /// <summary>
        /// Tries to return a model of type: T
        /// throws exception when HttpResponse Status Code is not Success(200)*
        /// Tries to parse exception Json into ApiErrorModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ApiException">This exception is thrown if Response Status Code is not Success</exception>
        /// <returns></returns>
        public static async Task<T> PostApiAsync<T>(this RestClient client, RestRequest request, CancellationToken cancellationToken = default)
        {
            return await PostAsyncSafe<T, ApiErrorModel>(client, request, cancellationToken);
        }

        /// <summary>
        /// Tries to return a model of type: T
        /// throws exception when HttpResponse Status Code is not Success(200)*
        /// Tries to parse exception Json into ApiErrorModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ApiException">This exception is thrown if Response Status Code is not Success</exception>
        /// <returns></returns>
        public static async Task<T> GetApiAsync<T>(this RestClient client, RestRequest request, CancellationToken cancellationToken = default)
        {
            return await GetAsyncSafe<T, ApiErrorModel>(client, request, cancellationToken);
        }

        /// <summary>
        /// Tries to return a model of type: T
        /// throws exception when HttpResponse Status Code is not Success(200)*
        /// Tries to parse exception Json into ApiErrorModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ApiException">This exception is thrown if Response Status Code is not Success</exception>
        /// <returns></returns>
        public static async Task<T> PutApiAsync<T>(this RestClient client, RestRequest request, CancellationToken cancellationToken = default)
        {
            return await PutAsyncSafe<T, ApiErrorModel>(client, request, cancellationToken);
        }

        /// <summary>
        /// Tries to return a model of type: T
        /// throws exception when HttpResponse Status Code is not Success(200)*
        /// Tries to parse exception Json into ApiErrorModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ApiException">This exception is thrown if Response Status Code is not Success</exception>
        /// <returns></returns>
        public static async Task<T> DeleteApiAsync<T>(this RestClient client, RestRequest request, CancellationToken cancellationToken = default)
        {
            return await DeleteAsyncSafe<T, ApiErrorModel>(client, request, cancellationToken);
        }
        #endregion

        #region with error models
        /// <summary>
        /// throws exception when HttpResponse Status Code is not Success(200)*
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="ExceptionModel"></typeparam>
        /// <param name="client"></param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="RestException{E}"></exception>
        public static async Task<T> PostAsyncSafe<T,ExceptionModel>(this RestClient client, RestRequest request, CancellationToken cancellationToken = default)
        {
            var response = await client.ExecutePostAsync<T>(request, cancellationToken);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            if(typeof(ExceptionModel) == typeof(ApiErrorModel))
            {
                throw new ApiException(response);
            }

            throw new RestException<ExceptionModel>(response);
        }



        public static async Task<T> PutAsyncSafe<T,ExceptionModel>(this RestClient client, RestRequest request, CancellationToken cancellationToken = default)
        {
            var response = await client.ExecutePutAsync<T>(request, cancellationToken);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            if (typeof(ExceptionModel) == typeof(ApiErrorModel))
            {
                throw new ApiException(response);
            }

            throw new RestException<ExceptionModel>(response);
        }


        public static async Task<T> GetAsyncSafe<T,ExceptionModel>(this RestClient client, RestRequest request, CancellationToken cancellationToken = default)
        {
            var response = await client.ExecuteGetAsync<T>(request, cancellationToken);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            if (typeof(ExceptionModel) == typeof(ApiErrorModel))
            {
                throw new ApiException(response);
            }

            throw new RestException<ExceptionModel>(response);
        }

        public static async Task<T> DeleteAsyncSafe<T,ExceptionModel>(this RestClient client, RestRequest request, CancellationToken cancellationToken = default)
        {
            var response = await client.ExecuteAsync<T>(request, Method.Delete, cancellationToken).ConfigureAwait(false);

            var exception = GetException(response);
            if (exception != null) throw exception;


            if (response.IsSuccessful)
            {
                return response.Data;
            }

            if (typeof(ExceptionModel) == typeof(ApiErrorModel))
            {
                throw new ApiException(response);
            }

            throw new RestException<ExceptionModel>(response);
        }

        private static Exception GetException<ExceptionModel>(RestResponse<ExceptionModel> response)
        {
            switch (response.ResponseStatus)
            {
                case ResponseStatus.Aborted:
                    return new HttpRequestException("Request aborted", response.ErrorException);
                case ResponseStatus.Error:
                    return response.ErrorException;
                case ResponseStatus.None:
                case ResponseStatus.Completed:
                    return null;
                default:
                    throw response.ErrorException ?? new ArgumentOutOfRangeException(nameof(ResponseStatus));
            }
        }
        #endregion
    }
}
