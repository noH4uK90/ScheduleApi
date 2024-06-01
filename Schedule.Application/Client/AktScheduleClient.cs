using HtmlAgilityPack;
using Schedule.Application.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Client;

public class AktScheduleClient : IAktScheduleClient
{
    private const string BaseURL = "https://arcotel.ru/studentam/raspisanie-i-grafiki/raspisanie-zanyatiy-studentov-ochnoy-i-vecherney-form-obucheniya";

    public async Task<ICollection<ParsedScheduleItem>> GetSchedule(int groupId)
    {
        var url = BaseURL + $"?group={groupId}";
        var html = await FetchHtml(url);

        if (html is null)
        {
            throw new Exception("Failed to fetch HTML.");
        }

        return ParseSchedule(html);
    }

    public async Task<ICollection<ParsedScheduleItem>> GetSchedule(int groupId, DateOnly date)
    {
        var url = BaseURL + $"?group={groupId}&date={date.ToString("yyyy-MM-dd")}";
        var html = await FetchHtml(url);

        if (html is null)
        {
            throw new Exception("Failed to fetch HTML.");
        }

        return ParseSchedule(html);
    }

    public async Task<ICollection<Group>> GetGroups()
    {
        var html = await FetchHtml(BaseURL);
        if (html is null)
        {
            throw new Exception("Failed to fetch HTML.");
        }

        return ParseGroups(html);
    }
    
    private async Task<string?> FetchHtml(string url)
    {
        using var client = new HttpClient();
        try
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (HttpRequestException e)
        {
            throw new Exception($"Request error: {e.Message}");
        }
    }
    
    private ICollection<ParsedScheduleItem> ParseSchedule(string html)
    {
        var schedule = new List<ParsedScheduleItem>();
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var rows = doc.DocumentNode.SelectNodes("//div[contains(@class, 'vt244b')]/div[contains(@class, 'vt244')]");

        if (rows is null) return schedule;

        foreach (var row in rows)
        {
            var pairNumberDiv = row.SelectSingleNode("div[contains(@class, 'vt239')]");
            if (pairNumberDiv is null)
                continue;

            var pairNumber = pairNumberDiv.InnerText.Trim();
            int.TryParse(pairNumber, out var number);

            var dayDivs = row.SelectNodes("div[contains(@class, 'vt239') and contains(@class, 'rasp-day')]");
            for (var i = 0; i < dayDivs.Count; i++)
            {
                var classDetailDivs = dayDivs[i].SelectNodes("div[contains(@class, 'vt258')]");
                if (classDetailDivs is null)
                    continue;
                
                var mainClassDiv = classDetailDivs[0];
                var subClassDiv = classDetailDivs.Count > 1 ? classDetailDivs[1] : null;

                var mainItem = CreateParsedScheduleItem(mainClassDiv, number, i + 1, subClassDiv);
                
                schedule.Add(mainItem);
            }
        }

        return schedule;
    }
    
    private ParsedScheduleItem CreateParsedScheduleItem(HtmlNode mainClassDiv, int number, int dayId, HtmlNode? subClassDiv)
    {
        var subItem = subClassDiv != null
            ? new ParsedScheduleSubItem
            {
                Number = number,
                DayId = dayId,
                Discipline = subClassDiv.SelectSingleNode("div[contains(@class, 'vt240')]")?.InnerText.Trim() ?? "",
                Teacher = subClassDiv.SelectSingleNode("div[contains(@class, 'vt241')]").SelectSingleNode("span[contains(@class, 'teacher')]")?.InnerText.Trim() ?? "",
                Classroom = subClassDiv.SelectSingleNode("div[contains(@class, 'vt242')]")?.InnerText.Trim() ?? "",
                Type = subClassDiv.SelectSingleNode("div[contains(@class, 'vt243')]")?.InnerText.Trim() ?? ""
            }
            : null;

        return new ParsedScheduleItem
        {
            Number = number,
            DayId = dayId,
            Discipline = mainClassDiv.SelectSingleNode("div[contains(@class, 'vt240')]")?.InnerText.Trim() ?? "",
            Teacher = mainClassDiv.SelectSingleNode("div[contains(@class, 'vt241')]").SelectSingleNode("span[contains(@class, 'teacher')]")?.InnerText.Trim() ?? "",
            Classroom = mainClassDiv.SelectSingleNode("div[contains(@class, 'vt242')]")?.InnerText.Trim() ?? "",
            Type = mainClassDiv.SelectSingleNode("div[contains(@class, 'vt243')]")?.InnerText.Trim() ?? "",
            SubItem = subItem
        };
    }
    
    private ICollection<Group> ParseGroups(string html)
    {
        var groups = new List<Group>();
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var groupNodes = doc.DocumentNode.SelectNodes("//div[@class='vt255']/a[contains(@class, 'vt256') and not(contains(@class, 'vt256_op'))]");

        if (groupNodes is null) return groups;

        var id = 0;

        groups.AddRange(from groupNode in groupNodes
            let stringId = groupNode.GetAttributeValue("data-i", "")
            let name = groupNode.GetAttributeValue("data-nm", "")
            where int.TryParse(stringId, out id) && !string.IsNullOrWhiteSpace(name)
            select new Group { GroupId = id, Name = name });

        return groups;
    }
}