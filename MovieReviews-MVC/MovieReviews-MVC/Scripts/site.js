var site = site || {};
site.baseUrl = site.baseUrl || "";

$(document).ready( (e) => {

  // locate each partial section.
  // if it has a URL set, load the contents into the area.

  $(".partials-content").each(function(index, item) {
    var url = site.baseUrl + $(item).data("url");
    if (url && url.length > 0) {
      $(item).load(url, () => {
        $(".card").hide().delay(100).fadeIn("600");
      });
    }
  });
})