using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DateCounter : MonoBehaviour
{
	public TextMeshProUGUI todayText;
	public TextMeshProUGUI currentText;
	public TextMeshProUGUI ddayText;
	public TextMeshProUGUI percentText;
	public Image fillImage;

	public string projectName;

	DateTime today;
	public string startString;
	public string ddayString;
	public string presentationString;
	DateTime sday;
	DateTime dday;
	DateTime pday;
	
    string dateFormat = "yyyy년 M월 d일 (ddd)";

	double totalTick;

    List<int> holidays = new()
        {
            new DateTime(2024,6,6).DayOfYear,
        };

    private IEnumerator Start()
	{
		today = DateTime.Now;

		sday = DateTime.Parse(startString);
		dday = DateTime.Parse(ddayString);
		pday = DateTime.Parse(presentationString);
		sday = new DateTime(sday.Ticks, DateTimeKind.Local);
		dday = new DateTime(dday.Ticks, DateTimeKind.Local);
		pday = new DateTime(pday.Ticks, DateTimeKind.Local);
		totalTick = dday.Ticks - sday.Ticks;

		StringBuilder sb = new();
		sb.AppendLine(today.ToString(dateFormat));
		
		int totalDate = 0, currentDate = 0;

		for (DateTime cdate = sday; cdate.DayOfYear <= dday.DayOfYear; cdate = cdate.AddDays(1))
		{
			if(cdate.DayOfWeek == DayOfWeek.Saturday || cdate.DayOfWeek == DayOfWeek.Sunday || holidays.Contains(cdate.DayOfYear))
			{
				continue;
			}
			if(cdate.DayOfYear <= today.DayOfYear)
			{
				currentDate++;
			}
			totalDate++;
		}

		sb.AppendLine($"{projectName} {currentDate}일차 ({currentDate}/{totalDate})");
		todayText.text = sb.ToString();
		sb.Clear();

		sb.AppendLine($"프로젝트 진행 : {sday.ToString(dateFormat)} ~ {dday.ToString(dateFormat)}");
		sb.AppendLine($"프로젝트 발표 : {pday.ToString(dateFormat)}");
		ddayText.text = sb.ToString();

		while (true)
		{
			sb.Clear();

			sb.Append("현재 시각 : ");
			sb.Append(DateTime.Now.ToString("T"));
			sb.AppendLine();
			sb.Append("종료까지 : ");

			TimeSpan span = dday - DateTime.Now;

			sb.Append($"{(int)span.TotalHours} 시간 {span.Minutes} 분 {span.Seconds} 초");

			currentText.text = sb.ToString();
			yield return new WaitForSeconds(1);
		}

	}

	private void Update()
	{
		double currentTick = DateTime.Now.Ticks - sday.Ticks;

		double currentAmount = currentTick / totalTick;

		fillImage.fillAmount = (float)currentAmount;

		percentText.text = currentAmount.ToString("p6");

	}

}

