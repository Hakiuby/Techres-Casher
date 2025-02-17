﻿using System;
using System.Threading.Tasks;
using TechresStandaloneSale.Interface;

namespace TechresStandaloneSale.Interfaces
{
    public static class TaskUtilities
    {
#pragma warning disable RECS0165 // Asynchronous methods should return a Task instead of void
        public static async void FireAndForgetSafeAsync(this Task task, IErrorLogger handler = null)
#pragma warning restore RECS0165 // Asynchronous methods should return a Task instead of void
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.LogError(ex, "");
            }
        }
    }
}
