﻿var site = site || {};
site.baseUrl = site.baseUrl || "";

$(document).ready((e) => {

  // locate each partial section.
  // if it has a URL set, load the contents into the area.

  $(".partials-content").each(function (index, item) {

    var url = site.baseUrl + $(item).data("url");
    if (url && url.length > 0) {
      $(item).load(url,
        () => {
          $(".card").hide().delay(100).fadeIn("600");
          ratingStars();
        });
    }
  });

  ratingStars();
});

// Comment reporting

function getCommentReportModal(e) {
  var url = $(e.target).data("url");

  $(".report-comment").load(url, function () {
    $('#comment-report-modal').modal('show');
  });
}

function alertCommentReportSuccess(data, status, xhr) {

  const header = JSON.parse(xhr.getResponseHeader("X-Responded-JSON"));
  if (header != null && header["status"] === 401) return alertCommentReportFailure();
  $("#comment-report-modal").modal('hide');
  const alert = $(".alert");
  alert.html("Report submitted");
  alert.addClass("show alert-success");

  setTimeout(() => {
    alert.removeClass("show alert-success");
  },
    2500);
}

function alertCommentReportFailure() {
  $("#comment-report-modal").modal('hide');
  const alert = $(".alert");
  alert.html("Ooops! Something went wrong. Try again!");
  alert.addClass("show alert-danger");

  setTimeout(() => {
    alert.removeClass("show alert-danger");
  },
    2500);
}

// Create Comment
function getCommentModal(e) {
  var url = $(e.target).data("url");

  $(".add-comment").load(url, function () {
    $('#add-comment-modal').modal('show');
  });
}

function addCommentSuccess(data, status, xhr) {
  $("#add-comment-modal").modal('hide');
  var comments = $(".comments-block");
  var url = site.baseUrl + comments.data("url");
  comments.load(url);
}

// Create Review

function getReviewModal(e) {

  var url = $(e.target).data("url");

  $(".add-review").load(url, function () {
    $('#add-review-modal').modal('show');
    ratingStars();
  });
}

function updateRatingValue() {

  $("#modal-movie-rating").rateYo("option", "onSet", function () {
    const value = $("#modal-movie-rating").rateYo("rating");
    $("#Rating").val(value);
  }); //returns a jQuery Element

}

function addReviewSuccess(data, status, xhr) {
  const header = JSON.parse(xhr.getResponseHeader("X-Responded-JSON"));
  if (header != null && header["status"] === 401) return alertCommentReportFailure();
  $("#add-review-modal").modal('hide');
  const alert = $(".alert");
  alert.html("Review submitted");
  alert.addClass("show alert-success");

  setTimeout(() => {
      alert.removeClass("show alert-success");
    },
    2500);
}


// Reviews on click navigate to review details page
$('tr[data-href]').on("click", function () {
  document.location = $(this).data('href');
});

// rateYo rating stars
function ratingStars() {
  $(".rateYo").each((index, item) => {

    const rating = parseFloat($(item).data("rating"));
    $(item).rateYo({
      rating: rating,
      maxValue: 10,
      numStars: 10,
      starWidth: "15px",
      readOnly: $(item).hasClass("readOnly"),
    });
  });
}

function updateRatingValue() {

$("#modal-movie-rating").rateYo("option", "onSet", function () {
  const value = $("#modal-movie-rating").rateYo("rating");
  $("#Rating").val(value);
}); //returns a jQuery Element

}
