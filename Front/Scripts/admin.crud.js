

$(document).ready(function () {


    $(".line").hover(function () {
        $(this).find(".AdminListButtons").removeClass("admin-hide");
        $(this).find(".AdminListButtons").addClass("admin-show");
    }, function () {
        $(this).find(".AdminListButtons").removeClass("admin-show");
        $(this).find(".AdminListButtons").addClass("admin-hide");
    });

});