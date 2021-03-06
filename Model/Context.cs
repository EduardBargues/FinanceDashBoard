﻿using CandleTimeSeriesAnalysis;
using CommonUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Model
{
    public class Context
    {
        private readonly string databaseConnectionString;
        private readonly string userName;
        private readonly string password;
        private const string DatabaseConfigurationFile = @"C:\databaseConnection.csv";

        private readonly Dictionary<DateTime, List<Trade>> tradesByDay = new Dictionary<DateTime, List<Trade>>();

        private CandleTimeSeries historyCandleTimeSeries;
        public CandleTimeSeries HistoryCandleTimeSeries => historyCandleTimeSeries
            ?? (historyCandleTimeSeries = GetHistoryCandleTimeSeriesFromDatabase());

        private Context(string dbConString, string uName, string psw)
        {
            databaseConnectionString = dbConString;
            userName = uName;
            password = psw;
        }
        private static Context instance;
        public static Context Instance
        {
            get
            {
                if (instance == null)
                {
                    string[] lines = File.ReadAllLines(DatabaseConfigurationFile);
                    instance = new Context(lines[0], lines[1], lines[2]);
                }

                return instance;
            }
        }
        public IEnumerable<Trade> GetDailyTrades(DateTime date)
        {
            DateTime day = date.Date;
            if (!tradesByDay.ContainsKey(day))
            {
                List<Trade> trades = GetDailyTimeSeriesFromDatabase(day);
                tradesByDay.Add(day, trades);
            }
            return tradesByDay[day];
        }
        public IEnumerable<Trade> GetTradesBetween(DateTime date1, DateTime date2) => date1.GetDaysTo(date2)
                .Distinct()
                .SelectMany(GetDailyTrades);


        private CandleTimeSeries GetHistoryCandleTimeSeriesFromDatabase()
        {
            CandleTimeSeries result;

            using (DatabaseConnector connector = new DatabaseConnector())
            {
                connector.Connect(databaseConnectionString, userName, password);
                IEnumerable<(string, object)[]> candlesInfo = connector.ExecuteReaderCommand("select * from BitCoinDailyCandles");
                result = candlesInfo
                    .Select(GetCandleFromDatabaseInfo)
                    .ToCandleTimeSeries();
                connector.Disconnect();
            }

            return result;
        }
        private Candle GetCandleFromDatabaseInfo(IEnumerable<(string, object)> info)
        {
            Dictionary<string, object> propsByName = info
                .ToDictionary(i => i.Item1, i => i.Item2);
            Candle candle = new Candle
            {
                Start = (DateTime)propsByName[nameof(Candle.Start)],
                Duration = TimeSpan.FromTicks((long)propsByName[nameof(Candle.Duration)]),
                Open = Convert.ToDouble(propsByName[nameof(Candle.Open)]),
                Close = Convert.ToDouble(propsByName[nameof(Candle.Close)]),
                Min = Convert.ToDouble(propsByName[nameof(Candle.Min)]),
                Max = Convert.ToDouble(propsByName[nameof(Candle.Max)]),
                BuyVolume = Convert.ToDouble(propsByName[nameof(Candle.BuyVolume)]),
                SellVolume = Convert.ToDouble(propsByName[nameof(Candle.SellVolume)]),
            };
            return candle;
        }

        private List<Trade> GetDailyTimeSeriesFromDatabase(DateTime day)
        {
            List<Trade> trades;
            using (DatabaseConnector connector = new DatabaseConnector())
            {
                connector.Connect(databaseConnectionString, userName, password);
                IEnumerable<(string, object)[]> tradeInfos = connector.ExecuteReaderCommand($"select * from BitCoinTrades where " +
                                                                                            $"year(Instant) = {day.Year} and " +
                                                                                            $"month(Instant) = {day.Month} and " +
                                                                                            $"day(Instant) = {day.Day}");
                trades = tradeInfos
                    .Select(GetTradeFromDatabaseInfo)
                    .ToList();

                connector.Disconnect();
            }

            return trades;
        }
        private Trade GetTradeFromDatabaseInfo((string, object)[] info)
        {
            Dictionary<string, object> propertiesByName = info
                .ToDictionary(i => i.Item1, i => i.Item2);

            Trade trade = new Trade
            {
                Instant = (DateTime)propertiesByName[nameof(Trade.Instant)],
                Volume = Convert.ToDouble(propertiesByName[nameof(Trade.Volume)]),
                Price = Convert.ToDouble(propertiesByName[nameof(Trade.Price)]),
            };
            string type = (string)propertiesByName[nameof(Trade.Type)];
            trade.Type = string.IsNullOrEmpty(type) ||
                         type == "SELL"
                ? TradeType.Sell
                : TradeType.Buy;

            return trade;
        }
    }
}
