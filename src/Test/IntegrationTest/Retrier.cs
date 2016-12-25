using System;
using System.Net;

namespace IntegrationTest
{
    public class Retrier<TResult> where TResult : new()
    {
        public TResult RetryWithDelay(Func<TResult> func, int maxRetries = 1, int delaynMin = 3000)
        {
            var returnValue = default(TResult);

            var numberOfRetries = 0;
            var succeded = false;

            while (numberOfRetries < maxRetries)
            {
                try
                {
                    returnValue = func();
                    succeded = true;
                    break;
                }
                catch (Exception)
                {
                    System.Threading.Thread.Sleep(delaynMin);
                    delaynMin = delaynMin * 2;
                }
                finally
                {
                    numberOfRetries++;
                }
            }

            if (!succeded)
            {
                returnValue = SetErrorResponse();
            }

            return returnValue;
        }

        private TResult SetErrorResponse()
        {
            var result = new TResult();

            var typeofTresult = result.GetType();

            var errorProperty = typeofTresult.GetProperty("Status");

            if (errorProperty != null)
            {
                errorProperty.SetValue(result, HttpStatusCode.NotFound);
            }

            return result;
        }
    }
}