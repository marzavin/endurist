using MongoDB.Bson;

namespace Endurist.Web.Constraints;

internal class MongoIdentifierConstraint : IRouteConstraint
{
    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (values.TryGetValue(routeKey, out object paramValue) && paramValue != null)
        {
            var stringValue = paramValue.ToString();
            return ObjectId.TryParse(stringValue, out _);
        }

        return false;
    }
}
