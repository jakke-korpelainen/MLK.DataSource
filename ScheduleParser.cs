using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MLK.DataSource
{
    public class ScheduleParser
    {
        private static ScheduleParser _scheduleParser;

        public static ScheduleParser Instance => _scheduleParser ?? (_scheduleParser = new ScheduleParser());

        public List<ScheduleItem> GetSchedule(string url, ScheduleType scheduleType, DateTime date)
        {
            

            var web = new HtmlWeb();
            var doc = web.Load(string.Format(url, (int)scheduleType, date.ToString("yyyy-MM-dd")));

            var table = doc.DocumentNode.SelectSingleNode("//html/body/table");

            var rows = table.SelectNodes("tr").Where(row =>
            {
                return row.ChildNodes.Any(col => col.ChildNodes.Any(el => el.InnerText.StartsWith("K") && el.InnerText.Length == 2));
            }).ToList();

            var availableItems = new List<HtmlNode>();
            foreach (var matching in rows.Select(tr => tr.ChildNodes.Where(x => x.ChildNodes.Any(el => el.InnerText.StartsWith("K") && el.InnerText.Length == 2))))
            {
                availableItems.AddRange(matching);
            }

            var items = new List<ScheduleItem>();

            foreach (var available in availableItems)
            {
                var data = available.ChildNodes.Where(x => x.NodeType == HtmlNodeType.Text || x.Name == "span").ToList();
                var item = new ScheduleItem
                {
                    Field = data.ElementAt(0).InnerText,
                    ScheduleType = scheduleType,
                    Time = data.ElementAt(1).InnerText,
                    ReservationUrl = data.ElementAt(2).ChildNodes.ElementAt(0).Attributes["href"]?.Value
                };
                items.Add(item);
            }

            return items;
        }
    }
}
