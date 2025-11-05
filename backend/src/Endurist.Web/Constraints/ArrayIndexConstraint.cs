namespace Endurist.Web.Constraints;

internal class ArrayIndexConstraint : IRouteConstraint
{
    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (values.TryGetValue(routeKey, out object paramValue) && paramValue != null)
        {
            var stringValue = paramValue.ToString();
            return int.TryParse(stringValue, out var intValue) && intValue > 0;
        }

        return false;
    }
}
