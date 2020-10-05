using ModelClass.model.holiday;
using System;

namespace REPORT.service.holiday
{
    public interface IHoliday
    {
        dynamic getListHoliday(int v_YEAR); // YYYY
        dynamic insertHoliday(int v_YEAR); // YYYY
        dynamic insertByDate(int v_DATE); // YYYYMMDD
        dynamic deleteDate(int v_DATE); // YYYYMMDD
    }
}
