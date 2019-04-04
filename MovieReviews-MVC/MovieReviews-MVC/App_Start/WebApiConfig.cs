using System.Web.Http;

namespace MovieReviews_MVC
{

  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
        name: "MovieApi",
        routeTemplate: "api/{controller}/{movieId}",
        defaults: new { movieId = RouteParameter.Optional }
      );

      config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new { id = RouteParameter.Optional }
          );
    }
  }

}