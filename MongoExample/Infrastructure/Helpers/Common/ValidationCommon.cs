namespace MongoExample.Infrastructure.Helpers.Common
{
    public static class ValidationCommon
    {
        public static bool IsNotNullNorEmpty<T>(this IEnumerable<T> array)
        {
            return array is not null && array.Count() != 0;
        }

        ///// <summary>
        ///// Determines whether [is date time dmy type valid].
        ///// </summary>
        ///// <param name="date">The date.</param>
        ///// <returns><br/></returns>
        //public static AppResponseResult<DateTime> IsDateOfBirthValid(this string date)
        //{
        //    var result = new AppResponseResult<DateTime>();

        //    if (!date.TryParseShortDate(out DateTime dateTime))
        //    {
        //        return result.BuildError(ERR_MSG_DATE_TIME_INVALID);
        //    }

        //    if (DateTime.Now.Subtract(dateTime).TotalDays < 0)
        //    {
        //        return result.BuildError(ERR_MSG_DATE_SELECT_NOT_EARLIER_TODAY);
        //    }

        //    return result.BuildSuccess(dateTime);
        //}

        ///// <summary>
        ///// Determines whether [is date time dmy type valid].
        ///// </summary>
        ///// <param name="date">date</param>
        ///// <returns></returns>
        //public static AppResponseResult<DateTime> IsDateTimeValid(this string date)
        //{
        //    var result = new AppResponseResult<DateTime>();

        //    if (!date.TryParseShortDate(out DateTime dateTime))
        //    {
        //        return result.BuildError(ERR_MSG_DATE_TIME_INVALID);
        //    }

        //    return result.BuildSuccess(dateTime);
        //}

        //public static bool TryParseShortDate(this string dateString, out DateTime date)
        //{
        //    return DateTime.TryParseExact(dateString, FORMAT_SHORT_DATE,
        //                   CultureInfo.InvariantCulture,
        //                   DateTimeStyles.None,
        //                   out date);
        //}

        //public static bool TryParseShortTime(this string time, out TimeSpan timeSpan)
        //{
        //    return TimeSpan.TryParseExact(time, FORMAT_SHORT_TIME,
        //                   CultureInfo.CurrentCulture,
        //                   TimeSpanStyles.None,
        //                   out timeSpan);
        //}

        //public static AppResponseResult<string> IsUserPermissionValid(this string permission)
        //{
        //    var result = new AppResponseResult<string>();
        //    if (string.IsNullOrEmpty(permission))
        //    {
        //        return result.BuildError(ERR_MSG_USER_PERRMISSION_BE_EMPTY);
        //    }

        //    if (permission == CUSTOMER_PERMISSION || permission == USER_PERMISSION || permission == HOST_PERMISSION && permission != SUPER_ADMIN_PERMISSION)
        //    {
        //        return result.BuildSuccess(permission, INF_MSG_SUCCESSFULLY);
        //    }

        //    return result.BuildError(permission, INF_MSG_USER_PERRMISSION_INVALID);
        //}

        //public static bool IsObjectStatusValid(this int status)
        //{
        //    return status == ObjectStatusConstant.ACTIVE ||
        //            status == ObjectStatusConstant.DENIED ||
        //            status == ObjectStatusConstant.DRAFT ||
        //            status == ObjectStatusConstant.INACTIVE ||
        //            status == ObjectStatusConstant.PENDING;
        //}
    }
}
