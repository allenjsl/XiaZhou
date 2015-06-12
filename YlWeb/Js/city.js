
$.CNCN = {
    click_show: function(control, hideBox) {
        $(control).hover(function() {

        }, function() {
            $(this).find(hideBox).hide();
            $(this).find(".jiantou").attr("src", "/images/jiantouR.png");
        })
        $(control).click(function() {
            $(this).closest("ul").find("li[data-class='dh']").css({ "position": "" });
            $(this).css({ "position": "relative" });
            $(this).find(hideBox).show();
            $(this).find(".jiantou").attr("src", "/images/jiantouB.png");
        })


    }
}

//if ($("#mycncn")) {
//    //$.CNCN.click_show(".L_side01box", ".linemore");

//}

$(function() {
    $(".L_side01box li").hover(function() {
        var liShow = ".li" + $(this).index();
        $.CNCN.click_show($(this), liShow);
    });
});

//$(function() {
//    $(".L_side01box li").hover(function() {
//        var liShow = ".li" + $(this).index();
//        $(".L_side01box li").eq($(this).index()).find(liShow).show();
//        $(".L_side01box").find("li").eq($(this).index()).find(".jiantou").attr("src", "../images/jiantouB.png");
//    }, function() {
//        var liShow = ".li" + $(this).index();
//        $(".L_side01box li").eq($(this).index()).find(liShow).hide();
//        $(".L_side01box").find("li").eq($(this).index()).find(".jiantou").attr("src", "../images/jiantouR.png");
//    })
//});

