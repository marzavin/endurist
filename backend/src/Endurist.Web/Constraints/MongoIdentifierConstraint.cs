using System.Text.RegularExpressions;

namespace Endurist.Web.Constraints;

internal partial class MongoIdentifierConstraint : IRouteConstraint
{
    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        var regex = MongoIdRegex();

        if (values.TryGetValue(routeKey, out object paramValue) && paramValue != null)
        {
            var stringValue = paramValue.ToString();
            return regex.IsMatch(stringValue);
        }

        return false;
    }

    [GeneratedRegex("^[a-f\\d]{24}$", RegexOptions.IgnoreCase)]
    private static partial Regex MongoIdRegex();
}
