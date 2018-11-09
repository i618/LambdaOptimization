using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LambdaFuncOptimization
{
    internal class LambdaFuncOptimizer : ExpressionVisitor
    {
        private readonly List<ParameterExpression> vars = new List<ParameterExpression>();
        private readonly List<Expression> exprs = new List<Expression>();

        public Expression<T> Run<T>(Expression<T> expr) where T : Delegate
        {
            Expression<T> changedExpr = (Expression<T>)Visit(expr);

            exprs.Add(changedExpr.Body);

            return Expression.Lambda<T>(
                Expression.Block(vars, exprs), changedExpr.Parameters);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.IsDefined(typeof(LambdaFuncOptimizationAttribute)) && node.Method.ReturnType != typeof(void))
            {
                string varName = node.ToString().Replace("(", "").Replace(")", "").ToLower();
                ParameterExpression funcVar = vars.Find((x) => varName == x.Name);

                if (funcVar == null)
                {
                    funcVar = Expression.Variable(node.Method.ReturnType, varName);
                    vars.Add(funcVar);
                    BinaryExpression assignExpr = Expression.Assign(funcVar, node);
                    exprs.Add(assignExpr);
                }

                return funcVar;
            }

            return base.VisitMethodCall(node);
        }
    }
}
