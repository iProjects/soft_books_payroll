using System;
using System.Data;
using System.Text.RegularExpressions;

namespace winSBPayroll.Utilities
{
    public class CalculationResult
    {
        public bool IsSuccess { get; private set; }
        public double Value { get; private set; }
        public string ErrorMessage { get; private set; }

        private CalculationResult(bool isSuccess, double value, string errorMessage)
        {
            IsSuccess = isSuccess;
            Value = value;
            ErrorMessage = errorMessage;
        }

        public static CalculationResult Success(double value)
        {
            return new CalculationResult(true, value, string.Empty);
        }

        public static CalculationResult Failure(string errorMessage)
        {
            return new CalculationResult(false, 0, errorMessage);
        }
    }

    public static class CalculatorEngine
    {
        /// <summary>
        /// Evaluates a raw string expression safely and returns a structured result.
        /// Compatible with .NET Framework 4.0 / C# 4.0.
        /// </summary>
        public static CalculationResult Evaluate(string expression)
        {
            // 1. Input Guardrails
            if (string.IsNullOrEmpty(expression) || expression.Trim().Length == 0)
            {
                return CalculationResult.Failure("Please enter an expression.");
            }

            // 2. Input Sanitization
            string sanitized = Regex.Replace(expression, @"[^0-9\+\-\*\/\(\)\.\s]", "");
            if (sanitized.Trim().Length == 0)
            {
                return CalculationResult.Failure("Expression contains no valid numbers or operators.");
            }

            // 3. Execution Block
            try
            {
                using (DataTable table = new DataTable())
                {
                    object rawResult = table.Compute(sanitized, string.Empty);

                    if (rawResult == DBNull.Value)
                    {
                        return CalculationResult.Failure("Unable to compute expression.");
                    }

                    double numericResult = Convert.ToDouble(rawResult);

                    if (double.IsInfinity(numericResult))
                    {
                        return CalculationResult.Failure("Division by zero encountered.");
                    }

                    if (double.IsNaN(numericResult))
                    {
                        return CalculationResult.Failure("Invalid mathematical result (NaN).");
                    }

                    return CalculationResult.Success(numericResult);
                }
            }
            catch (EvaluateException)
            {
                return CalculationResult.Failure("Syntax error in expression (check operators or parentheses).");
            }
            catch (Exception ex)
            {
                return CalculationResult.Failure(string.Format("Error: {0}", ex.Message));
            }
        }
    }
}
