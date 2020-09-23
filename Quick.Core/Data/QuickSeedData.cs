using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quick.Core.Data
{
    public static class QuickSeedData
    {
        public static void Init(QuickContext quickContext)
        {
            if (!!!quickContext.HelloModels.Any())
            {
                for(var i = 0; i < 100; i++)
                {
                    quickContext.HelloModels.Add(new Model.HelloModel { Text = $"Hello World {(i + 1)}" });
                    quickContext.SaveChanges();
                }
            }
        }
    }
}
