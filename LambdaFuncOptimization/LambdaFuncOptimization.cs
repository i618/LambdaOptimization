using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LambdaFuncOptimization
{
    public static class Optimization
    {
        public static T Run<T>(Expression<T> expr) where T : Delegate
        {
            LambdaFuncOptimizer opt = new LambdaFuncOptimizer();
            return opt.Run<T>(expr).Compile();
        }
    }
}
