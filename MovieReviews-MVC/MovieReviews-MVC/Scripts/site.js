

$(document).ready(function(e) {

  // locate each partial section.
  // if it has a URL set, load the contents into the area.

  $(".partial-content").each(function(index, item) {
    //var url = site.baseUrl + $(item).data("url");
    //if (url && url.length > 0) {
    $(item).load($(item).data("url"), () => {
      $(".card").delay(100).hide().slideDown(200);
    });

    //.slideDown('slow')
    //.animate(
    //  { opacity: 1 },
    //  { queue: false, duration: 'slow' }
    //);
    //}
  });

});