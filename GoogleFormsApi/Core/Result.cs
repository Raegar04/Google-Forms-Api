using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Helpers;

/// <summary>
/// Object which represents result of some operation(mostly for CRUDs).
/// </summary>
/// <typeparam name="T">Type of the result data</typeparam>
public class Result<T>
{
    /// <summary>
    /// Shows if operation is successful
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// Error message
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Data arrives from 'GET' operations
    /// </summary>
    public T Data { get; }

    /// <summary>
    /// Result in case when operation of getting data is successful
    /// </summary>
    /// <param name="isSuccessful">If operation is successful(true)</param>
    /// <param name="data">Retrieved data</param>
    public Result(bool success, T data)
    {
        Success = success;
        Data = data;
    }

    /// <summary>
    /// Result in case when operation isn't successful
    /// </summary>
    /// <param name="isSuccessful">Operation is not successful(false)</param>
    /// <param name="message">Error message</param>
    public Result(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    /// <summary>
    /// Result in case when operation wthout returning type is successful
    /// </summary>
    /// <param name="isSuccessful">If operation is successful(true)</param>
    public Result(bool success)
    {
        Success = success;
    }
}