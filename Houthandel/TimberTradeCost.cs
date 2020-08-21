using Containerverhuur;
using System;
using System.Collections.Generic;

namespace Houthandel
{
    internal class TimberTradeCost
    {
        private const int percentageTotal = 100;
        private const string exceptionWoodClassNotFound = "the given wood class was not found";

        private const string woodClassOneName = "class one";
        private const int woodClassOneCostPerCubicMeter = 300;
        private const string woodClassTwoName = "class two";
        private const int woodClassTwoCostPerCubicMeter = 500;
        private const string woodClassThreeName = "class three";
        private const int woodClassThreeCostPerCubicMeter = 450;

        private const int maxCostForScrapeCost = 200;
        private const double costPerSquareMeterScraped = 0.6;

        private const int discoundPerDaysOneMinimumAmountOfDays = 14;
        private const double discoundPerDaysOnePercentageDiscount = 1;
        private const int discoundPerDaysTwoMinimumAmountOfDays = 21;
        private const double discoundPerDaysTwoPercentageDiscount = 2;
        private const int discoundPerDaysThreeMinimumAmountOfDays = 28;
        private const double discoundPerDaysThreePercentageDiscount = 2.5;

        private static List<WoodClass> woodClasses;
        private static List<DiscountForDays> listDiscountForDays;

        internal static string[] GetWoodOrderClasses()
        {
            SetupWoodClasses();
            string[] toReturn = new string[woodClasses.Count];
            for(int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = woodClasses[i].GetName();
            }
            return toReturn;
        }
        internal static double GetCost(string className, double volume, double scrapeSurfase, int orderDay, int orderMonth, int orderYear, int deliveryDay, int deliveryMonth, int deliveryYear)
        {
            SetupWoodClasses();
            StupListDiscountForDays();
            double cost=0;
            bool woodClassFound = false;
            foreach(WoodClass woodClass in woodClasses)
            {
                if (woodClass.IsSameName(className))
                {
                    cost = woodClass.GetCostOfVolume(volume);
                    woodClassFound = true;
                    break;
                }
            }
            if (woodClassFound == false)
            {
                throw new Exception(exceptionWoodClassNotFound);
            }
            if (cost < maxCostForScrapeCost)
            {
                cost += scrapeSurfase * costPerSquareMeterScraped;
            }
            double percentageCostReduction = 0;
            int daysBetweenOrderDateAndDeliverDate = MoveTroughDays.AmountOfDays(orderDay, orderMonth, orderYear, deliveryDay, deliveryMonth, deliveryYear);
            foreach (DiscountForDays discountForDays in listDiscountForDays)
            {
                if (discountForDays.IsPossible(daysBetweenOrderDateAndDeliverDate))
                {
                    double possibleDiscount = discountForDays.getPercentageDiscount();
                    if (percentageCostReduction < possibleDiscount)
                    {
                        percentageCostReduction = possibleDiscount;
                    }
                }
            }
            cost *= 1.0 / percentageTotal * (percentageTotal - percentageCostReduction);
            return cost;
        }

        private static void SetupWoodClasses()
        {
            if(woodClasses == null)
            {
                woodClasses = new List<WoodClass>();
                woodClasses.Add(new WoodClass(woodClassOneName, woodClassOneCostPerCubicMeter));
                woodClasses.Add(new WoodClass(woodClassTwoName, woodClassTwoCostPerCubicMeter));
                woodClasses.Add(new WoodClass(woodClassThreeName, woodClassThreeCostPerCubicMeter));
            }
        }
        private static void StupListDiscountForDays()
        {
            if (listDiscountForDays == null)
            {
                listDiscountForDays = new List<DiscountForDays>();
                listDiscountForDays.Add(new DiscountForDays(discoundPerDaysOneMinimumAmountOfDays, discoundPerDaysOnePercentageDiscount));
                listDiscountForDays.Add(new DiscountForDays(discoundPerDaysTwoMinimumAmountOfDays, discoundPerDaysTwoPercentageDiscount));
                listDiscountForDays.Add(new DiscountForDays(discoundPerDaysThreeMinimumAmountOfDays, discoundPerDaysThreePercentageDiscount));
            }
        }

        private class WoodClass
        {
            private string name;
            private int pricePerCubicMeter;
            internal WoodClass(string name, int pricePerCubicMeter)
            {
                this.name = name;
                this.pricePerCubicMeter = pricePerCubicMeter;
            }
            internal string GetName()
            {
                return name;
            }
            internal bool IsSameName(string name)
            {
                return this.name.Equals(name);
            }
            internal double GetCostOfVolume(double volumeInCubicMeters)
            {
                return volumeInCubicMeters * pricePerCubicMeter;
            }
        }
        private class DiscountForDays
        {
            private int minimumAmountOfDays;
            private double percentageDiscount;
            internal DiscountForDays(int minimumAmountOfDays, double percentageDiscount)
            {
                this.minimumAmountOfDays = minimumAmountOfDays;
                this.percentageDiscount = percentageDiscount;
            }
            internal bool IsPossible(int amountOfDays)
            {
                return amountOfDays >= minimumAmountOfDays;
            }
            internal double getPercentageDiscount()
            {
                return percentageDiscount;
            }
        }
    }
}