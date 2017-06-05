using System;
using System.Collections.Generic;
using System.Linq;
using MoneyKeeper.Core.Entities;
using MoneyKeeper.DataAccess;

namespace MoneyKeeper.DataMining.DataAnalytics
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("івадлоі вало віадо");
            using (var context = new MoneyKeeperContext())
            {
                List<FinancialOperation> allExpenses = context
                    .FinancialOperations
                    .Where(x => x.Type == FinOperationType.Expense)
                    .OrderBy(x => x.Date)
                    .ToList();

                Console.WriteLine(string.Join(", ", allExpenses.Select(x => x.Value)));

                FinOperationsByDaysDto expensesByDays = FinOperationsByDaysDto.ExtractFromDataSet(allExpenses);

                Console.WriteLine(Resources.MondayPrediction, GetAverage(expensesByDays.OnMonday.Select(x => x.Value)));
                Console.WriteLine(Resources.TuesdayPrediction, GetAverage(expensesByDays.OnTuesday.Select(x => x.Value)));
                Console.WriteLine(Resources.WednesdayPrediction, GetAverage(expensesByDays.OnWednesday.Select(x => x.Value)));
                Console.WriteLine(Resources.ThursdayPrediction, GetAverage(expensesByDays.OnThursday.Select(x => x.Value)));
                Console.WriteLine(Resources.FridayPrediction, GetAverage(expensesByDays.OnFriday.Select(x => x.Value)));

                Console.WriteLine(Resources.SaturdayPrediction, GetAverage(expensesByDays.OnSaturday.Select(x => x.Value)));
                Console.WriteLine(Resources.SundayPrediction, GetAverage(expensesByDays.OnSunday.Select(x => x.Value)));
            }
        }

        private static double GetAverage(IEnumerable<double> dataSet)
        {
            List<double> normalized = NormalizeDataSet(dataSet.OrderBy(x => x).ToList());

            return normalized.Sum() / normalized.Count;
        }

        private static List<double> NormalizeDataSet(List<double> dataSet)
        {
            if (dataSet.Count == 0)
            {
                return dataSet;
            }

            double median = FindMedian(dataSet);
            double firstQuartile = FindMedian(dataSet.Where(x => x <= median));
            double thirdQuartile = FindMedian(dataSet.Where(x => x >= median));

            double iqr = thirdQuartile - firstQuartile;

            // range for minor outliers
            //double innerFenceLeftCorner = firstQuartile - (iqr * 1.5);
            //double innerFenceRightCorner = thirdQuartile + (iqr * 1.5);

            // range for major outliers
            double outterFenceLeftCorner = firstQuartile - (iqr * 3);
            double outterFenceRightCorner = firstQuartile + (iqr * 3);

            return dataSet
                .Where(x => x >= outterFenceLeftCorner && x <= outterFenceRightCorner)
                .ToList();
        }

        private static double FindMedian(IEnumerable<double> dataSet)
        {
            List<double> dataSetList = dataSet as List<double> ?? new List<double>(dataSet);

            if ((dataSetList.Count & 1) == 0)
            {
                int medianPosition = dataSetList.Count / 2;
                double leftFromMedian = dataSetList[medianPosition];
                double rightFromMedian = dataSetList[medianPosition - 1];

                return (leftFromMedian + rightFromMedian) / 2;
            }
            else
            {
                return dataSetList[dataSetList.Count / 2];
            }
        }
    }

    internal class FinOperationsByDaysDto
    {
        public List<FinancialOperation> OnMonday { get; set; }
        public List<FinancialOperation> OnTuesday { get; set; }
        public List<FinancialOperation> OnWednesday { get; set; }
        public List<FinancialOperation> OnThursday { get; set; }
        public List<FinancialOperation> OnFriday { get; set; }
        public List<FinancialOperation> OnSaturday { get; set; }
        public List<FinancialOperation> OnSunday { get; set; }

        public static FinOperationsByDaysDto ExtractFromDataSet(IEnumerable<FinancialOperation> allExpenses)
        {
            return new FinOperationsByDaysDto
            {
                OnMonday = allExpenses.Where(x => x.Date.DayOfWeek == DayOfWeek.Monday).ToList(),
                OnTuesday = allExpenses.Where(x => x.Date.DayOfWeek == DayOfWeek.Tuesday).ToList(),
                OnWednesday = allExpenses.Where(x => x.Date.DayOfWeek == DayOfWeek.Wednesday).ToList(),
                OnThursday = allExpenses.Where(x => x.Date.DayOfWeek == DayOfWeek.Thursday).ToList(),
                OnFriday = allExpenses.Where(x => x.Date.DayOfWeek == DayOfWeek.Friday).ToList(),
                OnSaturday = allExpenses.Where(x => x.Date.DayOfWeek == DayOfWeek.Saturday).ToList(),
                OnSunday = allExpenses.Where(x => x.Date.DayOfWeek == DayOfWeek.Sunday).ToList()
            };
        }
    }
}