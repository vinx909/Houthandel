using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Containerverhuur
{
    class MoveTroughDays
    {
        private static readonly int[,] daysPerMonth = { { 1, 31 }, { 2, 28 }, { 3, 31 }, { 4, 30 }, { 5, 31 }, { 6, 30 }, { 7, 31 }, { 8, 31 }, { 9, 30 }, { 10, 31 }, { 11, 30 }, { 12, 31 } };
        private const int daysPerMonthMonthIndex = 0;
        private const int daysPerMonthAmountOfDaysIndex = 1;

        private static int highestMonth;

        private const int arrayLength = 3;
        private const int dayIndex = 0;
        private const int monthIndex = 1;
        private const int yearIndex = 2;

        private const string dateDoesNotExistExceptionStringPartOne = "the given month doesn't exist: day:";
        private const string dateDoesNotExistExceptionStringPartTwo = " month: ";
        private const string monthNotFoundExceptionString = "the month is not found in days per months and thus the program can't continue";

        private static void SetuptHighestMonth()
        {
            for (int i = 0; i < daysPerMonth.GetLength(0); i++)
            {
                if (highestMonth < daysPerMonth[i, daysPerMonthMonthIndex])
                {
                    highestMonth = daysPerMonth[i, daysPerMonthMonthIndex];
                }
            }
        }
        private static void CheckIfDateExist(int day, int month)
        {
            SetuptHighestMonth();
            if (month > 0&&month< highestMonth &&day>0)
            {
                for (int i = 0; i < daysPerMonth.GetLength(0); i++)
                {
                    if (daysPerMonth[i, daysPerMonthMonthIndex] == month)
                    {
                        if (day <= daysPerMonth[i, daysPerMonthAmountOfDaysIndex])
                        {
                            return;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            throw new Exception(dateDoesNotExistExceptionStringPartOne + day + dateDoesNotExistExceptionStringPartTwo + month);
        }
        public static void EachDayCheck(int startDay, int startMonth, int startYear, int stopDay, int stopMonth, int stopYear, Action<int[]> toDoEachDay)
        {
            SetuptHighestMonth();
            CheckIfDateExist(stopDay, stopMonth);
            int[] date = new int[arrayLength];
            date[dayIndex] = startDay;
            date[monthIndex] = startMonth;
            date[yearIndex] = startYear;
            while (date[yearIndex] < stopYear || (date[yearIndex] == stopYear && date[monthIndex] < stopMonth) || (date[yearIndex] == stopYear && date[monthIndex] == stopMonth && date[dayIndex] <= stopDay))
            {
                toDoEachDay.Invoke(date);
                bool found = false;
                for (int i = 0; i < daysPerMonth.GetLength(0); i++)
                {
                    if (date[monthIndex] == daysPerMonth[i, daysPerMonthMonthIndex])
                    {
                        found = true;
                        if (date[dayIndex] < daysPerMonth[i, daysPerMonthAmountOfDaysIndex])
                        {
                            date[dayIndex]++;
                        }
                        else
                        {
                            date[dayIndex] = 1;
                            if (date[monthIndex] < highestMonth)
                            {
                                date[monthIndex]++;
                            }
                            else
                            {
                                date[monthIndex] = 1;
                                date[yearIndex]++;
                            }
                        }
                        break;
                    }
                }
                if (found == false)
                {
                    throw new Exception(monthNotFoundExceptionString);
                }
            }
        }
        public static void EachDayCheck(int[] startDate, int[]stopDate, Action<int[]> toDoEachDay)
        {
            EachDayCheck(startDate[dayIndex], startDate[monthIndex], startDate[yearIndex], stopDate[dayIndex], stopDate[monthIndex], stopDate[yearIndex], toDoEachDay);
        }
        public static int AmountOfDays(int startDay, int startMonth, int startYear, int stopDay, int stopMonth, int stopYear)
        {
            int amountOfDays = 0;
            Action<int[]> increaseAmountOfDays = (int[] irrelavent) =>
            {
                amountOfDays++;
            };
            EachDayCheck(startDay, startMonth, startYear, stopDay, stopMonth, stopYear, increaseAmountOfDays);
            return amountOfDays;
        }
        public static int GetDateArrayLength()
        {
            return arrayLength;
        }
        public static int GetDateArrayDayIndex()
        {
            return dayIndex;
        }
        public static int GetDateArrayMonthIndex()
        {
            return monthIndex;
        }
        public static int GetDateArrayYearIndex()
        {
            return yearIndex;
        }
    }
}
